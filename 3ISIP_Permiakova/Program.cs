public static class Programm
{
    public static void Main()
    {
        AddTestData();

        Console.WriteLine("Меню для управления");

        while (true)
        {
            Console.WriteLine("0. Вывод всех книг");
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
                case "0":
                    OuptputAllBooks();
                    break;
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
    static List<Book> books = new List<Book>();
    static void OuptputAllBooks()
    {
        if (books.Count == 0)
        {
            Console.WriteLine("Библиотека пуста.");
            return;
        }

        Console.WriteLine("\nСписок всех книг в библиотеке:\n");
        foreach (var book in books)
        {
            Console.WriteLine(book.Print());
        }
        Console.WriteLine();
    }
    static void AddTestData()
    {
        books.Add(new Book("Мастер и Маргарита", 1500m, 1966, "Михаил Булгаков", BookJanre.Fantasy));
        books.Add(new Book("Преступление и наказание", 1200m, 1866, "Федор Достоевский", BookJanre.Drama));
        books.Add(new Book("Война и мир", 2000m, 1867, "Лев Толстой", BookJanre.Drama));
        books.Add(new Book("Евгений Онегин", 800m, 1833, "Александр Пушкин", BookJanre.Romantic));
        books.Add(new Book("Гарри Поттер и философский камень", 1800m, 1997, "Джоан Роулинг", BookJanre.Fantasy));
        books.Add(new Book("Анна Каренина", 1300m, 1877, "Лев Толстой", BookJanre.Drama));
        books.Add(new Book("Ромео и Джульетта", 750m, 1597, "Уильям Шекспир", BookJanre.Romantic));
        books.Add(new Book("Властелин колец", 2200m, 1954, "Джон Толкин", BookJanre.Fantasy));

        Console.WriteLine("Тестовые данные добавлены успешно!");
        Console.WriteLine($"Добавлено 8 тестовых книг");
    }

    static void AddBook()
    {
        Console.Write("Введите название книги: ");
        string name = Console.ReadLine();

        Console.Write("Введите автора: ");
        string author = Console.ReadLine();

        Console.Write("Введите год издания: ");
        int year;
        while (!int.TryParse(Console.ReadLine(), out year) || year <= 0)
        {
            Console.Write("Некорректный ввод. Введите год издания снова: ");
        }

        Console.Write("Введите цену: ");
        decimal price;
        while (!decimal.TryParse(Console.ReadLine(), out price) || price <= 0)
        {
            Console.Write("Некорректный ввод. Введите положительную цену: ");
        }

        Console.WriteLine("\nВыберите жанр:");
        int index = 1;
        foreach (BookJanre genre in Enum.GetValues(typeof(BookJanre)))
        {
            Console.WriteLine($"{index}. {genre}");
            index++;
        }

        int genreChoice;
        while (!int.TryParse(Console.ReadLine(), out genreChoice) || genreChoice < 1 || genreChoice > Enum.GetValues(typeof(BookJanre)).Length)
        {
            Console.Write("Некорректный выбор. Введите номер жанра: ");
        }

        BookJanre selectedGenre = (BookJanre)Enum.GetValues(typeof(BookJanre)).GetValue(genreChoice - 1);

        Book newBook = new Book(name, price, year, author, selectedGenre);
        books.Add(newBook);



    }

    static void RemoveBook()
    {
        Console.Write("Введите ID для удаления:");
        int id;
        if (int.TryParse(Console.ReadLine(), out id))
        {
            var book = books.FirstOrDefault(b => b.ID == id);
            if (book != null)
            {
                books.Remove(book);
                Console.WriteLine("Успешно!");
            }
        }
    }

    static void SearchNameBook()
    {
        Console.Write("Введите название книги: ");
        string name = Console.ReadLine().ToLower();
        var results = books.Where(b => b.Name.ToLower().Contains(name)).ToList();

        if (results.Count == 0)
        {
            Console.WriteLine("Книги не найдены");
        }
        else
        {
            results.ForEach(b => Console.WriteLine(b.Print()));
        }
    }


    static void SearchAutorBook()
    {
        Console.Write("Введите автора книги: ");
        string Autor = Console.ReadLine().ToLower();
        var results = books.Where(b => b.Autor.ToLower().Contains(Autor)).ToList();

        if (results.Count == 0)
        {
            Console.WriteLine("Книги не найдены");
        }
        else
        {
            results.ForEach(b => Console.WriteLine(b.Print()));
        }
    }

    static void SearchJanreBook()
    {
        Console.Write("Введите жанр книги: ");
        if (books.Count == 0)
        {
            Console.WriteLine("Библиотека пуста.\n");
            return;
        }

        Console.WriteLine("Выберите жанр:");
        foreach (var genre in Enum.GetValues(typeof(BookJanre)))
            Console.WriteLine($"{(int)genre} — {genre}");

        Console.Write("\nВведите номер жанра: ");
        if (!int.TryParse(Console.ReadLine(), out int choice) || !Enum.IsDefined(typeof(BookJanre), choice))
        {
            Console.WriteLine("Некорректный выбор.\n");
            return;
        }

        BookJanre selectedGenre = (BookJanre)choice;

        var filteredBooks = books.Where(b => b.Janre == selectedGenre).ToList();

        if (filteredBooks.Count == 0)
        {
            Console.WriteLine($"Книг жанра {selectedGenre} не найдено.\n");
            return;
        }

        Console.WriteLine($"\nКниги жанра {selectedGenre}:\n");
        foreach (var book in filteredBooks)
            Console.WriteLine(book.Print());

    }

    static void SortByName()
    {
        var sorted = books.OrderBy(b => b.Name).ToList();
        sorted.ForEach(b => Console.WriteLine(b.Print()));
    }

    static void SortByYear()
    {
        var sorted = books.OrderBy(b => b.Year).ToList();
        sorted.ForEach(b => Console.WriteLine(b.Print()));
    }

    static void OutputMinPrice()
    {
        var min = books.OrderBy(b => b.Price).ToList().FirstOrDefault();
        Console.WriteLine(min.Print());
    }

    static void OutputMaxPrice()
    {
        var max = books.OrderBy(b => b.Price).ToList().LastOrDefault();
        Console.WriteLine(max.Print());
    }

    static void OutputCountBookAutor()
    {
        var groups = books.GroupBy(b => b.Autor)
                           .Select(g => new { Autor = g.Key, Count = g.Count() });
        Console.WriteLine("Количество книг по авторам:");
        foreach (var g in groups)
        {
            Console.WriteLine($"{g.Autor}: {g.Count}");
        }
        Console.WriteLine();
    }
}

public enum BookJanre
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
    public string Autor { get; private set; }
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