using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3ISIP_PermiakovaPR8
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("---------МЕНЮ МАГАЗИНА GMWOG---------");
                Console.WriteLine("1. Просмотр товаров магазина");
                Console.WriteLine("2. Регистрация");
                Console.WriteLine("3. Авторизация");
                Console.WriteLine("4. Просмотр доступных ПВЗ");

                Console.WriteLine("Введите номер пункта меню: ");

                int choice = Convert.ToInt32(Console.ReadLine());

                switch (choice)
                {
                    case 1:
                        OutputAllProduct();
                        break;
                    case 2:
                        RegistrationUser();
                        break;
                    case 3:
                        AuthorizationUser();
                        break;
                    case 4:
                        OutputAllPVZ();
                        break;
                    default:
                        Console.WriteLine("Вы ввели хуйню");
                        break;
                }
            }
        }
        static void OutputAllProduct()
        {
            List<Product> products = Core.Context.Product.ToList();

            Console.WriteLine("Наши товары");

            foreach (Product product in products)
            {
                Console.WriteLine($"{product.NameProduct}, {product.Description}, {product.Price}, {product.Count}");
            }

            Console.WriteLine("-------------------------------------------");
        }

        static void RegistrationUser()
        {
            Console.WriteLine("----Регистрация нового пользователя----");

            Console.WriteLine("Введите ваше имя");
            string name = Console.ReadLine();

            Console.WriteLine("Введите вашу фамилию");
            string lastname = Console.ReadLine();

            Console.WriteLine("Введите логин/ник");
            string login = Console.ReadLine();

            Console.WriteLine("Введите пароль");
            string password = Console.ReadLine();

            Console.WriteLine("Проверка пароля");
            string confirmPassword = Console.ReadLine();

            if (name == " " || lastname == " " || login == " " || password == " " || confirmPassword == " ")
            {
                Console.WriteLine("все поля должны быть заполнены");
                return;
            }

            if (password != confirmPassword)
            {
                Console.WriteLine("пароли не совпадают");
                return;
            }

            bool loginProverka = Core.Context.User.Any(u => u.Login == login);
            if (loginProverka)
            {
                Console.WriteLine("пользователь с таким логином уже существует");
                return;
            }

            User newUser = new User 
            { 
                Name = name,
                Lastname = lastname,
                Login = login,
                Password = password
            };

            Core.Context.User.Add(newUser);
            Core.Context.SaveChanges();

            Console.WriteLine("Регистрация прошла успешно! Теперь вы можете авторизоваться.");
            Console.WriteLine("----------------------------------------");
        }

        static void AuthorizationUser()
        {
            Console.WriteLine("----Авторизация пользователя----");

            Console.WriteLine("Введите логин/ник");
            string login = Console.ReadLine();

            Console.WriteLine("Введите пароль");
            string password = Console.ReadLine();

            if (login == " " || password == " ")
            {
                Console.WriteLine("все поля должны быть заполнены");
                return;
            }

            User user = Core.Context.User.FirstOrDefault(u => u.Login == login);

            if (user == null)
            {
                Console.WriteLine("Нет такого пользователя");
                return;
            }

            if (user.Password != password)
            {
                Console.WriteLine("Пароль неправильный");
                return;
            }

            Console.WriteLine($"Авторизация прошла успешно. Привет {user.Name} {user.Lastname}");

        }

        static void OutputAllPVZ()
        {
            List<PVZ> pvz = Core.Context.PVZ.ToList();

            Console.WriteLine("Наши ПВЗ");

            foreach (PVZ pvzs in pvz)
            {
                Console.WriteLine($"{pvzs.NamePVZ}, {pvzs.Adress}");
            }

            Console.WriteLine("----------------------------------------");
        }
    }
}
