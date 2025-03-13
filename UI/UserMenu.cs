using UserRegister.Entities;
using UserRegister.Repositories;

namespace UserRegister.UI
{
    public class UserMenu
    {

        private UserRepository _userRepository;

        public UserMenu()
        {
            _userRepository = new UserRepository();
        }

        public void MainMenu()
        {
            var menuRunning = true;
            while (menuRunning)
            {
                Console.WriteLine("Selecione a opção desejada: ");
                Console.WriteLine("1 - Criar usuário / 2 - Obter usuário / 3 - Sair");
                if (!int.TryParse(Console.ReadLine(), out int userChoice))
                {
                    Console.WriteLine("Entrada inválida. Por favor, digite um número.");
                    continue;
                }

                switch (userChoice)
                {
                    case 1:
                        CreateUserMenu();
                        break;
                    case 2:
                        GetUserMenu();
                        break;
                    case 3:
                        menuRunning = false;
                        break;
                    default:
                        Console.WriteLine("Opção inválida, tente novamente.");
                        break;

                }

            }
        }

        public void CreateUserMenu()
        {
            Console.WriteLine("Menu de criação de usuário: ");
            Console.Write("Digite o nome do usuário: ");
            var name = Console.ReadLine();

            Console.Write("Digite o ID do usuário: ");
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("ID inválido. O usuário não foi criado.");
                return;
            }

            var user = new User
            {
                Id = id,
                Name = name
            };

            _userRepository.CreateNewUser(user);
            Console.WriteLine("Usuário criado com sucesso");

        }

        public void GetUserMenu()
        {
            Console.WriteLine("Obter usuário");
            Console.Write("Digite o ID do usuário: ");
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("ID inválido.");
                return;
            }
            var user = _userRepository.GetUser(id);
            if (user == null)
            {
                Console.WriteLine("Usuário não encontrado.");
                return;
            }

            Console.WriteLine($"Usuário {user.Name}");
        }

    }
}