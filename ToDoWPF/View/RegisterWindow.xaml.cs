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

            if(pass.Length > 8 && pass == repeatedPassword)
            {
                User user = new User(login, pass);

                _usersDB.Users.Add(user);
                _usersDB.SaveChanges();
            }

        }
    }
}
