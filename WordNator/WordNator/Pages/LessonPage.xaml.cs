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
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using Microsoft.Speech.Recognition;
using System.Globalization;
using MahApps.Metro.IconPacks;

namespace WordNator.Pages
{
    /// <summary>
    /// Interaction logic for LessonPage.xaml
    /// </summary>
    public partial class LessonPage : Page
    {
        private readonly Context _context;
        private int LessonId;
        private string UserId;
        private List<Question> Questions;
        private Question RandomQuestion;
        private static Random rand;
        private static int QuestionNo;
        private static int QuestionCount;
        private static int QuestionIndex;
        private List<Result> UserAnswers;
        private int Try;
        private int CorrectAnswers;
        private LessonType LessonType;
        private LessonLanguage LessonLanguage;
        private bool isRecognizing;

        SpeechRecognitionEngine sre;

        public LessonPage(int id, string userId)
        {
            _context = new Context();
            LessonId = id;
            UserId = userId;
            Questions = (from q in _context.LessonQuestions
                         where q.Lesson.Id == LessonId
                         select q.Question).ToList();
            rand = new Random();
            UserAnswers = new List<Result>();
            QuestionCount = Questions.Count;
            QuestionNo = 0;
            Try = 0;
            CorrectAnswers = 0;
            isRecognizing = false;

            InitializeComponent();

            LessonName();
            SpeechQuestion();
        }

        public void TryNumber()
        {
            try
            {
                var tryIndex = _context.Results
                    .Where(r => r.LessonId == LessonId &&
                    r.LessonType == LessonType &&
                    r.LessonLanguage == LessonLanguage)
                    .Max(r => r.Try);

                Try = tryIndex + 1;
            }
            catch
            {
                Try = 1;
            }
        }

        public void LessonName()
        {
            string lessonName = (from l in _context.Lessons
                                 where l.Id == LessonId
                                 select l.Name).First().ToString();

            LessonNameText.Text = lessonName;
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            UserWindow window = new UserWindow(int.Parse(UserId));

            Application.Current.MainWindow.Content = window;
        }
        //Lekcja pisana
        private void SpellLesson_Click(object sender, RoutedEventArgs e)
        {
            LessonType = LessonType.Spelling;
            SelectLessonType();
        }
        //Lekcja mówiona
        private void SpeechLesson_Click(object sender, RoutedEventArgs e)
        {
            LessonType = LessonType.Speaking;
            EnglishLesson_Click(sender, e);
        }
        //Tłumaczenie na angielskie
        private void EnglishLesson_Click(object sender, RoutedEventArgs e)
        {
            LessonLanguage = LessonLanguage.English;

            if (Try == 0)
                TryNumber();

            SpeechPanel.Children.Clear();
            SpellPanel.Children.Clear();
            RandomQuestion = SelectRandomQuestion();
            QuestionNo++;

            Style style = this.FindResource("AccentedSquareButtonStyle") as Style;
            Button nextQuestion = new Button();
            nextQuestion.Style = style;

            if (QuestionNo < QuestionCount)
            {
                nextQuestion.Content = "Następne";
                nextQuestion.Click += CheckAnswerButton_Click;
            }
            else
            {
                nextQuestion.Content = "Zakończ";
                nextQuestion.Click += FinishLesson;
            }
            NextQuestionPanel.Children.Clear();
            NextQuestionPanel.Children.Add(nextQuestion);

            LessonInfoQuestion.Text = "Księciuniu, pytanko " + QuestionNo.ToString() + " z " + QuestionCount.ToString();

            if (LessonType == LessonType.Spelling)
            {
                TextBox answer = new TextBox();
                TextBoxHelper.SetWatermark(answer, "Angielskie tłumaczenie");
                TextBoxHelper.SetClearTextButton(answer, true);
                answer.Height = 20;
                answer.VerticalAlignment = VerticalAlignment.Center;
                answer.Margin = new Thickness(0, 2, 0, 2);
                answer.Name = "AnswerTextBox";
                AnswerPanel.Children.Clear();
                AnswerPanel.Children.Add(answer);
            }
            else if (LessonType == LessonType.Speaking)
            {
                TextBlock recognizedWord = new TextBlock();
                recognizedWord.Name = "RecognizedWord";
                recognizedWord.FontSize = 18;
                recognizedWord.TextAlignment = TextAlignment.Center;
                recognizedWord.Text = "Naciśnij przycisk rozpoznawania";
                AnswerPanel.Children.Clear();
                AnswerPanel.Children.Add(recognizedWord);

                Style circleButtonStyle = this.FindResource("MetroCircleButtonStyle") as Style;
                PackIconMaterial icon = new PackIconMaterial();
                icon.Kind = PackIconMaterialKind.Microphone;               

                Button listenButton = new Button();
                listenButton.Name = "Recognize";
                listenButton.Click += Recognize;
                listenButton.Style = circleButtonStyle;
                listenButton.Content = icon;
               
                ListenPanel.Children.Clear();
                ListenPanel.Children.Add(listenButton);
            }
    
            TextBlock question = new TextBlock();
            question.FontSize = 16;
            question.FontWeight = FontWeights.SemiBold;
            question.Text = RandomQuestion.PolishWord;
            QuestionPanel.Children.Clear();
            QuestionPanel.Children.Add(question);                                 
        }
        //Tłumaczenie na polskie
        private void PolishLesson_Click(object sender, RoutedEventArgs e)
        {
            LessonLanguage = LessonLanguage.Polish;

            if (Try == 0)
                TryNumber();

            SpeechPanel.Children.Clear();
            SpellPanel.Children.Clear();
            RandomQuestion = SelectRandomQuestion();
            QuestionNo++;

            Style style = this.FindResource("AccentedSquareButtonStyle") as Style;
            Button nextQuestion = new Button();
            nextQuestion.Style = style;

            if (QuestionNo < QuestionCount)
            {
                nextQuestion.Content = "Następne";
                nextQuestion.Click += CheckAnswerButton_Click;
            }
            else
            {
                nextQuestion.Content = "Zakończ";
                nextQuestion.Click += FinishLesson;
            }
            NextQuestionPanel.Children.Clear();
            NextQuestionPanel.Children.Add(nextQuestion);

            LessonInfoQuestion.Text = "Księciuniu, pytanko " + QuestionNo.ToString() + " z " + QuestionCount.ToString();

            TextBox answer = new TextBox();
            TextBoxHelper.SetWatermark(answer, "Polskie tłumaczenie");
            TextBoxHelper.SetClearTextButton(answer, true);
            answer.Height = 20;
            answer.VerticalAlignment = VerticalAlignment.Center;
            answer.Margin = new Thickness(0, 2, 0, 2);
            answer.Name = "AnswerTextBox";
            AnswerPanel.Children.Clear();
            AnswerPanel.Children.Add(answer);

            TextBlock question = new TextBlock();
            question.FontSize = 16;
            question.FontWeight = FontWeights.SemiBold;
            question.Text = RandomQuestion.EnglishWord;
            QuestionPanel.Children.Clear();
            QuestionPanel.Children.Add(question);
        }
        //Jakie słówka będą tłumaczone
        private void SelectLessonType()
        {
            Button englishLesson = new Button();
            englishLesson.Style = SpellLesson.Style;
            englishLesson.Content = "Polskie";
            englishLesson.Click += EnglishLesson_Click;
            englishLesson.Name = "EnglishLessonButton";

            SpeechPanel.Children.Clear();
            SpeechPanel.Children.Add(englishLesson);

            Button polishLesson = new Button();
            polishLesson.Style = SpeechLesson.Style;
            polishLesson.Content = "Angielskie";
            polishLesson.Click += PolishLesson_Click;
            polishLesson.Name = "PolishLessonButton";

            SpellPanel.Children.Clear();
            SpellPanel.Children.Add(polishLesson);

            LessonInfoQuestion.Text = "Księciuniu, jakie słówka tłumaczymy?";
        }

