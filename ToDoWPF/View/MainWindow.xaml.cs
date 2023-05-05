using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using ToDoWPF.Model;
using ToDoWPF.AppData;
using System.Collections.Generic;

namespace ToDoWPF.View
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private List<string> _notes = new List<string>();
        private List<StackPanel> _stackPanels = new List<StackPanel>();
        private string? _title;

        public MainWindow(User user)
        {
            InitializeComponent();
            CurrentUser.Text = user.Login;

            string cmd = "Select notes from users where login = @login";
            _notes = SQLCommander.SelectCommand(CurrentUser.Text, cmd);

            foreach (var item in _notes)
            {
                CreateNote(item);
            }
        }

        private void AddNoteButton_Click(object sender, RoutedEventArgs e)
        {
            string cmd;
            _title = AddNoteTextBox.Text;
            if (AddNoteTextBox.Text == "" || AddNoteTextBox.Text.Length > 40)
            {
                MessageBox.Show("Length is so long or string is empty!");
            }
            else
            {
                CreateNote(AddNoteTextBox.Text);

                if (_notes.Count > 0)
                {
                    cmd = "UPDATE users SET notes[array_length(notes, 1)] = @note WHERE login = @login";
                }
                else
                {
                    cmd = " UPDATE users SET notes[0] = @note WHERE login = @login";
                }

                _notes.Add(_title);
                SQLCommander.UpdateCommand(_title, CurrentUser.Text, cmd);
            }
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            if (NotesGroupBox.SelectedIndex >= 0)
            {
                _title = _notes[NotesGroupBox.SelectedIndex];
                EditNoteWindow editNoteWindow = new EditNoteWindow(_notes, CurrentUser.Text, _title);
                editNoteWindow.ShowDialog();

                NotesGroupBox.Items.Clear();
                _notes.Clear();
                string cmd = "SELECT notes From users where login = @login";
                _notes = SQLCommander.SelectCommand(CurrentUser.Text, cmd);

                foreach (var item in _notes)
                {
                    CreateNote(item);
                }
            }
            else
            {
                MessageBox.Show("Choose your notes!");
            }
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (NotesGroupBox.SelectedIndex >= 0)
            {
                var currentStackPanel = _stackPanels[NotesGroupBox.SelectedIndex];
                Border currentBorder = (Border)currentStackPanel.Children[0];
                TextBlock title = (TextBlock)currentBorder.Child;

                string cmd = "UPDATE users SET notes = array_remove(notes, @note ) WHERE login = @login";

                SQLCommander.UpdateCommand(title.Text, CurrentUser.Text, cmd);

                NotesGroupBox.Items.Remove(currentStackPanel);
                _stackPanels.Remove(currentStackPanel);
                _notes.Remove(title.Text);
            }
            else
            {
                MessageBox.Show("Choose your notes!");
            }
        }

        private void CreateNote(string title)
        {
            Image editImage = new Image();
            editImage.Source = new BitmapImage(new Uri("/Images/edit_64px.ico", UriKind.Relative));

            Image deleteImage = new Image();
            deleteImage.Source = new BitmapImage(new Uri("/Images/delete_64px.ico", UriKind.Relative));

            TextBlock textBlock = new TextBlock() { FontSize = 20, Text = title, Foreground = Brushes.Black };
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
            stackPanel.Children.Add(editButton);
            stackPanel.Children.Add(deleteButton);
            NotesGroupBox.Items.Add(stackPanel);
            _stackPanels.Add(stackPanel);
        }
    }
}
