using UserRegister.Entities;

namespace UserRegister.Repositories
{
    internal class UserRepository
    {

        public List<User> Users = new List<User>();

        public void CreateNewUser(User user)
        {
            Users.Add(user);
        }

        public void UpdateUser(User newUserModel, int id)
        {
            var userToUpdate = Users.Where(x => x.Id == id).First();
            Users.Remove(userToUpdate);

            userToUpdate.Name = newUserModel.Name;
            Users.Add(userToUpdate);
        }

        public User GetUser(int requestedId)
        {
            var user = Users.Where(x => x.Id == requestedId).FirstOrDefault();
            return user;
        }

        public void DeleteUser(User user)
        {
            Users.Remove(user);
        }

    }
}
