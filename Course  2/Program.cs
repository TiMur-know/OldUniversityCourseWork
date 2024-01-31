using System;
using System.Data.SQLite;

namespace Course__2
{
    public class Program
    {
        static SQLiteConnection sqliteConnection;
        public static SQLiteConnection getDb()
        {
            return sqliteConnection;
        }
        private static void connectDB()
        {
            string connectionString = "Data Source=drugesdata.db";
            sqliteConnection = new SQLiteConnection(connectionString);
            createTable(sqliteConnection);
        }
        private static void createTable(SQLiteConnection sqliteConnection)
        {
            string createTUsers = "CREATE TABLE IF NOT EXISTS Users(" +
                "Id INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT UNIQUE," +
                "Login TEXT NOT NULL UNIQUE," +
                "Password TEXT NOT NULL," +
                "Name TEXT NOT NULL," +
                "Phone TEXT NOT NULL" +
                ")";
            string createTDrugs = "CREATE TABLE IF NOT EXISTS Drugs(" +
                "Id INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT UNIQUE," +
                "Title TEXT NOT NULL," +
                "PriceBuy REAL NOT NULL," +
                "PriceSell REAL NOT NULL," +
                "Count INTEGER NOT NULL," +
                "Disease TEXT NOT NULL," +
                "Recipe TEXT NOT NULL," +
                "Supplier TEXT NOT NULL," +
                "ExpiryData TEXT NOT NULL," +
                "CreatedAt TEXT NOT NULL," +
                "UpdateAt TEXT NOT NULL" +
                ")";
            string createTDrugsSell = "CREATE TABLE IF NOT EXISTS DrugsSell(" +
                "Id INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT UNIQUE," +
                "Title TEXT NOT NULL," +
                "PriceBuy REAL NOT NULL," +
                "PriceSell REAL NOT NULL," +
                "Count INTEGER NOT NULL," +
                "Disease TEXT NOT NULL," +
                "Supplier TEXT NOT NULL," +
                "SellAt TEXT NOT NULL" +
                ")";
            sqliteConnection.Open();
            SQLiteCommand sqliteCommand1 = new SQLiteCommand(createTUsers, sqliteConnection);
            SQLiteCommand sqliteCommand2 = new SQLiteCommand(createTDrugs, sqliteConnection);
            SQLiteCommand sqliteCommand3 = new SQLiteCommand(createTDrugsSell, sqliteConnection);
            sqliteCommand1.ExecuteNonQuery();
            sqliteCommand2.ExecuteNonQuery();
            sqliteCommand3.ExecuteNonQuery();
            sqliteConnection.Close();
        }
        static void Main(string[] args)
        {
            connectDB();
            WorkClass workClass = new WorkClass();
            workClass.AuthorizeMeny();
            Console.Read();
        }
    }
}
