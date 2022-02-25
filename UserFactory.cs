using System.Collections.Generic;

namespace ChatFirePoupulator
{
    public class UserFactory
    {
        public List<User> Users;
        public static bool Published {get; set;}

        public UserFactory()
        {
            Users = new List<User>();
            Published = false;
        }
        public User GetDocument()
        {
            User usr = new User();
            Users.Add(usr);
            return usr;
        }

        public List<User> GetActiveUsers()
        {
            return Users;
        }
    }
}