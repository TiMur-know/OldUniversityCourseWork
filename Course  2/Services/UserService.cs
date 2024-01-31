using Course__2.Classes.Repos;
using System;
using BC = BCrypt.Net.BCrypt;
namespace Course__2.Classes.Services
{
    public class UserService
    {
        private UserRepos userRepos;
        public UserService()
        {
            userRepos = new UserRepos();
        }

        public User getFromID(int id)
        {
           return userRepos.GetItemFromId(id);
        }
        public bool Login(string login, string password)
        {
            User user = userRepos.GetItemFromLogin(login);

            if (user == null)
            {
                Console.WriteLine("Пользователь не найден");
                return false;
            }
            if (!BC.Verify(password, user.Password))
            {
                Console.WriteLine("Пароль неправльный");
                return false;
            }
            return true;

        }
        public bool add(User user1)
        {
            User user = userRepos.GetItemFromLogin(user1.Login);
            user1.Password = BC.HashPassword(user1.Password);
            
            if (user == null)
            {
                userRepos.AddItem(user1);
                return true;
            }
            else
            {
                Console.WriteLine("Этот логин уже используется");
                return false;
            }
            
        }
        public bool update(User user)
        {
           return  userRepos.UpdateItem(user);
        }
        public bool delete(User user)
        {
          return   userRepos.DeleteItem(user);
        }
    }
}
