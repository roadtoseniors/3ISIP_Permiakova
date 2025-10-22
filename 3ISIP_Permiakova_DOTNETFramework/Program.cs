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

            Random rand = new Random(); 
            int balance = Convert.ToInt32(rand.Next(2000,40000));
            while (true)
            {
                Console.WriteLine();

                int choise = Convert.ToInt32(Console.ReadLine());
                switch (choise)
                {
                    case 1:
                        Console.WriteLine(OutputDB());
                        break;
                    case 2:
                        Console.WriteLine(Agree());
                        break;
                    case 3:
                        Console.WriteLine(Disagree());
                        break;
                    case 4:
                        Console.WriteLine(BuyParts());
                        break;
                    default:
                        Console.WriteLine("Закончить игру");
                        break;
                }
            }
        }

        static void OutputDB()
        {
            List<Part> part = CoreFile.Context.Part.ToList();

            foreach (Part part2 in part)
            {
                Console.WriteLine(part2);
            }
        }

        static void Agree()
        {

        }

        static void Disagree()
        {

        }

        static void BuyParts()
        {

        }
    }
}
