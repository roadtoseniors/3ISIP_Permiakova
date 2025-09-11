using System.Buffers;

//Console.WriteLine("-----консольное приложение для подсчета потраченных за день средств-----");
//Console.WriteLine("Введите количество операций за день");
//int count = Convert.ToInt32(Console.ReadLine());
//for (int i = 0; i <= count; i++)
//{
//    Console.WriteLine($"Операция номер {i}");
//    Console.WriteLine("Введите название траты по шаблону");
//    Console.WriteLine("Название услуги или товара; Количество денег (рубли)");
    
//}

//Console.WriteLine("-----Меню-----");
//Console.WriteLine("1.Вывод данных");
//Console.WriteLine("2.Статистика (среднее, максимальное, минимальное, сумма)");
//Console.WriteLine("3.Сортировка по цене (пузырьковая сортировка)");
//Console.WriteLine("4.Конвертация валюты (пользователь вводит курс или выбирает из списка)");
//Console.WriteLine("5.Поиск по названию");
//Console.WriteLine("0.Выход");

using System;
using System.Globalization;

namespace ExpenseTracker
{
    class programm
    {
        void Main(string[] args)
        {
            Console.WriteLine("-----консольное приложение для подсчета потраченных за день средств-----");
            Console.WriteLine("Введите количество операций за день");
            int count;

            do
            {
                Console.Write("Введите количество операций (2-40): ");
            } while (!int.TryParse(Console.ReadLine(), out count) || count <= 2 || count >= 40);

            string[] names = new string[count];
            decimal[] amounts = new decimal[count];

            Console.WriteLine("\nВведите траты в формате: Название; Сумма");
            for (int i = 0; i <= count; i++)
            {
                bool isValid = false;
                while(!isValid)
                {
                    Console.Write($"Операция {i + 1}: ");
                    string input = Console.ReadLine();

                    string[] parts = input.Split(';');
                    if (parts.Length == 2)
                    {
                        string name = parts[0].Trim();
                        if (string.IsNullOrEmpty(name))
                        {
                            Console.WriteLine("Название не может быть пустым!");
                            continue;
                        }
                        if (decimal.TryParse(parts[1].Trim(), NumberStyles.Any, CultureInfo.InvariantCulture, out decimal amount))
                        {
                            if (amount <= 0)
                            {
                                Console.WriteLine("Сумма должна быть положительной!");
                                continue;
                            }

                            names[i] = name;
                            amounts[i] = amount;
                            isValid = true;
                        }
                        else
                        {
                            Console.WriteLine("Неверный формат суммы!");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Неверный формат ввода");
                    }
                }
            }

        }
    }
}