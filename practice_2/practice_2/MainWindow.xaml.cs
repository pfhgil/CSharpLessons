using System;
using System.Collections.Generic;
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

namespace practice_2
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private List<Note> notes = new List<Note>();

        private ListBox notesListBox;
        private DatePicker notesDatePicker;

        private TextBox noteNameTextBox;
        private TextBox noteDescriptionTextBox;

        private Note currentSelectedNote;

        private Thread handleChangesThread;

        private string pathToNotes = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "/notes.json";

        public MainWindow()
        {
            InitializeComponent();

            notes = Serializer.Deserialize<List<Note>>(pathToNotes);

            notesListBox = (ListBox)this.FindName("NotesListBox");
            notesDatePicker = (DatePicker)this.FindName("NotesDatePicker");

            noteNameTextBox = (TextBox)this.FindName("NoteNameTextBox");
            noteDescriptionTextBox = (TextBox)this.FindName("NoteDescriptionTextBox");

            SetNoteEditingEnabled(false);

            notesDatePicker.SelectedDate = DateTime.Now;

            handleChangesThread = new Thread(() => HandleChanges());
            handleChangesThread.Start();
        }

        private void MainWindow_Closed(object sender, EventArgs e)
        {
            handleChangesThread.Abort();
            Serializer.Serialize(pathToNotes, notes);
        }

        private void HandleChanges()
        {
            while(true)
            {
                Thread.Sleep(5000);

                Serializer.Serialize(pathToNotes, notes);
            }
        }

        private void AddNoteButton_Click(object sender, RoutedEventArgs e)
        {    
            Note newNote = new Note();
            newNote.dateTime = notesDatePicker.SelectedDate.Value;   
            
            notes.Add(newNote);
 
            notesListBox.Items.Add(newNote);  
        }

        private void DeleteNoteButton_Click(object sender, RoutedEventArgs e)
        {
            if (currentSelectedNote == null) return;

            int idx = notesListBox.Items.IndexOf(currentSelectedNote);

            notesListBox.Items.Remove(currentSelectedNote);
            notes.Remove(currentSelectedNote);

            idx--;
            if (idx < 0)
            {
                idx = 0;
            }
            notesListBox.SelectedIndex = idx;

        }

        private void NotesDatePicked_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            notesListBox.Items.Clear();
            GetDateNotes(notesDatePicker.SelectedDate.Value).ForEach(note => notesListBox.Items.Add(note));
        }

        private List<Note> GetDateNotes(DateTime dateTime)
        {
            List<Note> foundNotes = new List<Note>();
            foundNotes.AddRange(notes.FindAll(note => note.dateTime == dateTime));
            return foundNotes;
        }

        private void NotesListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            object selectedItem = notesListBox.SelectedItem;
            if (selectedItem == null)
            {
                SetNoteEditingEnabled(false);
                return;
            }

            SetNoteEditingEnabled(true);

            currentSelectedNote = (Note)selectedItem;
            noteNameTextBox.Text = currentSelectedNote.name;
            noteDescriptionTextBox.Text = currentSelectedNote.description;
        }

        private void SetNoteEditingEnabled(bool enabled) 
        {
            noteNameTextBox.IsEnabled = enabled;
            noteDescriptionTextBox.IsEnabled = enabled;
        }

        private void NoteNameTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (currentSelectedNote == null) return;

            currentSelectedNote.name = noteNameTextBox.Text;
            notesListBox.Items.Refresh();
        }

        private void NoteDescriptionTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (currentSelectedNote == null) return;

            currentSelectedNote.description = noteDescriptionTextBox.Text;
        }
    }
}
