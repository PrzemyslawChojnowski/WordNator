using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using WordNator.Pages;

namespace WordNator
{
    /// <summary>
    /// Interaction logic for AddLesson.xaml
    /// </summary>
    public sealed partial class AddLesson : Page
    {
        private string UserId;

        public AddLesson(string id)
        {
            InitializeComponent();

            UserId= id;
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            if (!String.IsNullOrEmpty(this.LessonName.Text))
            {
                LessonQuestions window = new LessonQuestions(this.LessonName.Text, UserId);

                Application.Current.MainWindow.Content = window;
            }
            else
            {
                InfoPanel.Children.Clear();
                TextBlock info = new TextBlock();
                info.Text = "Musisz wprowadzić jakąś nazwę";
                info.Foreground = Brushes.Red;
                InfoPanel.Children.Add(info);
            }
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            UserWindow window = new UserWindow(int.Parse(UserId));

            Application.Current.MainWindow.Content = window;
        }
    }
}
