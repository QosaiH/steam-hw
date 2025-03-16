using Steam_HW1.DAL;

namespace Steam_HW1.Models
{
    public class Userr
    {
        int id;
        string name;
        string email;
        string password;
        bool isActive;

        public Userr() { }

        public Userr(int id, string name, string email, string password, bool isActive)
        {
            this.id = id;
            this.name = name;
            this.email = email;
            this.password = password;
            this.isActive = isActive;
        }

        public static int Insert(Userr userr)
        {
            DBservices dbs = new DBservices();
             return dbs.Insertuser(userr);
        }

        public static int Update(Userr userr)
        {
            DBservices dbs = new DBservices();
            return dbs.UpdateUser(userr);
        }

        public static int Login(string email, string password)
        {
            DBservices dbs = new DBservices();
            return dbs.userLogin(email, password);

        }

public static List<Userr> Read()
        {
            DBservices dbs = new DBservices();
            return dbs.GetUsersList();
        }

        public static int updateIsActive(int id, bool isActive)
        {
            DBservices dbs = new DBservices();
            return dbs.UpdateIsActive(id,isActive);
        }

        public static Object GetUsersInfo()
        {
            DBservices dbs = new DBservices();
            return dbs.GetUsersinfo();
        }

        public int Id { get => id; set => id = value; }
        public string Name { get => name; set => name = value; }
        public string Email { get => email; set => email = value; }
        public string Password { get => password; set => password = value; }

        public bool IsActive { get => isActive; set => isActive = value; }
    }
}
