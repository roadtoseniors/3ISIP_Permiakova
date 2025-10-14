class Person
{
    private string FullName;
    private int Age;
    private bool Gender;

    public Person(string fullName, int age, bool gender)
    {
        FullName = fullName;
        Age = age;
        Gender = gender;
    }

    public virtual void Print()
    {
        Console.WriteLine($"ФИО: {FullName}, Возраст: {Age}, Пол: {Gender}");
    }
}

class Student : Person
{
    private List<Cours> Courses;
    private int NumberOfStudent;

    public Student (string fullName, int age, bool gender, List<Cours> courses, int numberOfstudent)
        : base(fullName, age, gender)
    {
        Courses = courses;
        NumberOfStudent = numberOfstudent;
    }
    
    public override void Print()
    {
        Console.WriteLine("Студент");
        base.Print();
        Console.WriteLine($"Список курсов {Courses}, номер студака {NumberOfStudent}");
    }
}

class Professor : Person
{
    private string Qualification;

    public Professor(string fullName, int age, bool gender, string qualification)
        : base(fullName, age, gender)
    {
        Qualification = qualification;
    }

    public override void Print()
    {
        Console.WriteLine("Преподаватель");
        base.Print();
        Console.WriteLine($"Квалификация {Qualification}");
    }
}

class Cours
{
    public int nextID = 1;
    public int ID { get; }
    public string NameProf { get; private set; }
    public string NameCourse { get; private set; }

    public Cours(int nextID, int iD, string nameProf, string nameCourse)
    {
        ID = nextID++;
        NameProf = nameProf;
        NameCourse = nameCourse;
    }

    public void Print()
    {
        Console.WriteLine($"Индификатор {ID}, Имя преподавателя {NameProf}, Название курса {NameCourse}");
    }
}