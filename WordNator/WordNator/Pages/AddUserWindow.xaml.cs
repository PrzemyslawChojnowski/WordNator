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
using WordNator.Models;

namespace WordNator
{
    /// <summary>
    /// Interaction logic for AddUserWindow.xaml
    /// </summary>
    public partial class AddUserWindow : Page
    {
        private readonly Context _context;

        public AddUserWindow()
        {
            _context = new Context();

            InitializeComponent();
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            MainPage window = new MainPage();

            Application.Current.MainWindow.Content = window;
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            string userName = UserName.Text;
            
            if (!String.IsNullOrEmpty(userName))
            {
                var Co_context = new Context();
                _context.Users.Add(new User
                {
                    Name = userName
                });
                _context.SaveChanges();

                MainPage window = new MainPage();

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
    }
}
