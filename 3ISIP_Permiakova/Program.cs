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


