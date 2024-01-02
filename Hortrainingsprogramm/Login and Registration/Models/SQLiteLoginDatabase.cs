using System.Data.SQLite;

namespace Hortrainingsprogramm.Login_and_Registration.Models
{
    // Klasse für Login DATABASE.. 
    public class SQLiteLoginDatabase

    {
        
        private SQLiteConnection connection;
        private string username;
       

        //Konstruktur
        public SQLiteLoginDatabase()
        {

            connection = new SQLiteConnection("Data Source = LoginDatabase.sqlite3;New=True") ;
            CreateTableForUser();

        }


        /// <summary>
        /// Create Table
        /// </summary>

        public void CreateTableForUser()
        {

            openConnection();

            string query = "CREATE TABLE if not exists UserTabelle (UserID INTEGER,UserName TEXT,PRIMARY KEY(UserID AUTOINCREMENT))";
            SQLiteCommand command = new SQLiteCommand(query, connection);
            command.ExecuteNonQuery();
            closeConnection();

        }



        /// <summary>
        /// Insert in To
        /// </summary>

        public void insertInToForUserTabelle(string username)
        {

            this.username = username;


            openConnection();

            string query = "INSERT INTO UserTabelle(UserName) Values(@name)";

            SQLiteCommand command = new SQLiteCommand(query, connection);

            command.Parameters.AddWithValue("@name", this.username);
            command.ExecuteNonQuery();
            closeConnection();

        }



        //public LinkedList<string> selectNameFromUserTabelle()
        //{

        //    openConnection();
        //    string query = "SELECT * from UserTabelle";
        //    SQLiteCommand command = new SQLiteCommand(query, connection);
        //    SQLiteDataReader reader = command.ExecuteReader();


        //    if (reader.HasRows)
        //    {

        //        userNameLinkedList.Clear();

        //        while (reader.Read())
        //        {

        //            userNameLinkedList.AddLast(reader["UserName"].ToString());

        //        }
        //    }
        //    closeConnection();
        //    return userNameLinkedList;
        //}


        public object selectNameFromUserTabelle(string username)
        {
            this.username = username;

            openConnection();
            string query = "Select * from UserTabelle WHERE UserName = @name;";
            SQLiteCommand command = new SQLiteCommand(query, connection);

            command.Parameters.AddWithValue("@name", this.username);

            var result = command.ExecuteScalar();

            closeConnection();
            return result;

        }


        public void openConnection()
        {

            if (connection.State != System.Data.ConnectionState.Open)
                connection.Open();
        }

        public void closeConnection()
        {

            if (connection.State != System.Data.ConnectionState.Closed)
                connection.Close();
        }

    }
}
