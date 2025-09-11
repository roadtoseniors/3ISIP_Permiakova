using System.Globalization;

Console.WriteLine("Введите количество операций (от 2 до 40):");
int count;
while (!(int.TryParse(Console.ReadLine(), out count) && count >= 2 && count <= 40))
{
    Console.WriteLine("Ошибка! Введите число от 2 до 40:");
}

string[] names = new string[count];
double[] amounts = new double[count];

for (int i = 0; i < count; i++)
{
    Console.WriteLine($"Операция {i + 1}: введите данные в формате (Название; Сумма)");
    string input = Console.ReadLine();
    string[] parts = input.Split(';');

    if (parts.Length == 2 && double.TryParse(parts[1], NumberStyles.Any, CultureInfo.InvariantCulture, out double amount))
    {
        names[i] = parts[0].Trim();
        amounts[i] = amount;
    }
    else
    {
        Console.WriteLine("Ошибка ввода! Попробуйте ещё раз.");
        i--;
    }
}

while (true)
{
    Console.WriteLine("\nМеню:");
    Console.WriteLine("1. Вывод данных");
    Console.WriteLine("2. Статистика (среднее, максимум, минимум, сумма)");
    Console.WriteLine("3. Сортировка по цене (пузырьковая сортировка)");
    Console.WriteLine("4. Поиск по названию");
    Console.WriteLine("0. Выход");
    Console.Write("Ваш выбор: ");

    string choice = Console.ReadLine();

    switch (choice)
    {
        case "1":
            Console.WriteLine("\nСписок расходов:");
            for (int i = 0; i < count; i++)
                Console.WriteLine($"{names[i]} — {amounts[i]} руб.");
            break;

        case "2":
            double sum = 0, max = double.MinValue, min = double.MaxValue;
            for (int i = 0; i < count; i++)
            {
                sum += amounts[i];
                if (amounts[i] > max) max = amounts[i];
                if (amounts[i] < min) min = amounts[i];
            }
            double avg = sum / count;
            Console.WriteLine($"\nСтатистика:");
            Console.WriteLine($"Сумма: {sum} руб.");
            Console.WriteLine($"Среднее: {avg:F2} руб.");
            Console.WriteLine($"Максимум: {max} руб.");
            Console.WriteLine($"Минимум: {min} руб.");
            break;

        case "3":
            for (int i = 0; i < count - 1; i++)
            {
                for (int j = 0; j < count - i - 1; j++)
                {
                    if (amounts[j] > amounts[j + 1])
                    {
                        // меняем местами суммы
                        double tempAmount = amounts[j];
                        amounts[j] = amounts[j + 1];
                        amounts[j + 1] = tempAmount;

                        // меняем местами названия
                        string tempName = names[j];
                        names[j] = names[j + 1];
                        names[j + 1] = tempName;
                    }
                }
            }
            Console.WriteLine("\nОтсортированный список:");
            for (int i = 0; i < count; i++)
                Console.WriteLine($"{names[i]} — {amounts[i]} руб.");
            break;



        case "4":
            Console.WriteLine("Введите название для поиска:");
            string search = Console.ReadLine().ToLower();
            bool found = false;
            for (int i = 0; i < count; i++)
            {
                if (names[i].ToLower().Contains(search))
                {
                    Console.WriteLine($"{names[i]} — {amounts[i]} руб.");
                    found = true;
                }
            }
            if (!found) Console.WriteLine("Совпадений не найдено.");
            break;

        case "0":
            Console.WriteLine("Выход...");
            return;

        default:
            Console.WriteLine("Неверный выбор!");
            break;
    }
}
