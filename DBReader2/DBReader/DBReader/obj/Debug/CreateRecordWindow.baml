   M S B A M L   `   `   `  ����C  ?DBReader, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null#clr-namespace:DBReaderDBReader  5   	   !    DBReader.CreateRecordWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   (localclr-namespace:DBReader     5   	   -   5          � Title$  CreateRecordWindow��5   	   $	��450��6$   $	��600��61   .��5	      � .+�5
   
   �� -   5         ��Name$ CloseWindowButton��5
      $��Закрыть��6,   $��1��6>   $��1��6R   $��0��6c   $��2��6s   6�   �� 5   
   -   5        $ AddRecordButton��5      $��Добавить��6*   $��1��6=   $��1��6Q   $��2��6b   $��2��6r   6   &   System.Windows.Controls.DataGrid  5   
   -   5          Name$ NewRecordGrid��5      $��3��6*   �� 5   
   -   5         ��Name$ LogTextBlock��5      $
��Left=�6*   $��3��6E   $��1��6Y   $��0��6f   $
��Wrap=�6v   $��	TextBlock��6�   $	��Top=�6�    � RowDefinitions 5   
   �� 5      $	��300 �6   �� 5      $��* �6   �� 5      $��20 �6    � ColumnDefinitions 5   
   �� 5      $��70 �6   �� 5      $��* �6   �� 5      $��70 �6   5      5      