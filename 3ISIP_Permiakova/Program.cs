public static class Programm
{
    public static void Main()
    {
        AddTestData();

        Console.WriteLine("Меню для управления");

        while (true)
        {
            Console.WriteLine("1. Добавление книги");
            Console.WriteLine("2. Удаление книги по ID");
            Console.WriteLine("3. Поиск книги по названию");
            Console.WriteLine("4. Поиск книги по автору");
            Console.WriteLine("5. Поиск книги по жанру");
            Console.WriteLine("6. Сортировка по названию");
            Console.WriteLine("7. Сортировка по году");
            Console.WriteLine("8. Вывод самой дешевой книги");
            Console.WriteLine("9. Вывод самой дорогой книги");
            Console.WriteLine("10. Вывод количества книг каждого автора");
            Console.WriteLine("11. Выход");
            Console.Write("Ваш выбор: ");
            string choice = Console.ReadLine();

            switch (choice) 
            {
                case "1":
                    AddBook();
                    break;
                case "2":
                    RemoveBook();
                    break;
                case "3":
                    SearchNameBook();
                    break;
                case "4":
                    SearchAutorBook();
                    break;
                case "5":
                    SearchJanreBook();
                    break;
                case "6":
                    SortByName();
                    break;
                case "7":
                    SortByYear();
                    break;
                case "8":
                    OutputMinPrice();
                    break;
                case "9":
                    OutputMaxPrice();
                    break;
                case "10":
                    OutputCountBookAutor();
                    break;
                case "11":
                    Console.WriteLine("До свидания!");
                    return;
                default:
                    Console.WriteLine("фигню нажал дебил");
                    break;
            }
        }
    }

    static void AddTestData()
    {
        Book book1 = new Book("Мастер и Маргарита", 1500m, 1966, "Михаил Булгаков", BookJanre.Fantasy);
        Book book2 = new Book("Преступление и наказание", 1200m, 1866, "Федор Достоевский", BookJanre.Drama);
        Book book3 = new Book("Война и мир", 2000m, 1867, "Лев Толстой", BookJanre.Drama);
        Book book4 = new Book("Евгений Онегин", 800m, 1833, "Александр Пушкин", BookJanre.Romantic);
        Book book5 = new Book("Гарри Поттер и философский камень", 1800m, 1997, "Джоан Роулинг", BookJanre.Fantasy);
        Book book6 = new Book("Анна Каренина", 1300m, 1877, "Лев Толстой", BookJanre.Drama);
        Book book7 = new Book("Ромео и Джульетта", 750m, 1597, "Уильям Шекспир", BookJanre.Romantic);
        Book book8 = new Book("Властелин колец", 2200m, 1954, "Джон Толкин", BookJanre.Fantasy);

        Console.WriteLine("Тестовые данные добавлены успешно!");
        Console.WriteLine($"Добавлено 8 тестовых книг");
    }

    static void AddBook()
    {

    }

    static void RemoveBook()
    {

    }

    static void SearchNameBook() 
    { 

    }

    static void SearchAutorBook()
    {

    }

    static void SearchJanreBook()
    {

    }

    static void SortByName()
    {

    }

    static void SortByYear()
    {

    }

    static void OutputMinPrice()
    {

    }

    static void OutputMaxPrice()
    {

    }

    static void OutputCountBookAutor()
    {

    }
}

enum BookJanre
{
    Romantic,
    Fantasy,
    Drama
}
public class Book
{
    private static int nextID = 1;
    public int ID { get; }
    public string Name { get; private set; }
    public string Autor {  get; private set; }
    public BookJanre Janre { get; private set; }
    public int Year { get; private set; }
    public decimal Price { get; private set; }

    public Book(string name, decimal price, int year, string autor, BookJanre category)
    {
        if (name == null) throw new ArgumentNullException("Название книги не может быть пустым");
        if (autor == null) throw new ArgumentNullException("Автор не может быть пустым");
        if (year <= 0) throw new ArgumentOutOfRangeException("Год не может быть отрицательным");
        if (price <= 0) throw new ArgumentOutOfRangeException("Цена должна быть положительной");

        ID = nextID++;
        Name = name;
        Price = price;
        Year = year;
        Autor = autor;
        Janre = category;
    }

    public string Print()
    {
        return $"Код: {ID}, Название: {Name}, Цена: {Price:R}, Год выхода: {Year}, Автор: {Autor}, Жанр: {Janre}";
    }

}

