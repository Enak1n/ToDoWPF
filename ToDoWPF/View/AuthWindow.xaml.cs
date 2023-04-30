using System.Windows;
using ToDoWPF.View;
using System.Linq;
using ToDoWPF.Model;
using ToDoWPF.AppData;

namespace ToDoWPF
{
    /// <summary>
    /// Interaction logic for AuthWindow.xaml
    /// </summary>
    public partial class AuthWindow : Window
    {
        private UsersContext _usersDB = new UsersContext();

        public AuthWindow()
        {
            InitializeComponent();
        }

        private void CreateAccountButton_Click(object sender, RoutedEventArgs e)
        {
            RegisterWindow registerWindow = new RegisterWindow();
            registerWindow.Show();
            Close();
        }

        private void LogInButton_Click(object sender, RoutedEventArgs e)
        {
            string login = LoginTextBox.Text;
            string pass = PasswordTextBox.Password;

            User user = new User(login, pass);
            user = _usersDB.Users.Where(b => b.Login == login && b.Pass == pass).FirstOrDefault();

            if (user != null)
            {
                MainWindow mainWindow = new MainWindow(user);
                mainWindow.Show();
                Close();
            }
            else
            {
                MessageBox.Show("Login is not found!");
            }
        }
    }
}
