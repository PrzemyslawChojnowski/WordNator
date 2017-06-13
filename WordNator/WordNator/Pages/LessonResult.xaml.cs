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
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using System.ComponentModel;
using WordNator.Models;
using System.Data;

namespace WordNator.Pages
{
    /// <summary>
    /// Interaction logic for LessonResult.xaml
    /// </summary>
    public partial class LessonResult : Page
    {
        private readonly Context _context;
        private int LessonId;
        private string UserId;
        private int Try;
        public List<LessonResultVM> UserResults;
        public int CorrectAnswers;
        private LessonType LessonType;

        public LessonResult(int lessonId, string userId, int correctAnswers, int _try, LessonType lessonType)
        {
            _context = new Context();
            UserResults = new List<LessonResultVM>();

            LessonId = lessonId;
            UserId = userId;
            Try = _try;
            CorrectAnswers = correctAnswers;
            LessonType = lessonType;

            InitializeComponent();

            setTile();
            setCorrectAnswersCount();
            setProgressBarValue(correctAnswers);
        }

        private void setTile()
        {
            var lesson = (from l in _context.Lessons
                          where l.Id == LessonId
                          select l).ToList();
            LessonNameText.Text = lesson[0].Name;
        }

        private void setProgressBarValue(int correctAnswers)
        {
            int userId = Int32.Parse(UserId);
            int questionsCount = (from lq in _context.LessonQuestions
                                  where lq.Lesson.Id == LessonId && lq.Lesson.User.Id == userId
                                  select lq).Count();


            ResultProgressBar.Value = 100 / questionsCount * correctAnswers;
        }

        private void TryAgainButton_Click(object sender, RoutedEventArgs e)
        {
            LessonPage window = new LessonPage(LessonId, UserId);

            Application.Current.MainWindow.Content = window;
        }

        private void BackToMenuButton_Click(object sender, RoutedEventArgs e)
        {
            int userId = Int32.Parse(UserId);

            UserWindow window = new UserWindow(userId);

            Application.Current.MainWindow.Content = window;
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            LessonResult window = new LessonResult(LessonId, UserId, CorrectAnswers, Try, LessonType);

            Application.Current.MainWindow.Content = window;
        }

        //Szczegóły lekcji
        private void LessonDetailsButton_Click(object sender, RoutedEventArgs e)
        {
            int userId = Int32.Parse(UserId);

            Button backButton = new Button();
            backButton.Name = "BackButton";
            backButton.Content = "Powrót";
            backButton.Click += BackButton_Click;
            Style style = this.FindResource("AccentedSquareButtonStyle") as Style;
            backButton.Style = style;

            var resultsList = (from r in _context.Results
                               where r.LessonId == LessonId && r.Try == Try && r.LessonType == LessonType 
                               select r).ToList();

            DataGrid lessonDetails = new DataGrid();
            lessonDetails.Name = "Results";
            lessonDetails.AutoGenerateColumns = false;
            lessonDetails.Width = 310;
            lessonDetails.HorizontalAlignment = HorizontalAlignment.Center;
            lessonDetails.CanUserResizeColumns = false;
            lessonDetails.CanUserResizeRows = false;
            lessonDetails.CanUserReorderColumns = false;
            lessonDetails.CanUserDeleteRows = false;
            lessonDetails.CanUserAddRows = false;

            DataGridTextColumn question = new DataGridTextColumn();
            question.Header = "Pytanie";
            question.Binding = new Binding("Question");
            question.Width = 100;
            lessonDetails.Columns.Add(question);

            DataGridTextColumn answer = new DataGridTextColumn();
            answer.Header = "Odpowiedź";
            answer.Binding = new Binding("UserAnswer");
            answer.Width = 100;
            lessonDetails.Columns.Add(answer);

            DataGridTextColumn correctness = new DataGridTextColumn();
            correctness.Header = "Poprawność";
            correctness.Binding = new Binding("AnswerCorrectness");
            answer.Width = 100;
            lessonDetails.Columns.Add(correctness);

            int a = lessonDetails.Columns.Count();

            foreach (var r in resultsList)
            {
                string answerCorrectness = "Poprawnie";

                if (r.AnswerCorrectness == false)
                    answerCorrectness = "Niepoprawnie";

                UserResults.Add(new LessonResultVM
                {
                    AnswerCorrectness = answerCorrectness,
                    Question = r.Question.EnglishWord,
                    UserAnswer = r.UserAnswer
                });
            }

            lessonDetails.ItemsSource = UserResults;

            TryAgainPanel.Children.Clear();
            BackToMenuPanel.Children.Clear();
            LessonDetailsPanel.Children.Clear();
            LessonResultsPanel.Children.Add(lessonDetails);
        }

        //Liczba poprawnych odpowiedzi
        private void setCorrectAnswersCount()
        {
            var questionsCount = (from q in _context.LessonQuestions
                                  where q.Lesson.Id == LessonId
                                  select q).Count();

            string correctAnswersCountText = CorrectAnswers.ToString() + " z " + questionsCount.ToString();
            CorrectAnswersCount.Text = correctAnswersCountText;
        }
    }
}