        private void CheckAnswerButton_Click(object sender, RoutedEventArgs e)
        {
            if (CheckAnswer() == 0)
                return;

            RemoveQuestion(QuestionIndex);

            if (LessonLanguage == LessonLanguage.English)
                EnglishLesson_Click(sender, e);
            else if (LessonLanguage == LessonLanguage.Polish)
                PolishLesson_Click(sender, e);
        }

        private void FinishLesson(object sender, RoutedEventArgs e)
        {
            CheckAnswer();

            var questionsCount = (from q in _context.LessonQuestions
                                  where q.Lesson.Id == LessonId
                                  select q).Count();

            foreach (var answer in UserAnswers)
            {
                _context.Results.Add(answer);
            }

            _context.ResultsSummary.Add(new ResultsSummary
            {
                CorrectAnswersCount = CorrectAnswers,
                LessonId = LessonId,
                LessonLanguage = LessonLanguage,
                LessonType = LessonType,
                Try = Try,
                QuestionsCount = questionsCount
            });

            _context.SaveChanges();

            LessonResult window = new LessonResult(LessonId, UserId, CorrectAnswers, Try, LessonType);

            Application.Current.MainWindow.Content = window;
        }

        private int CheckAnswer()
        {
            Result answer = new Result();

            if (LessonType == LessonType.Spelling)
            {
                TextBox userAnswer = AnswerPanel.FindChild<TextBox>("AnswerTextBox");                
                
                if(String.IsNullOrEmpty(userAnswer.Text))
                {
                    TextBlock info = new TextBlock();
                    info.Text = "Nie wprowadzono żadnego słowa...";
                    info.Foreground = Brushes.Red;
                    WarningPanel.Children.Clear();
                    WarningPanel.Children.Add(info);
                    return 0;
                }
                answer.UserAnswer = userAnswer.Text;
            }
            else if(LessonType == LessonType.Speaking)
            {
                TextBlock userAnswer = AnswerPanel.FindChild<TextBlock>("RecognizedWord");

                if(String.IsNullOrEmpty(userAnswer.Text) || userAnswer.Text == "Mów..." || 
                    userAnswer.Text == "Naciśnij przycisk rozpoznawania" || userAnswer.Text == "Nie rozpoznano")
                {
                    TextBlock info = new TextBlock();
                    info.Text = "Nie rozpoznano żadnego słowa...";
                    info.Foreground = Brushes.Red;
                    WarningPanel.Children.Clear();
                    WarningPanel.Children.Add(info);
                    stopRecognizing();
                    return 0;
                }

                answer.UserAnswer = userAnswer.Text;
            }

            answer.QuestionId = Questions[QuestionIndex].Id;
            answer.LessonId = LessonId;
            answer.Try = Try;
            answer.LessonLanguage = LessonLanguage;
            answer.LessonType = LessonType;

            if (LessonLanguage == LessonLanguage.English)
            {
                if (answer.UserAnswer.ToLower() == Questions[QuestionIndex].EnglishWord.ToLower())
                {
                    answer.AnswerCorrectness = true;
                    CorrectAnswers++;
                }
                else
                    answer.AnswerCorrectness = false;
            }
            else if (LessonLanguage == LessonLanguage.Polish)
            {
                if (answer.UserAnswer.ToLower() == Questions[QuestionIndex].PolishWord.ToLower())
                {
                    answer.AnswerCorrectness = true;
                    CorrectAnswers++;
                }
                else
                    answer.AnswerCorrectness = false;
            }
            UserAnswers.Add(answer);

            return 1;
        }

