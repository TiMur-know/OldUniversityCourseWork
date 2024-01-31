using Course__2.Classes.Models;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Course__2.Classes.Repos
{
    public class DrugSellRepos:InterfaceDB<DrugSell>
    {
        private string table;
        private SQLiteConnection sqlite;
        public DrugSellRepos()
        {
            table = "DrugsSell";
            sqlite = Program.getDb();
        }
        public bool AddItem(DrugSell item)
        {
            int number = 0;
            string sql = $"INSERT INTO {table} (Title,PriceBuy,PriceSell,Count,Disease,Supplier,SellAt)" +
                $" VALUES('{item.Title}',{item.PriceBuy},{item.PriceSell},{item.Count},'{item.Disease}','{item.Supplier}','{item.SellAt}');";
            sqlite.Open();
            SQLiteCommand command = new SQLiteCommand(sql, sqlite);
            number = command.ExecuteNonQuery();

            sqlite.Close();
            if (number > 0)
                return true;
            else
                return false;
        }
        public bool UpdateItem(DrugSell item)
        {
            int number = 0;
            string sql = $"UPDATE table SET" +
                $" Count={item.Count},SellAt='{item.SellAt}' WHERE Id={item.Id}";
            sqlite.Open();
            SQLiteCommand command = new SQLiteCommand(sql, sqlite);
            number = command.ExecuteNonQuery();

            sqlite.Close();
            if (number > 0)
                return true;
            else
                return false;
        }
        public bool CheckItem(DrugSell item)
        {
            string sql = $"SELECT * FROM {table} WHERE Id={item.Id} AND Title='{item.Title}' AND PriceSell={item.PriceSell} AND PriceBuy={item.PriceBuy} AND Supplier='{item.Supplier}'" ;
            sqlite.Open();
            SQLiteCommand sqliteCommand = new SQLiteCommand(sql, sqlite);
            using (SQLiteDataReader reader = sqliteCommand.ExecuteReader())
            {
                if (reader.HasRows)
                {
                    reader.Close();
                    sqlite.Close();
                    return true;
                }
                else
                {
                    reader.Close();
                    sqlite.Close();
                    return false;
                }
            }
        }
        public List<DrugSell> getAll()
        {
            List<DrugSell> products = new List<DrugSell>();
            string sql = "SELECT * FROM " + table;
            sqlite.Open();
            SQLiteCommand command = new SQLiteCommand(sql, sqlite);
            using (SQLiteDataReader reader = command.ExecuteReader())
            {

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        DrugSell item = new DrugSell();
                        item.Id = reader.GetInt32(0);
                        item.Title = reader.GetString(1);
                        item.PriceBuy = reader.GetDouble(2);
                        item.PriceSell = reader.GetDouble(3);
                        item.Count = reader.GetInt32(4);
                        item.Disease = reader.GetString(5);
                        item.Supplier = reader.GetString(6);
                        item.SellAt = reader.GetString(7);
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

        public DrugSell GetItemFromId(int id)
        {
            string sql = $"SELECT * FROM {table} WHERE Id={id}";
            sqlite.Open();
            SQLiteCommand command = new SQLiteCommand(sql, sqlite);
            using (SQLiteDataReader reader = command.ExecuteReader())
            {
                if (reader.HasRows)
                {
                    DrugSell item = new DrugSell();
                    while (reader.Read())
                    {
                        item.Id = reader.GetInt32(0);
                        item.Title = reader.GetString(1);
                        item.PriceBuy = reader.GetDouble(2);
                        item.PriceSell = reader.GetDouble(3);
                        item.Count = reader.GetInt32(4);
                        item.Disease = reader.GetString(5);
                        item.Supplier = reader.GetString(6);
                        item.SellAt = reader.GetString(7);
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
        public bool DeleteItem(DrugSell item)
        {
            int number = 0;
            string sql = $"DELETE FROM {table} WHERE Id={item.Id}";
            sqlite.Open();
            SQLiteCommand command = new SQLiteCommand(sql, sqlite);
            number = command.ExecuteNonQuery();
            sqlite.Close();
            if (number > 0)
                return true;
            else
                return false;
        }
    }
}
