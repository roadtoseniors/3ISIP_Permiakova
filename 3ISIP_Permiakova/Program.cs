Console.WriteLine("-----консольное приложение для подсчета потраченных за день средств-----");
Console.WriteLine("Введите количество операций за день");
int count = Convert.ToInt32(Console.ReadLine());
for (int i = 0; i <= count; i++)
{
    Console.WriteLine($"Операция номер {i}");
    Console.WriteLine("Введите название траты по шаблону");
    Console.WriteLine("Название услуги или товара; Количество денег (рубли)");
    
}

Console.WriteLine("-----Меню-----");
Console.WriteLine("1.Вывод данных");
Console.WriteLine("2.Статистика (среднее, максимальное, минимальное, сумма)");
Console.WriteLine("3.Сортировка по цене (пузырьковая сортировка)");
Console.WriteLine("4.Конвертация валюты (пользователь вводит курс или выбирает из списка)");
Console.WriteLine("5.Поиск по названию");
Console.WriteLine("0.Выход");