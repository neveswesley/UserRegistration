using UserRegister.Entities;

namespace UserRegister.Repositories
{
    internal class UserRepository : IRepository
    {

        public int Id { get; set; }
        public string Name { get; set; }

        List<User> _users;

        public UserRepository(List<User> users)
        {
            _users = users;
        }
        public void CreateNewUser(User users)
        {
            _users.Add(users);
        }

        public void UpdateUser(User newUserModel, int id)
        {
            var userToUpdate = _users.Where(x => x.Id == id).First();
            _users.Remove(userToUpdate);

            userToUpdate.Name = newUserModel.Name;
            _users.Add(userToUpdate);
        }

        public User GetUser(int requestedId)
        {
            var user = _users.Where(x => x.Id == requestedId).FirstOrDefault();
            return user;
        }

        public void DeleteUser(int requestedId)
        {
            var user = _users.Where(x => x.Id == requestedId).FirstOrDefault();
            _users.Remove(user);
        }

        public List<User> GetAllUsers()
        {
            return _users;
        }

    }

}