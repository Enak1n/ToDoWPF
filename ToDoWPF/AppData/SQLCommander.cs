using Npgsql;
using System;
using System.Configuration;
using ToDoWPF.Model;
using System.Collections.Generic;

namespace ToDoWPF.AppData
{
    public static class SQLCommander
    {
        static private string _connectionString = ConfigurationManager.ConnectionStrings["NotesDB"].ConnectionString;
        static private NpgsqlConnection _connection = new NpgsqlConnection(_connectionString);
        static private List<string> _notes = new List<string>();

        public static List<string> SelectCommand(string login, string cmd)
        {
            _connection.Open();
            NpgsqlCommand command = new NpgsqlCommand(cmd, _connection);
            command.Parameters.AddWithValue("login", login);

            var reader = command.ExecuteReader();
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

        public static void UpdateCommand(string editedNote, string login, string cmd, int index)
        {
            _connection.Open();

            NpgsqlCommand command = new NpgsqlCommand(cmd, _connection);

            command.Parameters.AddWithValue("note", editedNote);
            command.Parameters.AddWithValue("login", login);
            command.Parameters.AddWithValue("index", index);
            command.ExecuteNonQuery();

            _connection.Close();
        }
    }
}
