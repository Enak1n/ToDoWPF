using System.Windows;
using System.Collections.Generic;
using ToDoWPF.AppData;

namespace ToDoWPF.View
{
    /// <summary>
    /// Логика взаимодействия для EditNoteWindow.xaml
    /// </summary>
    public partial class EditNoteWindow : Window
    {
        private List<string> _notes = new List<string>();
        private string _login;
        private string _selectedNote;

        public EditNoteWindow(List<string> notes, string login, string note)
        {
            InitializeComponent();
            _notes = notes;
            _login = login;
            _selectedNote = note;
        }

        private void ApplyButton_Click(object sender, RoutedEventArgs e)
        {
            string editedNote = EditTextBox.Text;
            int currentNoteIndex = _notes.FindIndex(x => x == _selectedNote);

            string cmd = $"Update users SET notes[@index] = @note Where login = @login";

            SQLCommander.UpdateCommand(editedNote, _login, cmd, currentNoteIndex);

            Close();
        }
    }
}
