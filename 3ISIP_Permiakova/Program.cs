public enum ProductCategory
{
    Electronics = 1,
    Clothing = 2,
    Food = 3,
    Books = 4,
    Sports = 5
}

public class Product
{
    private static int nextId = 1;
    public int Id { get; }
    public string Name { get; private set; }
    public decimal Price { get; private set; }
    public int Quantity { get; private set; }
    public bool InStock => Quantity > 0;
    public ProductCategory Category { get; private set; }


    public Product(string name, decimal price, int quantity, ProductCategory category)
    {
        if (name == null) throw new ArgumentNullException("Название товара не может быть пустым");
        if (price <= 0) throw new ArgumentOutOfRangeException("Цена должна быть положительной");
        if (quantity < 0) throw new ArgumentOutOfRangeException("Количество не может быть отрицательным");

        Id = nextId++;
        Name = name;
        Price = price;
        Quantity = quantity;
        Category = category;
    }

    public void UpdateQuantity(int amount)
    {
        if (Quantity + amount < 0)
            throw new ArgumentOutOfRangeException("недостаток товара на складе");
        Quantity += amount;
    }

    public string Print()
    {
        return $"Код: {Id}, Название: {Name}, Цена: {Price:C}, " +
               $"Количество: {Quantity}, В наличии: {(InStock ? "Да" : "Нет")}, " +
               $"Категория: {Category}";
    }

}
//класс хранения информации специально для вовы это писал я 
public class SaleRecord
{
    public int ProductId { get; }
    public string ProductName { get; set; }
    public int Quantity { get; set; }
    public decimal TotalPrice { get; set; }
    public SaleRecord(int productId, string productName, int quantity, decimal totalPrice)
    {
        ProductId = productId;
        ProductName = productName;
        Quantity = quantity;
        TotalPrice = totalPrice;
    }
    public string GetInfo()
    {
        return $"{ProductName} (код {ProductId}) - {Quantity} шт. на сумму {TotalPrice:C}";
    }
}
class Programm
{
    private static List<Product> products = new List<Product>();
    private static Stack<SaleRecord> salesHistory = new Stack<SaleRecord>(); // тут храним историю
    private static Stack<SaleRecord> undoSale = new Stack<SaleRecord>(); // тут для отмены 
    static void Main(string[] args)
    {
        AddTestData();

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
            Console.WriteLine("8. Вывод всех товаров");
            Console.WriteLine("9. История продаж с возможностью отмены последней продажи.");
            Console.WriteLine("10. Отчёт о продажах.");
            Console.WriteLine("11. Выход");
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
                    ShowAllProducts();
                    break;
                case "9":
                    HistorySellProduct();
                    break;
                case "10":
                    ReportProduct();
                    break;
                case "11":
                    Console.WriteLine("До свидания!");
                    return;
                default:
                    Console.WriteLine("Неверный выбор. Попробуйте снова.");
                    break;
            }
        }
    }
    static void AddTestData()
    {
        try
        {
            products.Add(new Product("Смартфон Samsung", 25000, 15, ProductCategory.Electronics));
            products.Add(new Product("Футболка хлопковая", 1500, 50, ProductCategory.Clothing));
            products.Add(new Product("Яблоки", 120, 100, ProductCategory.Food));
            products.Add(new Product("Война и мир", 800, 20, ProductCategory.Books));
            products.Add(new Product("Футбольный мяч", 3000, 30, ProductCategory.Sports));
            Console.WriteLine("Добавлено 5 тестовых товаров");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка при добавлении тестовых данных: {ex.Message}");
        }
    }

    static void AddTovar()
    {
        Console.WriteLine("\n--- Добавление товара ---");
        Console.Write("Название товара: ");
        string name = Console.ReadLine();

        Console.Write("Цена товара: ");
        if (!decimal.TryParse(Console.ReadLine(), out decimal price))
        {
            Console.WriteLine("Ошибка: цена должна быть числом");
            return;
        }

        Console.Write("Количество: ");
        if (!int.TryParse(Console.ReadLine(), out int quantity))
        {
            Console.WriteLine("Ошибка: количество должно быть числом");
            return;
        }

        Console.WriteLine("Категории:");
        foreach (var cat in Enum.GetValues(typeof(ProductCategory)))
        {
            Console.WriteLine($"{(int)cat} - {cat}");
        }
        Console.Write("Выберите категорию: ");
        if (!int.TryParse(Console.ReadLine(), out int catNum) || !Enum.IsDefined(typeof(ProductCategory), catNum))
        {
            Console.WriteLine("Ошибка: неверная категория");
            return;
        }

        products.Add(new Product(name, price, quantity, (ProductCategory)catNum));
        Console.WriteLine("Товар успешно добавлен!");
    }


    static void RemoveTovar()
    {
        Console.Write("\nВведите код товара для удаления: ");
        if (!int.TryParse(Console.ReadLine(), out int id))
        {
            Console.WriteLine("Ошибка: код должен быть числом");
            return;
        }

        var product = products.FirstOrDefault(p => p.Id == id);
        if (product != null)
        {
            products.Remove(product);
            Console.WriteLine("Товар удален.");
        }
        else
        {
            Console.WriteLine("Товар не найден.");
        }

    }

    static void PostavkaTovara()
    {
        Console.Write("\nВведите код товара для поставки: ");
        if (!int.TryParse(Console.ReadLine(), out int id)) return;

        var product = products.FirstOrDefault(p => p.Id == id);
        if (product == null)
        {
            Console.WriteLine("Товар не найден.");
            return;
        }

        Console.Write("Введите количество для поставки: ");
        if (!int.TryParse(Console.ReadLine(), out int amount)) return;

        product.UpdateQuantity(amount);
        Console.WriteLine("Поставка выполнена.");
    }

    static void SellTovar()
    {
        Console.Write("\nВведите код товара для продажи: ");
        if (!int.TryParse(Console.ReadLine(), out int id)) return;

        var product = products.FirstOrDefault(p => p.Id == id);

        if (product == null)
        {
            Console.WriteLine("Товар не найден.");
            return;
        }
        Console.Write("Введите количество для продажи: ");
        if (!int.TryParse(Console.ReadLine(), out int amount) || amount <= 0)
        {
            Console.WriteLine("Ошибка: количество должно быть положительным числом");
            return;
        }
        try
        {
            product.UpdateQuantity(-amount);
            //Тут короче записываем в стек при продаже это для истории
            decimal total = amount * product.Price;
            var record = new SaleRecord(product.Id, product.Name, amount, total);
            salesHistory.Push(record);
            undoSale.Push(record);
            Console.WriteLine($"Продажа успешна. Остаток товара: {product.Quantity}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка: {ex.Message}");
        }
    }

    static void SearchTovarByCode()
    {
        Console.Write("\nВведите код товара: ");
        if (!int.TryParse(Console.ReadLine(), out int id)) return;

        var product = products.FirstOrDefault(p => p.Id == id);
        Console.WriteLine(product != null ? product.Print() : "Товар не найден.");
    }

    static void SearchTovarByName()
    {
        Console.Write("\nВведите название товара: ");
        string name = Console.ReadLine()?.ToLower();

        var found = products.Where(p => p.Name.ToLower().Contains(name)).ToList();
        if (found.Any())
            found.ForEach(p => Console.WriteLine(p));
        else
            Console.WriteLine("Товары не найдены.");
    }

    static void SearchTovarByCategory()
    {
        Console.WriteLine("\nКатегории:");
        foreach (var cat in Enum.GetValues(typeof(ProductCategory)))
        {
            Console.WriteLine($"{(int)cat} - {cat}");
        }

        Console.Write("Введите номер категории: ");
        if (!int.TryParse(Console.ReadLine(), out int catNum) || !Enum.IsDefined(typeof(ProductCategory), catNum))
        {
            Console.WriteLine("Ошибка: неверная категория");
            return;
        }

        var found = products.Where(p => p.Category == (ProductCategory)catNum).ToList();
        if (found.Any())
            found.ForEach(p => Console.WriteLine(p));
        else
            Console.WriteLine("Товары не найдены.");
    }

    static void ShowAllProducts()
    {
        Console.WriteLine("\n--- Все товары в магазине ---");

        if (products.Any())
        {
            Console.WriteLine($"Всего товаров: {products.Count}");
            foreach (var product in products.OrderBy(p => p.Id))
            {
                Console.WriteLine(product.Print());
            }
        }
        else
        {
            Console.WriteLine("В магазине нет товаров.");
        }
    }


    //ну тут просто обращаемся к вышенаписанному
    static void HistorySellProduct()
    {
        Console.WriteLine("\n--- История продаж ---");

        if (!salesHistory.Any())
        {
            Console.WriteLine("Продаж пока не было.");
            return;
        }

        foreach (var sale in salesHistory)
        {
            Console.WriteLine(sale.GetInfo());
        }
        Console.WriteLine("\nХотите отменить последнюю продажу? (д/н): ");
        string choice = Console.ReadLine();
        if (choice?.ToLower() == "д")
        {
            if (!undoSale.Any())
            {
                Console.WriteLine("Нет продаж для отмены.");
                return;
            }

            var lastSale = undoSale.Pop();
            var product = products.FirstOrDefault(p => p.Id == lastSale.ProductId);
            if (product != null)
            {
                product.UpdateQuantity(lastSale.Quantity);  //возвращаем товар 
                salesHistory = new Stack<SaleRecord>(salesHistory.Where(s => s != lastSale).Reverse());
                Console.WriteLine("Последняя продажа отменена!");
            }
        }
    }
    //ну тут именно очко перебираем по товарам и выводим 
    static void ReportProduct()
    {
        Console.WriteLine("\n--- Отчёт о продажах ---");

        if (!salesHistory.Any())
        {
            Console.WriteLine("Продаж пока не было.");
            return;
        }

        //общие показатели
        int totalItems = salesHistory.Sum(s => s.Quantity);
        decimal totalRevenue = salesHistory.Sum(s => s.TotalPrice);

        Console.WriteLine($"Всего продано товаров: {totalItems}");
        Console.WriteLine($"Общая выручка: {totalRevenue:C}");

        //продажи по каждому товару
        var grouped = salesHistory
            .GroupBy(s => s.ProductName)
            .Select(g => new
            {
                Name = g.Key,
                Count = g.Sum(s => s.Quantity),
                Sum = g.Sum(s => s.TotalPrice)
            });

        Console.WriteLine("\nПродажи по товарам:");
        foreach (var item in grouped)
        {
            Console.WriteLine($"{item.Name}: {item.Count} шт. на сумму {item.Sum:C}");
        }
    }

}