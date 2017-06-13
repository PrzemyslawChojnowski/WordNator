using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
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
using WordNator.Data;
using WordNator.Pages;

namespace WordNator
{
    /// <summary>
    /// Interaction logic for UserWindow.xaml
    /// </summary>
    public partial class UserWindow : Page
    {
        private readonly Context _context;
        private string UserId;
        private enum Operations {Update = 1, Delete = 2 };

        public UserWindow(int id)
        {
            _context = new Context();

            InitializeComponent();

            var user = (from u in _context.Users
                        where u.Id == id
                        select u).ToList();         

            Hello.Text = "Witaj " + user[0].Name.ToString();
            UserId = id.ToString();

            ListLessons();
        }

        private void Logout_Click(object sender, RoutedEventArgs e)
        {
            MainPage window = new MainPage();

            Application.Current.MainWindow.Content = window;
        }

        private void AddLesson_Click(object sender, RoutedEventArgs e)
        {
            AddLesson window = new AddLesson(UserId);

            Application.Current.MainWindow.Content = window;
        }

        private void SelectLesson_Click(object sender, RoutedEventArgs e)
        {
            Button lesson = (Button)sender;
            string name = lesson.Name;
            int id = Int32.Parse(name.Remove(0, 6));

            LessonPage window = new LessonPage(id, UserId);
            ((MainWindow)Application.Current.MainWindow).Content = window;
        }

        private void UpdateLesson_Click(object sender, RoutedEventArgs e)
        {
            int operation = (int)Operations.Update;
            SelectLesson window = new SelectLesson(UserId, operation, DialogCoordinator.Instance);
            ((MainWindow)Application.Current.MainWindow).Content = window;
        }

        private void DeleteLesson_Click(object sender, RoutedEventArgs e)
        {
            int operation = (int)Operations.Delete;
            SelectLesson window = new SelectLesson(UserId, operation, DialogCoordinator.Instance);
            ((MainWindow)Application.Current.MainWindow).Content = window;
        }

        private void ListLessons()
        {
            int userId = int.Parse(UserId);
            var lessons = (from l in _context.Lessons
                           where l.User.Id == userId
                           select l).ToList();

            if(!lessons.Any())
            {
                Style textBlockStyle = this.FindResource("InformationText") as Style;
                TextBlock block = new TextBlock();
                block.Text = "Ups...\n Nie zdefiniowałeś żadnej lekcji :(";               
                block.Style = textBlockStyle;

                Style tileStyle = this.FindResource("MenuButton") as Style;
                Tile addLesson = new Tile();
                addLesson.Content = "Dodaj lekcję";
                addLesson.Click += new RoutedEventHandler(AddLesson_Click);
                addLesson.Style = tileStyle;
               
                Lessons.Children.Add(block);
                Lessons.Children.Add(addLesson);
                Lessons.HorizontalAlignment = HorizontalAlignment.Center;
                Lessons.Orientation = Orientation.Vertical;

                return;
            }

            foreach (var l in lessons)
            {
                Tile lesson = new Tile();
                lesson.Content = l.Name;
                lesson.Name = "Lesson" + l.Id.ToString();
                lesson.Margin = new Thickness(10, 0, 0, 0);
                lesson.Click += new RoutedEventHandler(SelectLesson_Click);

                Lessons.Children.Add(lesson);               
            }
        }

        private void MyResults_Click(object sender, RoutedEventArgs e)
        {
            MyResults window = new MyResults(UserId);
            ((MainWindow)Application.Current.MainWindow).Content = window;
        }
    }
}
