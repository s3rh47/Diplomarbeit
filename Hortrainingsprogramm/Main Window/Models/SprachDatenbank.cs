using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Hortrainingsprogramm.Main_Window.Models
{
    public class SprachDatenbank
    {

        private SQLiteConnection connection;
  
        public SprachDatenbank()
        {
            connection = new SQLiteConnection("Data Source = SprachDatenbank.sqlite3");
        }


        public LinkedList<string> sqlQuery(string query, string tableName)
        {
            LinkedList<string> ergebnisList = new();

            connection.Open();

            SQLiteCommand command = new SQLiteCommand(query, connection);

            SQLiteDataReader reader = command.ExecuteReader();
           

            if (reader.HasRows)
            {

                ergebnisList.Clear();

                while (reader.Read())
                {

                    ergebnisList.AddLast(reader[tableName].ToString());

                }
            }

            connection.Close();

            return ergebnisList;

        }
    }
}
