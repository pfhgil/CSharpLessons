   M S B A M L   `   `   `  ����F  BAudioPlayer, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null)clr-namespace:AudioPlayerAudioPlayer  5   	       AudioPlayer.MainWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   +localclr-namespace:AudioPlayer     5   	     � Title$  
MainWindow��5   	   $	��450��6   $	��600��6)   .��5	      � .+�5
   
   �� $��	0,-15,0,0q�6   $��0��6'   $��0��67    J� RenderTransformOrigin$ 0.5,0.75&�6D   $��25��6e   z�5      V� 5      .~�5      ��  �� ScaleY$ -1Q�6%   �� 5      �� 5      T� 5      5      5      �� 5   
   -   5         ��Name$ OpenDirectory��5      $��0��6(   $��0��68   ��5      �� 5      $��/icons/dir50.png��6   5      �� 5   
   -   5        $ StartStopButton��5      $��11��6*   $��17��6;   ��5      �� 5      $��/icons/start30.png��6   5      �� 5!   
   -   5        $ NextAudioButton��5!      $��12��6*   $��17��6;   ��5"      �� 5#      $��/icons/go_forward50.png��6   5%      �� 5'   
   -   5        $ PreviousAudioButton��5'      $��10��6.   $��17��6?   ��5(      �� 5)      $��/icons/go_back50.png��6   5+      �� 5-   
   -   5        $ MixAudioButton��5-      $��8��6)   $��17��69   ��5.      �� 5/      $��/icons/mix64.png��6   51      �� 53   
   -   5        $ RepeatAudioButton��53      $��14��6,   $��17��6=   ��54      �� 55      $��/icons/repeat48.png��6   57      �� 59   
   -   5         ��Name$ AudioListBox��59      $��25��6(   $��14��6=   $��1��6O   $��0,0,0,23q�6�   r� 5;   
   -   5         r�Name$ MainMediaElement��5;       r� LoadedBehavior$ Manual=�61   $��3��6I   $��19��6Y   $��3��6n   $��14��6   6�   �� 5<   
   -	   5         ��Name$ AudioSlider��5<      $��4��6&   $��15��66   $��17��6D   $��Center=�5=      5?      �� 5A   
   -
   5         ��Name$ AudioCurrentTimeTextBlock��5A      $
��Left=�67   $��15��6R   	 �� TextAlignment$	 Center=�6`   $
��Wrap=�6w   $��Center=�6�   $��18��6�   $��72��6�   $��1��6�   $��3��6�   �� 6�   
 � Language$
 ru-ru�6�   $�00:00:00��6�   6
  �� 5B   
   -   5        $ AudioMaximumTimeTextBlock��5B      $��15��67   $
��Wrap=�6E   $	 Center=�6Y   $��Center=�6p   $��18��6�   $��72��6�   $��21��6�   $��3��6�   $��Right=�6�   �� 6�   $
 ru-ru�6�   $�00:00:00��6�   6   � RowDefinitions 5E   
   �� 5F      $��* �6   �� 5G      $��* �6   �� 5H      $��* �6   �� 5I      $��* �6   �� 5J      $��* �6   �� 5K      $��* �6   �� 5L      $��* �6   �� 5M      $��* �6   �� 5N      $��* �6   �� 5O      $��* �6   �� 5P      $��* �6   �� 5Q      $��* �6   �� 5R      $��* �6   �� 5S      $��* �6   �� 5T      $��* �6   �� 5U      $��* �6   �� 5V      $��* �6   �� 5W      $��* �6   �� 5X      $��* �6    � ColumnDefinitions 5[   
   �� 5\      $��* �6   �� 5]      $��* �6   �� 5^      $��* �6   �� 5_      $��* �6   �� 5`      $��* �6   �� 5a      $��* �6   �� 5b      $��* �6   �� 5c      $��* �6   �� 5d      $��* �6   �� 5e      $��* �6   �� 5f      $��* �6   �� 5g      $��* �6   �� 5h      $��* �6   �� 5i      $��* �6   �� 5j      $��* �6   �� 5k      $��* �6   �� 5l      $��* �6   �� 5m      $��* �6   �� 5n      $��* �6   �� 5o      $��* �6   �� 5p      $��* �6   �� 5q      $��* �6   �� 5r      $��* �6   �� 5s      $��* �6   �� 5t      $��* �6   5v      5w      