using Npgsql;
using System;
using System.Configuration;
using System.Windows.Documents;
using ToDoWPF.Model;
using System.Collections.Generic;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace ToDoWPF.AppData
{
    public static class SQLCommander
    {
        static private string _connectionString = ConfigurationManager.ConnectionStrings["NotesDB"].ConnectionString;
        static NpgsqlConnection _connection = new NpgsqlConnection(_connectionString);
        static private List<string> _notes = new List<string>();

        public static List<string> SelectCommand(User user)
        {
            _connection.Open();
            var cmd = new NpgsqlCommand("Select notes from users where id = @id", _connection);
            cmd.Parameters.AddWithValue("id", user.Id);

            var reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                var array = reader["notes"];

                if (array is DBNull)
                {
                    _connection.Close();
                    reader.Close();
                }
                else
                {
                    _notes.AddRange((string[])array);
                }
            }

            _connection.Close();
            reader.Close();
            return _notes;
        }

        public static void UpdateCommand(string title, string login, string cmd)
        {
            _connection.Open();

            NpgsqlCommand command = new NpgsqlCommand(cmd, _connection);

            command.Parameters.AddWithValue("note", title);
            command.Parameters.AddWithValue("login", login);
            command.ExecuteNonQuery();

            _connection.Close();
        }


    }
}
