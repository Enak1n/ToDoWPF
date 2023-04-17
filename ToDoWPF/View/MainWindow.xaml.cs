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

namespace ToDoWPF.View
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //private UsersContext _usersDB = new UsersContext();

        public MainWindow(User user)
        {
            InitializeComponent();
            CurrentUesr.Text = user.Login;
        }

        private void AddNoteButton_Click(object sender, RoutedEventArgs e)
        {
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


                List<string> notes = new List<string>();
                notes.Add(AddNoteTextBox.Text);
                var connection = new NpgsqlConnection("Host = localhost; Port = 5432; Database = users; Username = postgres; Password = masj109ia4002");
                connection.Open();
                string cmd = "INSERT INTO Users (Notes) VALUES(@Notes)";
                using (var command = new NpgsqlCommand(cmd, connection))
                {
                    command.Parameters.AddWithValue("Notes", notes);

                    command.ExecuteNonQuery();
                }
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
