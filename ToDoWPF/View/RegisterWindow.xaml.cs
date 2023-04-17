using System.Linq;
using System.Windows;
using ToDoWPF.AppData;
using ToDoWPF.Model;

namespace ToDoWPF.View
{
    /// <summary>
    /// Логика взаимодействия для RegisterWindow.xaml
    /// </summary>
    public partial class RegisterWindow : Window
    {
        private UsersContext _usersDB = new UsersContext();

        public RegisterWindow()
        {
            InitializeComponent();
        }

        private void CreateAccountButton_Click(object sender, RoutedEventArgs e)
        {
            string login = LoginTextBox.Text;
            string pass = PasswordTextBox.Password;
            string repeatedPassword = RepeatPasswordTextBox.Password;

            if (pass == repeatedPassword)
            {
                User user = new User(login, pass);
                User userFromDB = new User();

                using (UsersContext context = new UsersContext())
                {
                    userFromDB = context.Users.Where(b => b.Login == login).FirstOrDefault();
                }

                if (userFromDB != null)
                {
                    MessageBox.Show("Login is already using!");
                }
                else
                {
                    AuthWindow authWindow = new AuthWindow();

                    _usersDB.Users.Add(user);
                    _usersDB.SaveChanges();

                    MessageBox.Show("Congratulations, you created account, now you need to Log In!");
                    authWindow.Show();
                    Close();
                }
            }

        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            AuthWindow authWindow = new AuthWindow();
            authWindow.Show();
            Close();
        }
    }
}
