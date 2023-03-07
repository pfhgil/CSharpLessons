using Microsoft.WindowsAPICodePack.Dialogs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using static Microsoft.WindowsAPICodePack.Shell.PropertySystem.SystemProperties.System;

namespace AudioPlayer
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string currentDirPath;

        private CancellationTokenSource shutDown = new CancellationTokenSource();

        private bool mouseCapturedSlider = false;

        private bool audioPlaying = true;

        private bool shuffled = false;

        private bool cyclic = false;

        // elements
        private ListBox audioListBox;
        private MediaElement mainMediaElement;
        private Slider audioSlider;

        private TextBlock audioCurrentTimeTextBlock;
        private TextBlock audioMaximumTimeTextBlock;

        private Button repeatAudioButton;
        private Button startStopButton;
        private Button mixAudioButton;

        public MainWindow()
        {
            InitializeComponent();

            audioListBox = (ListBox)this.FindName("AudioListBox");
            mainMediaElement = (MediaElement)this.FindName("MainMediaElement");

            audioSlider = (Slider)this.FindName("AudioSlider");

            System.Threading.Tasks.Task.Run(HandleAudio);
            this.Closed += (s, e) => shutDown.Cancel();

            audioCurrentTimeTextBlock = (TextBlock)this.FindName("AudioCurrentTimeTextBlock");
            audioMaximumTimeTextBlock = (TextBlock)this.FindName("AudioMaximumTimeTextBlock");

            repeatAudioButton = (Button)this.FindName("RepeatAudioButton");
            startStopButton = (Button)this.FindName("StartStopButton");
            mixAudioButton = (Button)this.FindName("MixAudioButton");
        }

        private void HandleAudio()
        {

            while (!shutDown.IsCancellationRequested)
            {
                Thread.Sleep(100);

                if (mouseCapturedSlider) continue;

                audioSlider.Dispatcher.Invoke(new Action(() =>
                {
                    audioSlider.Value = mainMediaElement.Position.Ticks;

                    audioCurrentTimeTextBlock.Text = mainMediaElement.Position.ToString();
                }));
            }
        }

        private void OpenDirectoryButton_Click(object sender, RoutedEventArgs e)
        {
            CommonOpenFileDialog dialog = new CommonOpenFileDialog() { IsFolderPicker = true };
            var showRes = dialog.ShowDialog();

            if(showRes == CommonFileDialogResult.Ok)
            {
                audioListBox.Items.Clear();
                audioListBox.Items.SortDescriptions.Add(new SortDescription("NAME", ListSortDirection.Ascending));

                currentDirPath = dialog.FileName;

                string[] filesPathes = Directory.GetFiles(currentDirPath);
                foreach(string fPath in filesPathes)
                {    
                    if(!IsAudio(fPath)) continue;

                    audioListBox.Items.Add(fPath);
                }

                if (audioListBox.Items.Count == 0) return;

                audioListBox.SelectedIndex = 0;
                mainMediaElement.Source = new Uri((string) audioListBox.Items[0], UriKind.RelativeOrAbsolute);
                mainMediaElement.Play();
            }
        }

        private bool IsAudio(string path)
        {
            string[] audioExtensions = {
                ".WAV", ".MID", ".MIDI", ".WMA", ".MP3", ".OGG", ".RMA", ".MP4"
            };

            return -1 != Array.IndexOf(audioExtensions, System.IO.Path.GetExtension(path).ToUpperInvariant());
        }

        private void AudioListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            object selectedItem = audioListBox.SelectedItem;
            if (selectedItem == null)
            {
                mainMediaElement.Stop();
                audioPlaying = false;
                return;
            }

            mainMediaElement.Source = new Uri((string) audioListBox.SelectedItem, UriKind.RelativeOrAbsolute);
        }

        private void MainMediaElement_MediaOpened(object sender, RoutedEventArgs e)
        {
            audioSlider.Maximum = mainMediaElement.NaturalDuration.TimeSpan.Ticks;
            audioMaximumTimeTextBlock.Text = mainMediaElement.NaturalDuration.TimeSpan.ToString();

            audioPlaying = true;
            mainMediaElement.Play();
        }

        private void AudioSlider_DragCompleted(object sender, System.Windows.Controls.Primitives.DragCompletedEventArgs e)
        {
            mouseCapturedSlider = false;
            mainMediaElement.Position = new TimeSpan(Convert.ToInt64(audioSlider.Value));
        }

        private void AudioSlider_DragStarted(object sender, System.Windows.Controls.Primitives.DragStartedEventArgs e)
        {
            mouseCapturedSlider = true;
        }

        private void MainMediaElement_MediaEnded(object sender, RoutedEventArgs e)
        {
            if (!cyclic)
            {
                IncrementAudio(1);
            }
            else
            {
                mainMediaElement.Position = new TimeSpan(0);
            }
        }

        private void IncrementAudio(int inc)
        {
            if (AudioListBox.SelectedIndex + inc >= audioListBox.Items.Count ||
                AudioListBox.SelectedIndex + inc < 0)
            {
                mainMediaElement.Stop();
                audioPlaying = false;
                return;
            }
            else
            {
                audioListBox.SelectedIndex += inc;
                mainMediaElement.Source = new Uri((string)audioListBox.SelectedItem, UriKind.RelativeOrAbsolute);
            }
        }

        private void StartStopButton_Click(object sender, RoutedEventArgs e)
        {
            if(audioPlaying)
            {
                mainMediaElement.Pause();
                startStopButton.Background = Brushes.Gray;
            }
            else
            {
                mainMediaElement.Play();
                startStopButton.Background = Brushes.LightGray;
            }

            audioPlaying = !audioPlaying;
        }

        private void NextAudioButton_Click(object sender, RoutedEventArgs e)
        {
            IncrementAudio(1);
        }

        private void PreviousAudioButton_Click(object sender, RoutedEventArgs e)
        {
            if(mainMediaElement.Position.Ticks > mainMediaElement.NaturalDuration.TimeSpan.Ticks / 100.0f * 10.0f)
            {
                mainMediaElement.Play();
                mainMediaElement.Position = new TimeSpan(0);
            }
            else
            {
                IncrementAudio(-1);
            }
        }

        private void MixAudioButton_Click(object sender, RoutedEventArgs e)
        {
            if (!shuffled)
            {
                Shuffle(audioListBox.Items);
                mixAudioButton.Background = Brushes.Gray;
            }
            else
            {
                audioListBox.Items.SortDescriptions.Add(new SortDescription("", ListSortDirection.Ascending));
                mixAudioButton.Background = Brushes.LightGray;
            }
            shuffled = !shuffled;
        }

        private Random rnd = new Random();
        public void Shuffle(ItemCollection list)
        {
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = rnd.Next(n + 1);
                object value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }

        private void RepeatAudioButton_Click(object sender, RoutedEventArgs e)
        { 
            cyclic = !cyclic;

            if(cyclic)
            {
                repeatAudioButton.Background = Brushes.Gray;
            }
            else
            {
                repeatAudioButton.Background = Brushes.LightGray;
            }
        }
    }
}
