Console.WriteLine("Меню для управления");

while (true)
{
    Console.WriteLine("1. Добавление товара");
    Console.WriteLine("2. Удаление товара");
    Console.WriteLine("3. Заказать поставку товара");
    Console.WriteLine("4. Продать товар");
    Console.WriteLine("5. Поиск товаров по коду");
    Console.WriteLine("6. Поиск товаров по названию");
    Console.WriteLine("7. Поиск товаров по категории");
    Console.WriteLine("8. Выход");
    Console.Write("Ваш выбор: ");
    string choice = Console.ReadLine();

    switch (choice)
    {
        case "1":
            AddTovar();
            break;
        case "2":
            RemoveTovar();
            break;
        case "3":
            PostavkaTovara();
            break;
        case "4":
            SellTovar();
            break;
        case "5":
            SearchTovarByCode();
            break;
        case "6":
            SearchTovarByName();
            break;
        case "7":
            SearchTovarByCategory();
            break;
        case "8":
            Console.WriteLine("До свидания!");
            return;
        default:
            Console.WriteLine("Неверный выбор. Попробуйте снова.");
            break;
    }
}

void AddTovar()
{

}

void RemoveTovar()
{

}

void PostavkaTovara()
{

}

void SellTovar()
{

}

void SearchTovarByCode()
{

}

void SearchTovarByName()
{

}

void SearchTovarByCategory()
{

}