using System.Data.SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Course__2.Classes.Repos
{
    public class UserRepos : InterfaceDB<User>
    {
        private string table;
        private SQLiteConnection sqlite;
        public UserRepos()
        {
            table = "Users";
            sqlite = Program.getDb();
        }
        public bool AddItem(User item)
        {
            int number = 0;
            string sql = $"INSERT INTO {table} (Login,Password,Name,Phone)" +
                $" VALUES('{item.Login}','{item.Password}','{item.Name}','{item.Phone}');";
            sqlite.Open();
            SQLiteCommand command = new SQLiteCommand(sql,sqlite);
            number=command.ExecuteNonQuery();
            
            sqlite.Close();
            if (number > 0)
                return true;
            else
                return false;
            
        }
        public bool UpdateItem(User item)
        {
            int number = 0;
            string sql = $"UPDATE table SET" +
               $" Password='{item.Password}', Name='{item.Name}', Phone='{item.Phone}' WHERE Id={item.Id}";
            sqlite.Open();
            SQLiteCommand command = new SQLiteCommand(sql, sqlite);
            number = command.ExecuteNonQuery();

            sqlite.Close();
            if (number > 0)
                return true;
            else
                return false;
        }
        public bool DeleteItem(User item)
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

        public List<User> getAll()
        {
            List<User> users = new List<User>();
            string sql = $"SELECT * FROM {table}";
            sqlite.Open();
            SQLiteCommand command = new SQLiteCommand(sql, sqlite);
            using(SQLiteDataReader reader = command.ExecuteReader())
            {
                
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        User item = new User();
                        item.Id = reader.GetInt32(0);
                        item.Login = reader.GetString(1);
                        item.Password = reader.GetString(2);
                        item.Phone = reader.GetString(3);
                        users.Add(item);
                        
                    }
                    reader.Close();
                    sqlite.Close();
                    return users;
                }
                else
                {
                    reader.Close();
                    sqlite.Close();
                    return null;
                }
            }
            
            
        }

        public User GetItemFromId(int id)
        {
            string sql = $"SELECT * FROM {table} WHERE Id={id}";
            sqlite.Open();
            SQLiteCommand sqliteCommand = new SQLiteCommand(sql, sqlite);
            using(SQLiteDataReader reader = sqliteCommand.ExecuteReader())
            {
                if (reader.HasRows)
                {
                    User item = new User();
                    while (reader.Read())
                    {

                        item.Id = reader.GetInt32(0);
                        item.Login = reader.GetString(1);
                        item.Password = reader.GetString(2);
                        item.Phone = reader.GetString(3);
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
        public User GetItemFromLogin(string login)
        {
            string sql = $"SELECT * FROM {table} WHERE Login='{login}'";
            sqlite.Open();
            SQLiteCommand sqliteCommand = new SQLiteCommand(sql, sqlite);
            using (SQLiteDataReader reader = sqliteCommand.ExecuteReader())
            {
                if (reader.HasRows)
                {
                    User item = new User();
                    while (reader.Read())
                    {
                        
                        item.Id = reader.GetInt32(0);
                        item.Login = reader.GetString(1);
                        item.Password = reader.GetString(2);
                        item.Phone = reader.GetString(3);
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

        
    }
}
