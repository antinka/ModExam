using System;
using System.Collections.Generic;

namespace ModelingExam
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Student> Group1 = new List<Student>();
            Group1.Add(new Student("viktor"));
            Group1.Add(new Student("andrey"));
            Group1.Add(new Student("ivan"));

            List<Student> Group2 = new List<Student>();
            Group2.Add(new Student("Lena"));
            Group2.Add(new Student("Liza"));
            Group2.Add(new Student("Lilya"));

            Teacher teacherMath1 = new Teacher("Ivan Ivanov");
            Teacher teacherMath2 = new Teacher("Vasya Vasiliev");
            Teacher teacherLang1 = new Teacher("Alena Ivanova");

            Exam math = new Exam("Mathematic");
            Exam lang = new Exam("Language");

            math.ModelingExam(Group1, teacherMath1);
            math.ShowResult(Group1);
            Console.WriteLine();
            math.ModelingExam(Group2, teacherMath2);
            math.ShowResult(Group2);
            Console.WriteLine();
            lang.ModelingExam(Group2, teacherLang1);
            lang.ShowResult(Group2);
        }
    }
    
    interface IPerson
    {
        string Name { get; set; }
        int Id { get; set; }
    }
    class Student:IPerson
    {
        public string Name { get; set; }
        public int Id { get; set; }
        public int KoefOfLuck { get; set; }
        Random rnd = new Random();
        private static int id = 0;
        public Student(string Name)
        {
            this.Name = Name;
            KoefOfLuck = rnd.Next(80, 100);
            Id = id;
            id++;
        }

    }
    class Teacher : IPerson
    {
        public string Name { get; set; }
        public int Id { get; set; }
        public double KoefOfHarmful{ get; set; }
        Random rnd = new Random();
        private static int id = 0;
        public Teacher(string Name)
        {
            this.Name = Name;
            KoefOfHarmful = rnd.Next(5, 10);
            KoefOfHarmful /= 10;
            Id = id;
            id++;
        }
    }
    class Exam
    {
        public string Name { get; set; }
        Statement st;
        public Exam(string name)
        {
            Name = name;
        }
        public void ModelingExam(List<Student> stud, Teacher teacher)
        {
            Dictionary<int, int> markStud = new Dictionary<int, int>();
            foreach (Student std in stud)
            {
                markStud.Add(std.Id, (int)(std.KoefOfLuck * teacher.KoefOfHarmful));
            }
           st = new Statement(Name, teacher.Name, markStud);
        }
        public void ShowResult(List<Student> stud)
        {
            int countFailedStud = 0;
            Console.WriteLine($"Exam {st.NameExam}");
            Console.WriteLine($"Teacher {st.NameTeacher}");
            Console.WriteLine($"Students: ");
            foreach (KeyValuePair<int, int> keyValue in st.markStud)
            {
                foreach (Student std in stud)
                {
                    if (std.Id == keyValue.Key)
                    {
                        if(keyValue.Value < 60)
                        {
                            countFailedStud++;
                        }
                        Console.WriteLine(std.Name + " mark " + keyValue.Value);
                    }
                }
            }
            if(countFailedStud== stud.Count)
            {
                Console.WriteLine("the whole group did not pass the exam");
            }
            else if(countFailedStud==0)
            {
                Console.WriteLine("the whole group passed the exam");
            }
            else
            {
            Console.WriteLine($"In this groud {countFailedStud} did not pass the exam");
            }
        }
    }
    class Statement
    {
        public string NameExam { get; set; }
        public string NameTeacher { get; set; }
        public Dictionary<int, int> markStud = new Dictionary<int, int>();
        public Statement(string nameExam, string nameTeacher, Dictionary<int, int> markStud)
        {
            NameExam = nameExam;
            NameTeacher = nameTeacher;
           this.markStud = markStud;
        }
    }
}
