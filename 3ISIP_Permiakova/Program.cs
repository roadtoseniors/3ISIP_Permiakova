
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
    private int NumberOfStudent;

    public Student (string fullName, int age, bool gender, int numberOfstudent)
        : base(fullName, age, gender)
    {
        NumberOfStudent = numberOfstudent;
    }
    
    public override void Print()
    {
        Console.WriteLine("Студент");
        base.Print();
        Console.WriteLine($"Номер студака {NumberOfStudent}");
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
    public List<Student> Student {  get; private set; }
    public List<Professor> Professor { get; private set; }


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

public class University
{   
        //var testing = new List<Person>
        //{
        //    new Person("Горланыч Вова", 22, true),
        //    new Student("Валим Гусенков", 18, true, 123456),
        //};

        Console.WriteLine("Менюшка");
        while (true)
        {
            Console.WriteLine("1. добавление студента");
            Console.WriteLine("2. добавление преподавателя");
            Console.WriteLine("3. добавление курса");
            Console.WriteLine("4. записать студента на курс");
            Console.WriteLine("5. запись преподавателя на курс");

            string choise = Console.ReadLine();

            switch(choise )
            {
                case "1":
                    AddStudent();
                    break;
                case "2":
                    AddProfessor();
                    break;
                case "3":
                    AddCourses();
                    break;
                case"4":
                    WriteStudent();
                    break;
                case"5":
                    WriteProfessor();
                    break;
                default:
                    Console.WriteLine("гавно");
                    break;
            }

            static void AddStudent()
            {

            }

            static void AddProfessor()
            {

            }

            static void AddCourses()
            {

            }

            static void WriteStudent()
            {

            }

            static void WriteProfessor()
            {

            }
        }
}