        private Question SelectRandomQuestion()
        {
            QuestionIndex = rand.Next(Questions.Count);

            return Questions[QuestionIndex];
        }

        private void RemoveQuestion(int index)
        {
            var q = Questions[index];
            Questions.Remove(q);
        }

        private void SpeechQuestion()
        {
            sre = new SpeechRecognitionEngine(new CultureInfo("en-GB"));

            Choices questions = new Choices();

            int userId = Int32.Parse(UserId);

            var questionsList = from q in _context.Questions
                                select q;

            foreach (var q in questionsList)
            {
                questions.Add(q.EnglishWord);
            }

            GrammarBuilder gb = new GrammarBuilder();
            gb.Culture = new CultureInfo("en-GB");
            gb.Append(questions);

            Grammar g = new Grammar(gb);
            sre.LoadGrammar(g);

            sre.SpeechRecognized += new EventHandler<SpeechRecognizedEventArgs>(sre_SpeechRecognized);
            sre.SetInputToDefaultAudioDevice();
            sre.SpeechRecognitionRejected += new EventHandler<SpeechRecognitionRejectedEventArgs>(sre_SpeechRecognitionRejected);
        }

        private void Recognize(object sender, RoutedEventArgs e)
        {
            if (isRecognizing == false)
            {
                startRecognizing();
            }
            else
            {
                stopRecognizing();
            }            
        }

        private void startRecognizing()
        {
            TextBlock userAnswer = AnswerPanel.FindChild<TextBlock>("RecognizedWord");
            userAnswer.Text = "Mów...";
            sre.RecognizeAsync(RecognizeMode.Single);           
            isRecognizing = true;

            PackIconMaterial icon = new PackIconMaterial();
            icon.Kind = PackIconMaterialKind.MicrophoneOff;
            Button recognizeButton = ListenPanel.FindChild<Button>("Recognize");
            recognizeButton.Content = icon;
        }

        private void stopRecognizing()
        {
            TextBlock userAnswer = AnswerPanel.FindChild<TextBlock>("RecognizedWord");
            userAnswer.Text = "Naciśnij przycisk rozpoznawania";
            sre.RecognizeAsyncCancel();            
            isRecognizing = false;

            PackIconMaterial icon = new PackIconMaterial();
            icon.Kind = PackIconMaterialKind.Microphone;
            Button recognizeButton = ListenPanel.FindChild<Button>("Recognize");
            recognizeButton.Content = icon;
        }

        void sre_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            WarningPanel.Children.Clear();
            TextBlock userAnswer = AnswerPanel.FindChild<TextBlock>("RecognizedWord");
            userAnswer.Text = e.Result.Text;

            PackIconMaterial icon = new PackIconMaterial();
            icon.Kind = PackIconMaterialKind.Microphone;
            Button recognizeButton = ListenPanel.FindChild<Button>("Recognize");
            recognizeButton.Content = icon;
            isRecognizing = false;
        }

        private void sre_SpeechRecognitionRejected(object sender, SpeechRecognitionRejectedEventArgs e)
        {
            WarningPanel.Children.Clear();
            TextBlock userAnswer = AnswerPanel.FindChild<TextBlock>("RecognizedWord");
            userAnswer.Text = "Nie rozpoznano";

            PackIconMaterial icon = new PackIconMaterial();
            icon.Kind = PackIconMaterialKind.Microphone;
            Button recognizeButton = ListenPanel.FindChild<Button>("Recognize");
            recognizeButton.Content = icon;
            isRecognizing = false;
        }
    }
}
