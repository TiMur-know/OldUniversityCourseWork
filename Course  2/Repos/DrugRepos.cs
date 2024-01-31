
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
namespace Course__2.Classes.Repos
{
    public class DrugRepos : InterfaceDB<Drug>
    {
        private string table;
        private SQLiteConnection sqlite;
        public DrugRepos()
        {
            table = "Drugs";
            sqlite = Program.getDb();
        }
        public bool AddItem(Drug item)
        {
            int number = 0;
            string sql = $"INSERT INTO {table} (Title,PriceBuy,PriceSell,Count,Disease,Recipe,Supplier,ExpiryData,CreatedAt,UpdateAt)" +
                $" VALUES('{item.Title}',{item.PriceBuy},{item.PriceSell},{item.Count},'{item.Disease}','{item.Recipe}','{item.Supplier}','{item.ExpiryData}','{item.CreatedAt}','{item.UpdateAt}');";
            sqlite.Open();
            SQLiteCommand command = new SQLiteCommand(sql, sqlite);
            number = command.ExecuteNonQuery();

            sqlite.Close();
            if (number > 0)
                return true;
            else
                return false;
        }
        public bool UpdateItem(Drug item)
        {
            int number=0;
            string sql = $"UPDATE {table} SET" +
                $" Title='{item.Title}', PriceBuy={item.PriceBuy}, PriceSell={item.PriceSell}, Count={item.Count}, Disease='{item.Disease}', Recipe='{item.Recipe}', Supplier='{item.Supplier}', ExpiryData='{item.ExpiryData}', UpdateAt='{item.UpdateAt}' WHERE Id={item.Id}";

            SQLiteCommand command = new SQLiteCommand(sql, sqlite);
            command.Connection.Open();
            number = command.ExecuteNonQuery();
            if (number > 0)
                return true;
            else
                return false;
        }
        public bool DeleteItem(Drug item)
        {
            int number = 0;
            string sql = $"DELETE FROM {table} WHERE Id={item.Id}";

            SQLiteCommand command = new SQLiteCommand(sql, sqlite);
            command.Connection.Open();
           
            number = command.ExecuteNonQuery();
            command.Connection.Close();
            if (number > 0)
                return true;
            else
                return false;
        }

        public List<Drug> getAll()
        {
            List<Drug> products = new List<Drug>();
            string sql = "SELECT * FROM "+table;
            sqlite.Open();
            SQLiteCommand command = new SQLiteCommand(sql, sqlite);
            using (SQLiteDataReader reader = command.ExecuteReader())
            {

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Drug item = new Drug();
                        item.Id = reader.GetInt32(0);
                        item.Title = reader.GetString(1);
                        item.PriceBuy = reader.GetDouble(2);
                        item.PriceSell = reader.GetDouble(3);
                        item.Count = reader.GetInt32(4);
                        item.Disease = reader.GetString(5);
                        item.Recipe = reader.GetString(6);
                        item.Supplier = reader.GetString(7); 
                        item.ExpiryData = reader.GetString(8);
                        item.CreatedAt = reader.GetString(9);
                        item.UpdateAt = reader.GetString(10);
                        products.Add(item);

                    }

                    reader.Close();
                    sqlite.Close();
                    return products;
                }
                else
                {

                    reader.Close();
                    sqlite.Close();
                    return null;
                }
            }
        }

        public Drug GetItemFromId(int id)
        {
            string sql = $"SELECT * FROM {table} WHERE Id={id}";
            sqlite.Open();
            SQLiteCommand command = new SQLiteCommand(sql, sqlite);
            using (SQLiteDataReader reader = command.ExecuteReader())
            {
                if (reader.HasRows)
                {
                    Drug item = new Drug();
                    while (reader.Read())
                    {
                        item.Id = reader.GetInt32(0);
                        item.Title = reader.GetString(1);
                        item.PriceBuy = reader.GetDouble(2);
                        item.PriceSell = reader.GetDouble(3);
                        item.Count = reader.GetInt32(4);
                        item.Disease = reader.GetString(5);
                        item.Recipe = reader.GetString(6);
                        item.Supplier = reader.GetString(7);
                        item.ExpiryData = reader.GetString(8);
                        item.CreatedAt = reader.GetString(9);
                        item.UpdateAt = reader.GetString(10);
                        command.Dispose();
                        reader.Close();
                        sqlite.Close();
                        break;
                    }
                    return item;
                }
                else
                {
                    reader.Close();
                    sqlite.Close();
                    return null;
                }
            }
        }

        public List<Drug> GetItemsFromParametr(string param, string search)
        {
            List<Drug> products = new List<Drug>();
            string sql="";
            switch (param) { 
                case "title":
                    sql = $"SELECT * FROM {table} WHERE Title={search}";
                    break;
                case "buy":
                    sql = $"SELECT * FROM {table} WHERE PriceBuy={search}";
                    break;
                case "sell":
                    sql = $"SELECT * FROM {table} WHERE PriceSell={search}";
                    break;
                case "count":
                    sql = $"SELECT * FROM {table} WHERE Count={search}";
                    break;
                case "disease":
                    sql = $"SELECT * FROM {table} WHERE Disease={search}";
                    break;
                case "supplier":
                    sql = $"SELECT * FROM {table} WHERE Supplier={search}";
                    break;
                case "expiry":
                    sql = $"SELECT * FROM {table} WHERE ExpiryData={search}";
                    break;
            }
            sqlite.Open();
            SQLiteCommand command = new SQLiteCommand(sql, sqlite);
            using (SQLiteDataReader reader = command.ExecuteReader())
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Drug item = new Drug();
                        item.Id = reader.GetInt32(0);
                        item.Title = reader.GetString(1);
                        item.PriceBuy = reader.GetDouble(2);
                        item.PriceSell = reader.GetDouble(3);
                        item.Count = reader.GetInt32(4);
                        item.Disease = reader.GetString(5);
                        item.Recipe = reader.GetString(6);
                        item.Supplier = reader.GetString(7);
                        item.ExpiryData = reader.GetString(8);
                        item.CreatedAt = reader.GetString(9);
                        item.UpdateAt = reader.GetString(10);
                        products.Add(item);
                    }
                    reader.Close();
                    sqlite.Close();
                    return products;
                }
                else
                {
                    reader.Close();
                    sqlite.Close();
                    return null;
                }
            }
        }
    }
}
