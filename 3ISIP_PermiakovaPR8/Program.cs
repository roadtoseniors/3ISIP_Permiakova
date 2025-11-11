using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

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

            UserMenu(user);
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

        static void UserMenu(User user)
        {
            Console.WriteLine("---Личный кабинет----");
            while (true) 
            {
                Console.WriteLine("1. Просмотр товаров");
                Console.WriteLine("2. Добавить товар в корзину");
                Console.WriteLine("3. Просмотр корзины");
                Console.WriteLine("4. Оформить заказ");
                Console.WriteLine("5. История заказов");
                Console.WriteLine("6. Выйти из аккаунта");

                int choice = Convert.ToInt32(Console.ReadLine());

                switch(choice)
                {
                    case 1:
                        OutputAllProduct();
                        break;
                    case 2:
                        AddProductToBasket(user);
                        break;
                    case 3:
                        ShowBasket(user);
                        break;
                    case 4:
                        OformitZakaz(user);
                        break;
                    case 5:
                        HistoryZakazov(user);
                        break;
                    case 6:
                        Console.WriteLine("выход из аккаунта пакаааааа");
                        return;
                    default:
                        Console.WriteLine("Неверный пункт меню! Попробуйте снова.");
                        break;
                }
            }
        }

        static void AddProductToBasket(User user)
        {
            Console.WriteLine("----- ДОБАВЛЕНИЕ ТОВАРА В КОРЗИНУ -----");

            OutputAllProduct();

            Console.Write("Введите ID товара для добавления в корзину: ");
            int productId = Convert.ToInt32(Console.ReadLine());

            Console.Write("Введите количество: ");
            int quantity = Convert.ToInt32(Console.ReadLine());

            Product product = Core.Context.Product.FirstOrDefault(p => p.ID == productId);
            if (product == null)
            {
                Console.WriteLine("Товар с таким ID не найден!");
                return;
            }

            if (quantity > product.Count)
            {
                Console.WriteLine($"Недостаточно товара на складе! Доступно: {product.Count}");
                return;
            }

            if (quantity <= 0)
            {
                Console.WriteLine("Количество должно быть больше 0!");
                return;
            }

            Basket1 existingBasketItem = Core.Context.Basket
                .FirstOrDefault(b => b.User_ID == user.ID && b.Product_ID == productId);

            if (existingBasketItem != null)
            {
                existingBasketItem.Count += quantity;
                Console.WriteLine($"Количество товара '{product.NameProduct}' в корзине увеличено!");
            }
            else
            {
                Basket1 newBasketItem = new Basket1
                {
                    User_ID = user.ID,
                    Product_ID = productId,
                    Count = quantity
                };
                Core.Context.Basket.Add(newBasketItem);
                Console.WriteLine($"Товар '{product.NameProduct}' добавлен в корзину!");
            }

            Core.Context.SaveChanges();
            Console.WriteLine("----------------------------------------");
        }

        static void ShowBasket(User user)
        {
            List<Basket1> baskets = Core.Context.Basket
                .Include(b => b.Product)
                .Where(b => b.User_ID == user.ID)
                .ToList();

            Console.WriteLine("----- ВАША КОРЗИНА -----");

            if (!baskets.Any())
            {
                Console.WriteLine("Корзина пуста");
                Console.WriteLine("----------------------------------------");
                return;
            }

            decimal totalPrice = 0;
            int itemNumber = 1;

            foreach (Basket1 basket in baskets)
            {
                Console.WriteLine($"{itemNumber}. {basket.Product.NameProduct}");
                Console.WriteLine($"   Описание: {basket.Product.Description}");
                Console.WriteLine($"   Цена: {basket.Product.Price} руб.");
                Console.WriteLine($"   Количество: {basket.Count}");
                Console.WriteLine($"   Сумма: {basket.Product.Price * basket.Count} руб.");
                Console.WriteLine();

                totalPrice += basket.Product.Price * basket.Count;
                itemNumber++;
            }

            Console.WriteLine($"ОБЩАЯ СУММА: {totalPrice} руб.");
            Console.WriteLine("----------------------------------------");
        }

        static void OformitZakaz(User user)
        {
            Console.WriteLine("оформление заказа");
            ShowBasket(user);
            while (true)
            {
                Console.WriteLine("1. Купить один товар");
                Console.WriteLine("2. Купить всю корзину");
                Console.WriteLine("3. Вернуться назад");
                int chice = Convert.ToInt32( Console.ReadLine());

                switch(chice)
                {
                    case 1:
                        BuySingleProduct(user);
                        break;
                    case 2:
                        BuyWholeBasket(user);
                        break;
                    case 3:
                        return;
                    default:
                        Console.WriteLine("Неверный пункт меню!");
                        break;
                }
            }
        }

        static void HistoryZakazov(User user)
        {
            Console.WriteLine("----история заказов----");


        }

        static void BuySingleProduct(User user)
        {

        }

        static void BuyWholeBasket(User user)
        {

        }
    }
}
