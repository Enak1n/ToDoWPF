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

namespace ToDoWPF.View
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void AddNoteButton_Click(object sender, RoutedEventArgs e)
        {
            TextBlock textBlock = new TextBlock() { FontSize = 20, Text = AddNoteTextBox.Text, Foreground=Brushes.Black};
            textBlock.FontWeight = FontWeights.SemiBold;
            StackPanel stackPanel = new StackPanel();

            Border border = new Border
            {
                Background = Brushes.White,
                BorderBrush = Brushes.LightGray,
                BorderThickness = new Thickness(3),
                Margin = new Thickness(5, 10, 0, 0),
                CornerRadius = new CornerRadius(5),
                HorizontalAlignment = HorizontalAlignment.Left,
                Width = 350,
                Height = 40,
                Child = textBlock
            };

            border.Child = textBlock;
            stackPanel.Children.Add(border);
            NotesGroupBox.Items.Add(stackPanel);
            AddNoteTextBox.Clear();
        }
    }
}
