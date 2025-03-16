using UserRegister.Enums;

namespace UserRegister.Entities
{
    internal class Admins
    {

        public string Username { get; set; }
        public int Password { get; set; }
        public Permission Permission { get; set; }

        public Admins(string username, int password, Permission permission)
        {
            Username = username;
            Password = password;
            Permission = permission;
        }

    }
}
