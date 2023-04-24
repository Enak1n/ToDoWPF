using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using ToDoWPF.Model;
using ToDoWPF.AppData;
using Npgsql;
using System.Linq;
using System.Collections.Generic;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using Wpf.Ui.Appearance;
using Microsoft.EntityFrameworkCore;

namespace ToDoWPF.View
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private UsersContext _usersDB = new UsersContext();
        private List<string> _notes = new List<string>();
        private NpgsqlConnection _connection = new NpgsqlConnection("Host = localhost; Port = 5432;" +
            "Database = users; Username = postgres; Password = masj109ia4002");

        public MainWindow(User user)
        {
            InitializeComponent();
            CurrentUesr.Text = user.Login;

            _connection.Open();

            var cmd = new NpgsqlCommand("Select notes from users where id = @id", _connection);
            cmd.Parameters.AddWithValue("id", user.Id);

            var reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                var array = (string[])reader["notes"];

                _notes.AddRange(array);
            }

            _connection.Close();
            reader.Close();

            foreach (var item in _notes)
            {
                Image editImage = new Image();
                editImage.Source = new BitmapImage(new Uri("/Images/edit_64px.ico", UriKind.Relative));

                Image deleteImage = new Image();
                deleteImage.Source = new BitmapImage(new Uri("/Images/delete_64px.ico", UriKind.Relative));

                TextBlock textBlock = new TextBlock() { FontSize = 20, Text = item, Foreground = Brushes.Black };
                textBlock.FontWeight = FontWeights.SemiBold;
                StackPanel stackPanel = new StackPanel();

                Button editButton = new Button()
                {
                    Content = editImage,
                    Background = Brushes.White,
                    Margin = new Thickness(5, 10, 0, 0),
                };

                editButton.Click += new RoutedEventHandler(EditButton_Click);

                Button deleteButton = new Button()
                {
                    Content = deleteImage,
                    Background = Brushes.White,
                    Margin = new Thickness(5, 10, 0, 0),
                };

                deleteButton.Click += new RoutedEventHandler(DeleteButton_Click);

                CheckBox finishTask = new CheckBox()
                {
                    BorderThickness = new Thickness(3),
                    BorderBrush = Brushes.Black,
                    HorizontalAlignment = HorizontalAlignment.Left,
                    VerticalAlignment = VerticalAlignment.Bottom,
                    Content = "Task is completed"
                };

                Border border = new Border
                {
                    Background = Brushes.White,
                    BorderBrush = Brushes.LightGray,
                    BorderThickness = new Thickness(3),
                    Margin = new Thickness(5, 10, 0, 0),
                    CornerRadius = new CornerRadius(5),
                    HorizontalAlignment = HorizontalAlignment.Left,
                    Width = 700,
                    Height = 40,
                    Child = textBlock
                };


                stackPanel.Children.Add(border);
                stackPanel.Children.Add(finishTask);
                stackPanel.Children.Add(editButton);
                stackPanel.Children.Add(deleteButton);
                NotesGroupBox.Items.Add(stackPanel);
            }
        }

        private void AddNoteButton_Click(object sender, RoutedEventArgs e)
        {
            string title = AddNoteTextBox.Text;
            if (AddNoteTextBox.Text == "" || AddNoteTextBox.Text.Length > 40)
            {
                MessageBox.Show("Length is so long or string is empty!");
            }
            else
            {
                Image editImage = new Image();
                editImage.Source = new BitmapImage(new Uri("/Images/edit_64px.ico", UriKind.Relative));

                Image deleteImage = new Image();
                deleteImage.Source = new BitmapImage(new Uri("/Images/delete_64px.ico", UriKind.Relative));

                TextBlock textBlock = new TextBlock() { FontSize = 20, Text = AddNoteTextBox.Text, Foreground = Brushes.Black };
                textBlock.FontWeight = FontWeights.SemiBold;
                StackPanel stackPanel = new StackPanel();

                Button editButton = new Button()
                {
                    Content = editImage,
                    Background = Brushes.White,
                    Margin = new Thickness(5, 10, 0, 0),
                };

                editButton.Click += new RoutedEventHandler(EditButton_Click);

                Button deleteButton = new Button()
                {
                    Content = deleteImage,
                    Background = Brushes.White,
                    Margin = new Thickness(5, 10, 0, 0),
                };

                deleteButton.Click += new RoutedEventHandler(DeleteButton_Click);

                CheckBox finishTask = new CheckBox()
                {
                    BorderThickness = new Thickness(3),
                    BorderBrush = Brushes.Black,
                    HorizontalAlignment = HorizontalAlignment.Left,
                    VerticalAlignment = VerticalAlignment.Bottom,
                    Content = "Task is completed"
                };

                Border border = new Border
                {
                    Background = Brushes.White,
                    BorderBrush = Brushes.LightGray,
                    BorderThickness = new Thickness(3),
                    Margin = new Thickness(5, 10, 0, 0),
                    CornerRadius = new CornerRadius(5),
                    HorizontalAlignment = HorizontalAlignment.Left,
                    Width = 700,
                    Height = 40,
                    Child = textBlock
                };


                stackPanel.Children.Add(border);
                stackPanel.Children.Add(finishTask);
                stackPanel.Children.Add(editButton);
                stackPanel.Children.Add(deleteButton);
                NotesGroupBox.Items.Add(stackPanel);
                AddNoteTextBox.Clear();


                _connection.Open();
                string cmd = $"UPDATE users SET notes[0] = @note WHERE login = @login";
                NpgsqlCommand command = new NpgsqlCommand(cmd, _connection);
                command.Parameters.AddWithValue("note", title);
                command.Parameters.AddWithValue("login", CurrentUesr.Text);
                command.ExecuteNonQuery();
                _connection.Close();
            }
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Edit");
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Delete");
        }
    }
}
