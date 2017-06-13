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

namespace WordNator
{
    /// <summary>
    /// Interaction logic for MainPage.xaml
    /// </summary>
    public partial class MainPage : Page
    {
        private readonly Context _context;

        public MainPage()
        {
            InitializeComponent();

            _context = new Context();
            ListUsers();
        }

        public void ListUsers()
        {
            var users = (from u in _context.Users
                         select u).ToList();

            if (!users.Any())
            {
                TextBlock block = new TextBlock();
                block.Text = "Brak \nużytkowników";
                block.TextAlignment = TextAlignment.Center;
                block.VerticalAlignment = VerticalAlignment.Center;
                block.HorizontalAlignment = HorizontalAlignment.Center;

                Users.Children.Add(block);

                return;
            }

            foreach (var u in users)
            {
                Tile user = new Tile();
                user.Content = u.Name;
                user.Name = "User" + u.Id.ToString();
                user.Margin = new Thickness(10, 0, 0, 0);
                user.Click += new RoutedEventHandler(SelectUser_Click);

                Users.Children.Add(user);
            }
        }

        private void AddUser_Click(object sender, RoutedEventArgs e)
        {
            AddUserWindow addUser = new AddUserWindow();

            ((MainWindow)Application.Current.MainWindow).Content = addUser;
        }

        private void SelectUser_Click(object sender, RoutedEventArgs e)
        {
            Button user = (Button)sender;
            string name = user.Name;
            int id = Int32.Parse(name.Remove(0, 4));
            UserWindow window = new UserWindow(id);

            ((MainWindow)Application.Current.MainWindow).Content = window;
        }
    }
}
