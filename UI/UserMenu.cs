﻿using UserRegister.Entities;
using UserRegister.Repositories;
using UserRegister.Enums;

namespace UserRegister.UI
{
    public class UserMenu
    {

        private UserRepository _userRepository;

        public UserMenu()
        {
            _userRepository = new UserRepository(new List<User>());
        }

        static List<Admins> administradores = new List<Admins>
        {
        new Admins("welly", 1234, Permission.Admin),
        new Admins("luan", 4321, Permission.Creator)
        };


        static Admins userLogged = null;


        public void LoginMenu()
        {

            while (true)
            {
                Console.Write("Digite o nome de usuário: @");
                var userName = Console.ReadLine();

                int password;

                while (true)
                {
                    Console.Write("Digite a senha: ");
                    var input = Console.ReadLine();

                    if (!int.TryParse(input, out password))
                    {
                        Console.WriteLine("Entrada inválida. A senha só pode conter números");
                        Console.WriteLine();
                        continue;
                    }
                    break;
                }

                userLogged = administradores.Find(u => u.Username == userName && u.Password == password);

                if (userLogged != null)
                {
                    Console.WriteLine($"Bem vindo, {userLogged.Username}!");
                    Console.WriteLine();
                    MainMenu();
                    return;
                }

                Console.WriteLine("Usuário ou senha incorretos. Tente novamente.");
                Console.WriteLine();

            }
        }

        public void MainMenu()
        {
            var menuRunning = true;

            while (menuRunning)
            {
                Console.WriteLine("Selecione a opção desejada: ");
                Console.WriteLine("1 - Criar usuário / 2 - Obter usuário / 3 - Atualizar usuário / 4 - Apagar usuário / 5 - Mostrar todos os usuários / 0 - Sair");
                if (!int.TryParse(Console.ReadLine(), out int userChoice))
                {
                    Console.WriteLine("Entrada inválida. Por favor, digite um número.");
                    continue;
                }

                switch (userChoice)
                {
                    case 1: CreateUserMenu(); break;
                    case 2: GetUserMenu(); break;
                    case 3: UpdateUser(); break;
                    case 4: DeleteUser(); break;
                    case 5: ShowAllUsers(); break;
                    case 0: menuRunning = false; break;
                    default: Console.WriteLine("Opção inválida, tente novamente."); break;
                }
            }
        }


        public void CreateUserMenu()
        {
            Console.WriteLine("Menu de criação de usuário: ");
            Console.Write("Digite o nome do usuário: @");
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
            Console.WriteLine();
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

            Console.WriteLine($"Usuário: @{user.Name}");
            Console.WriteLine();
        }

        public void UpdateUser()
        {

            if (userLogged.Permission != Permission.Admin)
            {
                Console.WriteLine("Você não tem permissão para atualizar usuários!");
                Console.WriteLine();
                return;
            }

            Console.WriteLine("Atualizar usuário");
            int id;
            var user = (User)null;

            do
            {
                Console.WriteLine("Digite o ID do usuário: ");
                if (!int.TryParse(Console.ReadLine(), out id))
                {
                    Console.WriteLine("ID inválido. Tente novamente.");
                    continue;
                }

                user = _userRepository.GetUser(id);

                if (user == null)
                {
                    Console.WriteLine("Usuário não encontrado. Tente novamente.");
                }

            } 
            
            while (user == null);
                {
                    Console.WriteLine("Qual informação você gostaria de atualizar? (1 - Nome / 2 - ID / 3 - Ambos)");

                    if (!int.TryParse(Console.ReadLine(), out int userChoice))
                    {
                        Console.WriteLine("Escolha inválida.");
                        return;
                    }


                    if (userChoice == 1 || userChoice == 3)
                    {
                        Console.WriteLine("Novo nome de usuário: ");
                        var newUsername = Console.ReadLine();

                        if (string.IsNullOrWhiteSpace(newUsername))
                        {
                            Console.WriteLine("Nome inválido.");
                            return;
                        }

                        user.Name = newUsername;
                        Console.WriteLine("Nome atualizado com sucesso!");
                    }

                    if (userChoice == 2 || userChoice == 3)
                    {
                        Console.WriteLine("Novo ID: ");
                        if (!int.TryParse(Console.ReadLine(), out int newID))
                        {
                            Console.WriteLine("ID inválido. Tente novamenete: ");
                            return;

                        }

                        user.Id = newID;

                    }

                }
            }


        public void DeleteUser()
        {

            if (userLogged.Permission != Permission.Admin)
            {
                Console.WriteLine("Você não tem permissão para atualizar usuários!");
                Console.WriteLine();
                return;
            }

            Console.Write("Digite o ID do usuário que será excluído: ");
            if (!int.TryParse(Console.ReadLine(), out int Id))
            {
                Console.WriteLine("ID inválido.");
                return;
            }

            var user = _userRepository.GetUser(Id);
            if (user == null)
            {
                Console.WriteLine("Usuário não encontrado.");
                return;
            }

            _userRepository.DeleteUser(Id);
            Console.WriteLine("Usuário apagado com sucesso!");

        }

        public void ShowAllUsers()
        {
            var users = _userRepository.GetAllUsers();

            if (users == null || users.Count == 0)
            {
                Console.WriteLine("Nenhum usuário cadastrado."); return;
            }

            Console.WriteLine();
            Console.WriteLine("Lista de Usuários: ");
            foreach (var user in users)
            {
                Console.WriteLine($"ID: {user.Id} | Nome: {user.Name}");
            }
            Console.WriteLine();
        }

    }
}