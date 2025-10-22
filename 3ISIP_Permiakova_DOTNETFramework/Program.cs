using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3ISIP_Permiakova_DOTNETFramework
{
    class Client
    {

    }
    class Program
    {
        static void Main(string[] args)
        {
            int markup = 1000; 
            Random rand = new Random(); 
            int balance = Convert.ToInt32(rand.Next(2000,40000));
            Console.WriteLine($"Игра запустилась, ваш баланс {balance}");
            Console.WriteLine("Ваш склад: ");
            List<Part> part = CoreFile.Context.Part.ToList();

            foreach (Part part2 in part)
            {
                Console.WriteLine($"{part2.Name}; {part2.Price}; {part2.Count}");
            }
            Console.WriteLine("-------------------------------------------------");
            while (true)
            {
                Console.WriteLine("К вам пришел новый клиент, у него сломалась деталь");
                int inkrement = 0;
                while (true)
                {
                    Random randomID = new Random();
                    int id = randomID.Next(1, part.Count);
                    Console.WriteLine($"Клиент {inkrement++}");
                    Console.WriteLine($"Поломка: {part[id].Name}");
                    Console.WriteLine($"Стоимость ремонта {part[id].Price + markup}");

                    Console.WriteLine("-------------------------------------------------");

                    Console.WriteLine("Выберите действие: ");
                    Console.WriteLine("1. Вывести весь список");
                    Console.WriteLine("2. Согласится на ремонт");
                    Console.WriteLine("3. Не соглашатся на ремонт");
                    Console.WriteLine("4. Докупить детали");

                    Console.WriteLine("-------------------------------------------------");

                    int choise = Convert.ToInt32(Console.ReadLine());
                    switch (choise)
                    {
                        case 1:
                            OutputDB();
                            break;
                        case 2:
                            Agree();
                            break;
                        case 3:
                            Disagree();
                            break;
                        case 4:
                            BuyParts();
                            break;
                        default:
                            Console.WriteLine("Вы ввели несуществующее действие");
                            break;
                    }
                }
                
            }
        }

        static void OutputDB()
        {
            List<Part> part = CoreFile.Context.Part.ToList();

            Console.WriteLine("Ваш склад");

            foreach (Part part2 in part)
            {
                Console.WriteLine($"{part2.Name}; {part2.Price}; {part2.Count}");
            }

            Console.WriteLine("-------------------------------------------------");
        }

        static void Agree()
        {
            Console.WriteLine("Вы согласились на ремонт");
        }

        static void Disagree()
        {

        }

        static void BuyParts()
        {

        }
    }
}
