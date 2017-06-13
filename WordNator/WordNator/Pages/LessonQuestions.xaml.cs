using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
    /// Interaction logic for LessonQuestions.xaml
    /// </summary>
    public partial class LessonQuestions : Page
    {
        private readonly Context _context;
        private string lesson;
        private string UserId;

        private ObservableCollection<LessonQuestionsVM> questions = new ObservableCollection<LessonQuestionsVM>();
        private int doUpdate = 0;
        private string english = "";
        private string polish = "";

        public LessonQuestions(string lessonName, string userId, int? lessonNo = -1)
        {
            _context = new Context();
            lesson = lessonName;

            InitializeComponent();

            if(lessonNo > -1)
            {
                var q = from lq in _context.LessonQuestions
                        where lq.Lesson.Id == lessonNo
                        select lq;

                foreach(var a in q)
                {
                    questions.Add(new LessonQuestionsVM()
                    {
                        EnglishWord = a.Question.EnglishWord,
                        PolishWord = a.Question.PolishWord
                    });
                }
            }

            lbQuestions.ItemsSource = questions;
            this.LessonName.Text = lessonName;
            UserId = userId;
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            UserWindow window = new UserWindow(int.Parse(UserId));

            Application.Current.MainWindow.Content = window;
        }

        private void AddQuestion_Click(object sender, RoutedEventArgs e)
        {
            if (String.IsNullOrEmpty(PolishWord.Text) && String.IsNullOrEmpty(EnglishWord.Text))
            {
                InfoPanel.Children.Clear();
                TextBlock info = new TextBlock();
                info.Text = "Musisz wprowadzić oba słowa!";
                info.Foreground = Brushes.Red;
                InfoPanel.Children.Add(info);
            }
            else if (String.IsNullOrEmpty(PolishWord.Text))
            {
                InfoPanel.Children.Clear();
                TextBlock info = new TextBlock();
                info.Text = "Musisz wprowadzić polskie słowo!";
                info.Foreground = Brushes.Red;
                InfoPanel.Children.Add(info);
            }
            else if (String.IsNullOrEmpty(EnglishWord.Text))
            {
                InfoPanel.Children.Clear();
                TextBlock info = new TextBlock();
                info.Text = "Musisz wprowadzić angielskie tłumaczenie!";
                info.Foreground = Brushes.Red;
                InfoPanel.Children.Add(info);
            }
            else if (!String.IsNullOrEmpty(PolishWord.Text) && !String.IsNullOrEmpty(EnglishWord.Text)
                && String.IsNullOrEmpty(polish) && String.IsNullOrEmpty(english) && doUpdate != 1)
            {
                questions.Add(new LessonQuestionsVM()
                {
                    PolishWord = PolishWord.Text,
                    EnglishWord = EnglishWord.Text
                });

                InfoPanel.Children.Clear();
                PolishWord.Text = "";
                EnglishWord.Text = "";
            }
            else if (!String.IsNullOrEmpty(PolishWord.Text) && !String.IsNullOrEmpty(EnglishWord.Text)
                && !String.IsNullOrEmpty(polish) && !String.IsNullOrEmpty(english) && doUpdate == 1)
            {
                var selectedQuestion = questions.Where(q => q.PolishWord == polish && q.EnglishWord == english).First();
                questions.Remove(selectedQuestion as LessonQuestionsVM);

                questions.Add(new LessonQuestionsVM()
                {
                    PolishWord = PolishWord.Text,
                    EnglishWord = EnglishWord.Text
                });

                InfoPanel.Children.Clear();
                PolishWord.Text = "";
                EnglishWord.Text = "";
                polish = "";
                english = "";
                doUpdate = 0;
            }           
        }

        private void EditQuestion_Click(object sender, RoutedEventArgs e)
        {
            if (lbQuestions.SelectedItem != null)
            {
                var a = lbQuestions.SelectedItem;
                var c = questions.Where(x => x == a).ToList();

                if (c.Any())
                {
                    EnglishWord.Text = c[0].EnglishWord;
                    PolishWord.Text = c[0].PolishWord;

                    doUpdate = 1;
                    english = c[0].EnglishWord;
                    polish = c[0].PolishWord;
                }
            }
        }

        private void DeleteQuestion_Click(object sender, RoutedEventArgs e)
        {
            if (lbQuestions.SelectedItem != null)
                questions.Remove(lbQuestions.SelectedItem as LessonQuestionsVM);
        }

        private void SaveLesson_Click(object sender, RoutedEventArgs e)
        {
            if (questions.Any() && !String.IsNullOrEmpty(lesson) && !String.IsNullOrEmpty(UserId))
            {
                var id = int.Parse(UserId);
                var user = (from u in _context.Users
                            where u.Id == id
                            select u).ToList();

                var loggedUser = user[0];

                var lessonList = (from l in _context.Lessons
                                  where l.Name == lesson && l.User.Id == loggedUser.Id
                                  select l).ToList();

                if (!lessonList.Any())
                {
                    _context.Lessons.Add(new Lesson { Name = lesson, User = loggedUser });
                    _context.SaveChanges();

                    var lastAddedLesson = (from l in _context.Lessons
                                           select l).ToList().Last();

                    foreach (var q in questions)
                    {
                        var isAnyQuestion = from quest in _context.Questions
                                            where quest.EnglishWord == q.EnglishWord &&
                                            quest.PolishWord == q.PolishWord
                                            select quest;

                        if (!isAnyQuestion.Any())
                        {
                            _context.Questions.Add(new Question
                            {
                                EnglishWord = q.EnglishWord,
                                PolishWord = q.PolishWord
                            });

                            _context.SaveChanges();

                            var lastAddedQuestion = (from qu in _context.Questions
                                                     select qu).ToList().Last();

                            _context.LessonQuestions.Add(new LessonQuestion
                            {
                                Question = lastAddedQuestion,
                                Lesson = lastAddedLesson
                            });
                        }
                        else
                        {
                            var selectedQuestion = (isAnyQuestion.ToList())[0];
                            _context.LessonQuestions.Add(new LessonQuestion
                            {
                                Question = selectedQuestion,
                                Lesson = lastAddedLesson
                            });
                        }
                    }

                    _context.SaveChanges();
                }
                else
                {
                    var myLesson = lessonList[0];
                    var lessonQuestionsList = (from lq in _context.LessonQuestions
                                               where lq.Lesson.Id == myLesson.Id
                                               select lq).ToList();
                    foreach (var lq in lessonQuestionsList)
                    {
                        var lessonQuestion = (from quest in _context.Questions
                                              where quest.Id == lq.Question.Id
                                              select quest).ToList()[0];
                        _context.LessonQuestions.Remove(lq);
                        _context.SaveChanges();
                        _context.Questions.Remove(lessonQuestion);
                        _context.SaveChanges();
                    }

                    foreach (var q in questions)
                    {
                        _context.Questions.Add(new Question
                        {
                            EnglishWord = q.EnglishWord,
                            PolishWord = q.PolishWord
                        });

                        _context.SaveChanges();

                        var lastAddedQuestion = (from qu in _context.Questions
                                                 select qu).ToList().Last();

                        _context.LessonQuestions.Add(new LessonQuestion
                        {
                            Question = lastAddedQuestion,
                            Lesson = myLesson
                        });
                    }

                    _context.SaveChanges();
                }

                UserWindow window = new UserWindow(int.Parse(UserId));
                Application.Current.MainWindow.Content = window;
            }
            else
            {
                InfoPanel.Children.Clear();
                TextBlock info = new TextBlock();
                info.Text = "Musisz wprowadzić przynajmniej 1 pytanie!";
                info.Foreground = Brushes.Red;
                InfoPanel.Children.Add(info);
            }
        }
    }
}
