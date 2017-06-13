using MahApps.Metro.Controls;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using WordNator.Data;
using MahApps.Metro.Controls.Dialogs;
using WordNator.Controls;

namespace WordNator.Pages
{
    /// <summary>
    /// Interaction logic for SelectLesson.xaml
    /// </summary>
    public partial class SelectLesson : Page
    {
        private readonly Context _context;
        private string UserId;
        private int Operation;
        private IDialogCoordinator dialogCoordinator;
        private CustomDialog _customDialog;
        private DeleteDialog _deleteDialog;
        private int _lessonId;
        private enum Operations { Update = 1, Delete = 2 };

        public SelectLesson(string id, int operation, IDialogCoordinator instance)
        {
            _context = new Context();

            InitializeComponent();

            UserId = id;
            Operation = operation;
            dialogCoordinator = instance;
            DataContext = this;

            if (operation == 2)
                PageName.Text = "Usuń lekcję";

            ListLessons();
        }

        private void ListLessons()
        {
            int userId = int.Parse(UserId);
            var lessons = (from l in _context.Lessons
                           where l.User.Id == userId
                           select l).ToList();

            if (!lessons.Any())
            {
                Style textBlockStyle = this.FindResource("InformationText") as Style;
                TextBlock block = new TextBlock();
                block.Text = "Ups...\n Nie zdefiniowałeś żadnej lekcji :(";
                block.Style = textBlockStyle;

                Lessons.Children.Add(block);
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

        private async void SelectLesson_Click(object sender, RoutedEventArgs e)
        {
            Button lesson = (Button)sender;
            string lessonName = lesson.Content.ToString();
            _lessonId = Int32.Parse(lesson.Name.Remove(0, 6));
            
            switch(Operation)
            {
                case 1:
                    LessonQuestions window = new LessonQuestions(lessonName, UserId, _lessonId);
                    ((MainWindow)Application.Current.MainWindow).Content = window;
                    break;
                case 2:
                    string message = "Jesteś pewien, że chcesz usunąć lekcję \"" + lessonName + "\"?";
                    string title = "Usuń";
                    _customDialog = new CustomDialog();
                    var mySettings = new MetroDialogSettings()
                    {
                        AffirmativeButtonText = "OK",
                        AnimateShow = true,
                        NegativeButtonText = "Go away!",
                        FirstAuxiliaryButtonText = "Cancel",
                        
                    };
                    _deleteDialog = new DeleteDialog();
                    _deleteDialog.AcceptButton.Click += DeleteLesson;
                    _deleteDialog.CancelButton.Click += Cancel;
                    _deleteDialog.Title.Text = title;
                    _deleteDialog.Message.Text = message;
                    _customDialog.Content = _deleteDialog;
                    
                    await dialogCoordinator.ShowMetroDialogAsync(this, _customDialog, mySettings);

                    break;
            }
        }

        private async void DeleteLesson(object sender, RoutedEventArgs e)
        {
            int userId = int.Parse(UserId);
            var lesson = (from l in _context.Lessons
                          where l.Id == _lessonId && l.User.Id == userId
                          select l).ToList(); ;

            var lessonQuestionsList = (from l in _context.LessonQuestions
                                      where l.Lesson.Id == _lessonId
                                      select l);

            foreach (var l in lessonQuestionsList)
            {
                _context.LessonQuestions.Remove(l);
            }
            _context.Lessons.Remove(lesson[0]);
            _context.SaveChanges();

            await dialogCoordinator.HideMetroDialogAsync(this, _customDialog);

            int operation = (int)Operations.Delete;
            SelectLesson window = new SelectLesson(UserId, operation, DialogCoordinator.Instance);
            ((MainWindow)Application.Current.MainWindow).Content = window;
        }

        private async void Cancel(object sender, RoutedEventArgs e)
        {
            await dialogCoordinator.HideMetroDialogAsync(this, _customDialog);
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            UserWindow window = new UserWindow(int.Parse(UserId));

            Application.Current.MainWindow.Content = window;
        }
    }
}
