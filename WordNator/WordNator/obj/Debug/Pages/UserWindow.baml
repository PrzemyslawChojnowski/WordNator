   M S B A M L   `   `   `  ����H  DMahApps.Metro, Version=1.6.0.0, Culture=neutral, PublicKeyToken=nullV;clr-namespace:MahApps.Metro.Controls;assembly=MahApps.MetroMahApps.Metro.Controls  5   	   D @WordNator, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null%clr-namespace:WordNator	WordNator 5   	      WordNator.UserWindow   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   PControls;clr-namespace:MahApps.Metro.Controls;assembly=MahApps.Metro     5   	   )localclr-namespace:WordNator     5   	     N� Title$  
UserWindow��5	   	   .��5
      �  � ColumnDefinitions 5   
   �� 5      $	��40* �6   �� 5      $	��25* �6   �� 5      $	��30* �6   �� 5      $	��15* �6    � RowDefinitions 5   
   �� 5      $	��100 �6   �� 5      $��30 �6   �� 5      $��* �6   .+�5   
   �� $��3��6   $��1��6   $��Bottom=�6,   ���	#FFEE1818$��1��6X    �� Stretch$
 Fill=�6c   
���   �	 �� X2$ 1��6�   $��0,1,2,0q�6�   �� 5   
   $��3��6   $��2��6   ���	#FFEE1818$
��Left=�6=   $
��Auto��6X   $
 Fill=�6f   
���   �	 �� Y2$ 1��6�   $��0,0,2,0q�6�   �� 5   
   $��3��6   $��1��6   ���	#FFEE1818$
��Left=�6=   $
��Auto��6X   $
 Fill=�6f   
���   �$ 1��6�   $��0,0,2,0q�6�   �� 5   
      AppName#��[  6   $��	WordNator��65   $��4��6F   �� 5   
   -   5        $��Hello��5        	HelloText#��[ 6!   �� 5   
   -   5         ��Name$ Lessons��5        
LessonList#��[ 6&   �� 5   
   $��3��6   $��1��6%   $��2��62   .+�5      �� $
��MENU��6     MenuText#��[ 6$   !   MahApps.Metro.Controls.Tile  5       -   5          Name$ 	MyResults��5       $��Moje wyniki��6/     
MenuButton#��[ 6E   6i     5!      -   5        $ 	AddLesson��5!      $��Dodaj lekcję��6/   #��[ 6F   6j     5"      -   5        $ 
EditLesson��5"      $��Edytuj lekcję��60   #��[ 6H   6l     5#      -   5        $ DeleteLesson��5#      $��Usuń lekcję��62   #��[ 6H   6l     5$      -   5        $ Logout��5$      $��Wyloguj��6,   #��[ 6>   6b   5%      5&      5'      