
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
        string strGender = Gender ? "Мужской" : "Женский";
        Console.WriteLine($"ФИО: {FullName}, Возраст: {Age}, Пол: {strGender}");
    }

    public string GetFullName() => FullName; // чтобы внутри University обращаться к имени
}

class Student : Person
{
    private int NumberOfStudent;

    public Student(string fullName, int age, bool gender, int numberOfstudent)
        : base(fullName, age, gender)
    {
        NumberOfStudent = numberOfstudent;
    }

    public override void Print()
    {
        Console.WriteLine("Студент");
        base.Print();
        Console.WriteLine($"Номер студака: {NumberOfStudent}");
    }

    public string GetFullNameStudent() => GetFullName();
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
        Console.WriteLine($"Квалификация: {Qualification}");
    }

    public string GetFullNameProfessor() => GetFullName();
}

class Cours
{
    private static int nextID = 1;
    public int ID { get; }
    public string NameProf { get; private set; }
    public string NameCourse { get; private set; }
    public List<Student> Student { get; private set; }
    public Professor Professor { get; private set; }

    public Cours(string nameCourse)
    {
        ID = nextID++;
        NameCourse = nameCourse;
        Student = new List<Student>();
    }

    public void WriteProfessor(Professor prof)
    {
        Professor = prof;
        NameProf = prof.GetFullNameProfessor();
    }

    public void AddStusent(Student student)
    {
        Student.Add(student);
    }

    public void Print()
    {
        Console.WriteLine($"Индификатор: {ID}, Имя преподавателя: {(NameProf ?? "не назначен")}, Название курса: {NameCourse}");
        Console.WriteLine("Студенты на курсе:");
        if (Student.Count == 0)
            Console.WriteLine("  (нет студентов)");
        else
            foreach (var s in Student)
                s.Print();
    }
}

public class University
{
    private static List<Student> students = new List<Student>();
    private static List<Professor> professors = new List<Professor>();
    private static List<Cours> courses = new List<Cours>();

    public static void Main()
    {
        Student student = new Student("Durak", 18, false, 3);
        student.GetFullName();

        Console.WriteLine("Менюшка");
        while (true)
        {
            Console.WriteLine("1. добавление студента");
            Console.WriteLine("2. добавление преподавателя");
            Console.WriteLine("3. добавление курса");
            Console.WriteLine("4. записать студента на курс");
            Console.WriteLine("5. запись преподавателя на курс");
            Console.WriteLine("0. выход");

            string choise = Console.ReadLine();

            switch (choise)
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
                case "4":
                    WriteStudent();
                    break;
                case "5":
                    WriteProfessor();
                    break;
                case "0":
                    return;
                default:
                    Console.WriteLine("Некорректный ввод!");
                    break;
            }

            static void AddStudent()
            {
                Console.Write("ФИО: ");
                string name = Console.ReadLine();
                Console.Write("Возраст: ");
                int age = Convert.ToInt32(Console.ReadLine());
                Console.Write("Пол (М или Ж): ");
                bool gender = Console.ReadLine().ToLower() == "м";
                Console.Write("Номер студатка: ");
                int num = Convert.ToInt32(Console.ReadLine());

                var s = new Student(name, age, gender, num);
                students.Add(s);

                Console.WriteLine("\nСписок студентов:");
                foreach (var student in students)
                {
                    student.Print();
                    Console.WriteLine("<----------------!----------------->");
                }
            }

            static void AddProfessor()
            {
                Console.Write("ФИО: ");
                string name = Console.ReadLine();
                Console.Write("Возраст: ");
                int age = Convert.ToInt32(Console.ReadLine());
                Console.Write("Пол (М или Ж): ");
                bool gender = Console.ReadLine().ToLower() == "м";
                Console.Write("Квалификация: ");
                string qual = Console.ReadLine();

                var p = new Professor(name, age, gender, qual);
                professors.Add(p);

                Console.WriteLine("\nСписок преподавателей:");
                foreach (var prof in professors)
                {
                    prof.Print();
                    Console.WriteLine("<----------------!----------------->");
                }
            }

            static void AddCourses()
            {
                Console.Write("Название курса: ");
                string name = Console.ReadLine();

                var c = new Cours(name);
                courses.Add(c);

                Console.WriteLine("\nСписок курсов:");
                foreach (var course in courses)
                {
                    course.Print();
                    Console.WriteLine("<----------------!----------------->");
                }
            }

            static void WriteStudent()
            {
                if (courses.Count == 0 || students.Count == 0)
                {
                    Console.WriteLine("Нет курсов или студентов");
                    return;
                }

                Console.WriteLine("Выберите курс:");
                for (int i = 0; i < courses.Count; i++)
                    Console.WriteLine($"{i + 1}. {courses[i].NameCourse}");
                int courseIndex = int.Parse(Console.ReadLine()) - 1;

                Console.WriteLine("Выберите студента:");
                for (int i = 0; i < students.Count; i++)
                    Console.WriteLine($"{i + 1}. {students[i].GetFullNameStudent()}");
                int studentIndex = int.Parse(Console.ReadLine()) - 1;

                courses[courseIndex].AddStusent(students[studentIndex]);
                Console.WriteLine("Студент добавлен на курс");
            }

            static void WriteProfessor()
            {
                if (courses.Count == 0 || professors.Count == 0)
                {
                    Console.WriteLine("Нет курсов или преподавателей!");
                    return;
                }

                Console.WriteLine("Выберите курс:");
                for (int i = 0; i < courses.Count; i++)
                    Console.WriteLine($"{i + 1}. {courses[i].NameCourse}");
                int courseIndex = int.Parse(Console.ReadLine()) - 1;

                Console.WriteLine("Выберите преподавателя:");
                for (int i = 0; i < professors.Count; i++)
                    Console.WriteLine($"{i + 1}. {professors[i].GetFullNameProfessor()}");
                int profIndex = int.Parse(Console.ReadLine()) - 1;

                courses[courseIndex].WriteProfessor(professors[profIndex]);
                Console.WriteLine("Преподаватель назначен на курс");
            }
        }
    }
}