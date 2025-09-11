using System.Globalization;

Console.WriteLine("Введите количество операций (от 2 до 40):");
int count;
while (!(int.TryParse(Console.ReadLine(), out count) && count >= 2 && count <= 40))
{
    Console.WriteLine("Ошибка! Введите число от 2 до 40:");
}

