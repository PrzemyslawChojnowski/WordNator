   M S B A M L   `   `   `  ����D  @WordNator, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null1clr-namespace:WordNator.PagesWordNator.Pages  5      H DMahApps.Metro, Version=1.6.0.0, Culture=neutral, PublicKeyToken=nullV;clr-namespace:MahApps.Metro.Controls;assembly=MahApps.MetroMahApps.Metro.Controls 5      "    WordNator.Pages.LessonResult   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      /localclr-namespace:WordNator.Pages     5      PControls;clr-namespace:MahApps.Metro.Controls;assembly=MahApps.Metro     5        N� Title$  LessonResult��5
      .��5      �  � ColumnDefinitions 5   
   �� 5      $	��40* �6   �� 5      $	��25* �6   �� 5      $	��40* �6    � RowDefinitions 5   
   �� 5      $	��100 �6   �� 5      $��30 �6   �� 5      $��* �6   .+�5   
   ��    AppName#��[  6   $��	WordNator��65   �� 5   
     
LessonName#��[ 6   $��Right=�68   $��0��6T   $��1��6d   $��Temat: ��6q   �� 5   
   -   5        $��LessonNameText��5   8   #��[ 6   $��1��6N   �� 5   
   #��[ 6   $��0,20,0,0q�68   $��Right=�6J   $��0��6f   $��2��6v   $��Wynik: ��6�   ]� 5   
     ProgressBarPanel#��[ 6   .+�5      -  'MahApps.Metro.Controls.MetroProgressBar  -   5        $��ResultProgressBar��5   (   $��20��6A    � Minimum$ 0Q�6M    � Maximum$	 100Q�6Y    � Value$ 0Q�6g   ��5      �� 5       �� Color# � ��6&   5       5!      ]� 5#   
   -   5        $��LessonDetailsPanel��5#        LessonDetailsPanel#��[ 6.   .+�5$      �� -   5        $��LessonDetailsButton��5$      $��Szczegóły��60     AccentedSquareButtonStyle#��[ 6f   5%      ]� 5'   
   -   5        $��TryAgainPanel��5'        LessonResultButtonPanel#��[ 6)   .+�5(      �� -   5        $��TryAgainButton��5(      $��Powtórz lekcję��6+   #��[ 6a   5)      ]� 5+   
   -   5        $��BackToMenuPanel��5+      #��[ 6+   $��Right=�6\   .+�5,      �� -   5        $��BackToMenuButton��5,      $��Powrót do menu��6-   #��[ 6e   5-      ]� 5/   
   -	   5        $��LessonResultsPanel��5/        LessonResultsPanel#��[ 6.   ]� 51   
   -
   5        $��BackButtonPanel��51        BackButtonPanel#��[ 6+   .+�52      �� -   5        $��
BackButton��52      $��Powrót��6'   #��[ 6Q   53      ]� 55   
   -   5        $��CorrectAnswersCountPanel��55        CorrectAnswersCount#��[ 64   .+�56      �� -   5        $��CorrectAnswersCount��56       	 TextBlockStyle#��[	 63   57      58      59      