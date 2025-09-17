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
        if(Quantity+amount < 0)
            throw new ArgumentOutOfRangeException("недостаток товара на складе");
        Quantity += amount;
    }

    public override string ToString()
    {
        return $"Код: {Id}, Название: {Name}, Цена: {Price:C}, " +
               $"Количество: {Quantity}, В наличии: {(InStock ? "Да" : "Нет")}, " +
               $"Категория: {Category}";
    }
}

class Programm
{
    private static List<Product> products = new List<Product>();
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

    }

    static void RemoveTovar()
    {

    }

    static void PostavkaTovara()
    {

    }

    static void SellTovar()
    {

    }

    static void SearchTovarByCode()
    {

    }

    static void SearchTovarByName()
    {

    }

    static void SearchTovarByCategory()
    {

    }
}
