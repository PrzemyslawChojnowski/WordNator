   M S B A M L   `   `   `  ����H  DMahApps.Metro, Version=1.6.0.0, Culture=neutral, PublicKeyToken=nullV;clr-namespace:MahApps.Metro.Controls;assembly=MahApps.MetroMahApps.Metro.Controls  5      D @WordNator, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null1clr-namespace:WordNator.PagesWordNator.Pages 5      %   WordNator.Pages.LessonQuestions   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      PControls;clr-namespace:MahApps.Metro.Controls;assembly=MahApps.Metro     5      /localclr-namespace:WordNator.Pages     5        N� Title$  LessonQuestions��5
      .��5      �  � ColumnDefinitions 5   
   �� 5      $	��40* �6   �� 5      $	��25* �6   �� 5      $	��40* �6    � RowDefinitions 5   
   �� 5      $	��100 �6   �� 5      $��30 �6   �� 5      $��* �6   .+�5   
   ��    AppName#��[  6   $��	WordNator��65   �� 5   
     	HelloText#��[ 6   $��Dodaj lekcję��67   $��Verdana)�6K   �� 5   
     
LessonName#��[ 6   $��Right=�68   $��0��6T   $��Temat: ��6d   �� 5   
   -   5        $��
LessonName��5   8   #��[ 6   68   ]� 5   
   -   5         ]�Name$ 	InfoPanel��5        Info#��[ 6'   $��	0,180,0,0q�6E   ]� 5   
     BackButtonPanel#��[ 6   .+�5      �� -   5        $��
BackButton��5      $��Powrót��6'     AccentedSquareButtonStyle#��[ 6Q   5      �� 5   
     
WordsPanel#��[ 6   .+�5       �� -   5        $��
PolishWord��5       *   $MahApps.Metro.Controls.TextBoxHelper   	Watermark$ Polskie słowo��6(      ClearTextButton . $��1��6�   $��2��6�   $��20��6�   $	��Top=�6�   $��Stretch=�6�   $��1,40,1,0q�6�   �� 5!      -   5        $��EnglishWord��5!      $ Angielskie słowo��6)    . $��1��6�   $��2��6�   $��20��6�   $	��Top=�6�   $��Stretch=�6�   $��1,40,1,0q�6�   �� 5"      -   5        $��AddQuestion��5"      $��40��6(   $��40��63     MetroCircleButtonStyle#���  6?   ��5#      �� 5$       �� Color# � ��6&    J� OpacityMask 5&      !� 5'        
appbar_add !� Visual# [ 6"   	 j� Stretch$
	 Fill=�6G   5)      5*      ]� 5+   
   $��10,50,10,10q�6   $��2��6)   $��2��69   .+�5,      �� $��Bottom=�6   
 �� Orientation$
 
Horizontal=�61   .+�5-      �� -   5        $��EditQuestion��5-      $��Edytuj��6H   $��5q�6Y   #��[ 6d   �� 5.      -   5        $��DeleteQuestion��5.      $��Usuń��6L   $��5q�6[   #��[ 6f   �� 5/      -	   5        $
��Save��5/      $��Zapisz lekcję��6>   $��5q�6V   #��[ 6a   50      �� 51      -
   5        $��lbQuestions��51      $��Stretch=�6)   ��52      �� 53      .R�54      � $	��0.2q�6    55      �� 56   "   $��* �63   �� 57   "   $	��100 �63   .+�59      �� ��6(   �� *
PolishWord+�� 5:      $��	100,0,0,0q�6E   ��6(   �� *EnglishWord+6E   5;      5<      5>      5?      5@      5A      