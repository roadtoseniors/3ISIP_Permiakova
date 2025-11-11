using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace _3ISIP_PermyakovaPR8
{
    class Program
    {
        static void Main(string[] args)
        {
            while(true)
            {
                Console.WriteLine("---------МЕНЮ МАГАЗИНА GMWOG---------");
                Console.WriteLine("1. Просмотр товаров магазина");
                Console.WriteLine("2. Регистрация");
                Console.WriteLine("3. Авторизация");
                Console.WriteLine("4. Просмотр доступных ПВЗ");

                Console.WriteLine("Введите номер пункта меню: ");

                int choice = Convert.ToInt32(Console.ReadLine());

                switch(choice)
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

            foreach(Product product in products)
            {
                Console.WriteLine($"{product.NameProduct}, {product.Description}, {product.Price}, {product.Count}");
            }

            Console.WriteLine("-------------------------------------------");
        }

        static void RegistrationUser()
        {

        }

        static void AuthorizationUser()
        {

        }

        static void OutputAllPVZ()
        {

        }
    }
}
