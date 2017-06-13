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
using WordNator.Models;

namespace WordNator.Pages
{
    /// <summary>
    /// Interaction logic for MyResults.xaml
    /// </summary>
    public partial class MyResults : Page
    {
        private readonly Context _context;
        private string UserId;
        private List<Results> LessonsList;

        public MyResults(string userId)
        {
            InitializeComponent();

            _context = new Context();
            UserId = userId;
            InitializeLessonsList();
        }

        private void InitializeLessonsList()
        {
            LessonsList = new List<Results>();
            int userId = Int32.Parse(UserId);

            var lessonsQuery = (from rs in _context.ResultsSummary
                                where rs.Lesson.User.Id == userId
                                select rs).ToList();

            string lang;
            string type;

            foreach (var r in lessonsQuery)
            {
                if (r.LessonLanguage == LessonLanguage.English)
                    lang = "Angielski";
                else
                    lang = "Polski";

                if (r.LessonType == LessonType.Speaking)
                    type = "Mówienie";
                else
                    type = "Pisanie";

                LessonsList.Add(new Results
                {
                    QuestionsCount = r.QuestionsCount,
                    CorrectAnswersCount = r.CorrectAnswersCount,
                    LessonLanguage = lang,
                    LessonType = type,
                    Try = r.Try,
                    LessonName = r.Lesson.Name
                });
            }

            DataGrid lessonsList = new DataGrid();
            lessonsList.AutoGenerateColumns = false;
            lessonsList.CanUserAddRows = false;
            lessonsList.CanUserAddRows = false;
            lessonsList.CanUserDeleteRows = false;
            lessonsList.CanUserReorderColumns = false;
            lessonsList.CanUserResizeColumns = false;
            lessonsList.CanUserResizeRows = false;
            lessonsList.HorizontalAlignment = HorizontalAlignment.Center;

            DataGridTextColumn lessonName = new DataGridTextColumn();
            lessonName.Header = "Lekcja";
            lessonName.Binding = new Binding("LessonName");
            lessonName.Width = 100;
            lessonsList.Columns.Add(lessonName);

            DataGridTextColumn lessonType = new DataGridTextColumn();
            lessonType.Header = "Rodzaj lekcji";
            lessonType.Binding = new Binding("LessonType");
            lessonType.Width = 110;
            lessonsList.Columns.Add(lessonType);

            DataGridTextColumn lessonLanguage = new DataGridTextColumn();
            lessonLanguage.Header = "Język";
            lessonLanguage.Binding = new Binding("LessonLanguage");
            lessonLanguage.Width = 100;
            lessonsList.Columns.Add(lessonLanguage);

            DataGridTextColumn lessonTry = new DataGridTextColumn();
            lessonTry.Header = "Próba";
            lessonTry.Binding = new Binding("Try");
            lessonTry.Width = 60;
            lessonsList.Columns.Add(lessonTry);

            DataGridTextColumn questionsCount = new DataGridTextColumn();
            questionsCount.Header = "Liczba pytań";
            questionsCount.Binding = new Binding("QuestionsCount");
            questionsCount.Width = 105;
            lessonsList.Columns.Add(questionsCount);

            DataGridTextColumn correctAnswersCount = new DataGridTextColumn();
            correctAnswersCount.Header = "Poprawne odpowiedzi";
            correctAnswersCount.Binding = new Binding("CorrectAnswersCount");
            correctAnswersCount.Width = 170;
            lessonsList.Columns.Add(correctAnswersCount);

            lessonsList.ItemsSource = LessonsList;

            DataGridList.Children.Add(lessonsList);
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            UserWindow window = new UserWindow(int.Parse(UserId));

            Application.Current.MainWindow.Content = window;
        }
    }

    public class Results
    {
        public string LessonName { get; set; }
        public string LessonType { get; set; }
        public string LessonLanguage { get; set; }
        public int Try { get; set; }
        public int QuestionsCount { get; set; }
        public int CorrectAnswersCount { get; set; }       
    }
}
