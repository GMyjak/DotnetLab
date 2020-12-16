using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Xml.Linq;

namespace Lab9
{
    public class Department
    {
        public int Id { get; set; }
        public String Name { get; set; }
    }
    public enum Gender
    {
        Female, Male
    }
    public class Student
    {
        public int Id { get; set; }
        public int Index { get; set; }
        public string Name { get; set; }
        public Gender Gender { get; set; }
        public bool Active { get; set; }
        public int DepartmentId { get; set; }
        public List<string> Topics { get; set; }

        public Student(int id, int index, string name, Gender gender, bool active, int departmentId, List<string> topics)
        {
            Id = id;
            Index = index;
            Name = name;
            Gender = gender;
            Active = active;
            DepartmentId = departmentId;
            Topics = topics;
        }

        public override string ToString()
        {
            return
                $"{Id}) {Index}, {Name}, {Gender}, {(Active ? "active" : "no active")}, {DepartmentId}, {string.Concat(Topics.Select(t => t + ", "))}";
        }
    }

    public class StudentWithTopics
    {
        public int Id { get; set; }
        public int Index { get; set; }
        public string Name { get; set; }
        public Gender Gender { get; set; }
        public bool Active { get; set; }
        public int DepartmentId { get; set; }
        public List<int> Topics { get; set; }

        public StudentWithTopics(int id, int index, string name, Gender gender, bool active, int departmentId,
            List<int> topics)
        {
            Id = id;
            Index = index;
            Name = name;
            Gender = gender;
            Active = active;
            DepartmentId = departmentId;
            Topics = topics;
        }

        public override string ToString()
        {
            return
                $"{Id}) {Index}, {Name}, {Gender}, {(Active ? "active" : "no active")}, {DepartmentId}, t: {string.Concat(Topics.Select(t => t + ", "))}";
        }
    }

    class Topic
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public Topic(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public override string ToString()
        {
            return $"t{Id}: {Name}";
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            foreach (var stud in GenerateStudents())
            {
                Console.WriteLine(stud);   
            }
            Console.WriteLine("\n");

            //Zad2
            GroupWithNElements(3);
            
            //Zad3a
            GroupByTopic();
            
            //Zad3b
            GroupByTopicWithGender();

            //Zad4
            ConvertToSWT();
        }

        static void GroupWithNElements(int n)
        {
            // Zadanie 2
            var groups = GenerateStudents()
                .OrderBy(std => (std.Name, std.Index))
                .Select((std, index) => new KeyValuePair<Student, int>(std, index))
                .GroupBy(pair => pair.Value / n)
                .Select(grp => grp.Select(pair => pair.Key))
                .ToList();

            for (int i = 0; i < groups.Count; i++)
            {
                Console.WriteLine("GRUPA " + i);
                foreach (var stud in groups[i])
                {
                    Console.WriteLine(stud);
                }
            }
            Console.WriteLine("\n");
        }

        static void GroupByTopic()
        {
            // Zadanie 3
            var topics = GenerateStudents()
                .SelectMany(std => std.Topics)
                .GroupBy(str => str)
                .Select(grp => new KeyValuePair<string, int>(grp.Key, grp.Count()))
                .OrderBy(pair => pair.Value)
                .Reverse()
                //.Select(pair => pair.Key)
                .ToList();
            foreach (var stud in topics)
            {
                Console.WriteLine(stud);
            }
            Console.WriteLine("\n");
        }

        static void GroupByTopicWithGender()
        {
            // Zadanie 3
            var topics = GenerateStudents()
                .GroupBy(std => std.Gender)
                .SelectMany(grp => grp.SelectMany(std => std.Topics.Select(top => new KeyValuePair<string, Gender>(top, grp.Key))))
                .GroupBy(pair => pair)
                .Select(grp => new KeyValuePair<KeyValuePair<string, Gender>, int>(grp.Key, grp.Count()))
                .OrderBy(pair => pair.Value)
                .Reverse()
                //.Select(pair => pair.Key)
                .ToList();

            foreach (var stud in topics)
            {
                Console.WriteLine(stud);
            }
            Console.WriteLine("\n");
        }

        static void ConvertToSWT()
        {
            // Zadanie 4
            var topics = GenerateStudents()
                .SelectMany(std => std.Topics)
                .Distinct()
                .Select((top, index) => new Topic(index, top))
                .ToList();

            Console.WriteLine("Topics:");
            foreach (var topic in topics)
            {
                Console.WriteLine(topic);
            }

            var swtList = GenerateStudents()
                .Select(std => new StudentWithTopics(std.Id, std.Index, std.Name, std.Gender, std.Active, std.DepartmentId,
                    std.Topics.Select(str => topics.First(t => t.Name == str).Id).ToList()))
                .ToList();

            Console.WriteLine("SWT list:");
            foreach (var stud in swtList)
            {
                Console.WriteLine(stud);
            }
        }

        static List<Student> GenerateStudents()
        {
            List<Student> result = new List<Student>();
            result.Add(new Student(1, 12345, "Nowak", Gender.Female, true, 1, new List<string>() { "C#", "PHP", "algorithms" }));
            result.Add(new Student(2, 13235, "Kowalski", Gender.Male, true, 1, new List<string>() { "C#", "C++", "fuzzy logic" }));
            result.Add(new Student(3, 13444, "Schmidt", Gender.Male, false, 2, new List<string>() { "Basic", "Java" }));
            result.Add(new Student(4, 14000, "Newman", Gender.Female, false, 3, new List<string>() { "JavaScript", "neural networks" }));
            result.Add(new Student(5, 14001, "Bandingo", Gender.Male, true, 3, new List<string>() { "Java", "C#" }));
            result.Add(new Student(6, 14100, "Miniwiliger", Gender.Male, true, 2, new List<string>() { "algorithms", "web programming" }));
            result.Add(new Student(11, 22345, "Nowak", Gender.Female, true, 2, new List<string>() { "C#", "PHP", "algorithms" }));
            result.Add(new Student(12, 23235, "Nowak", Gender.Male, true, 1, new List<string>() { "C#", "C++", "fuzzy logic" }));
            result.Add(new Student(13, 23444, "Schmidt", Gender.Male, false, 1, new List<string>() { "Basic", "Java" }));
            result.Add(new Student(14, 24000, "Newman", Gender.Female, false, 1, new List<string>() { "JavaScript", "neural networks" }));
            result.Add(new Student(15, 24001, "Bandingo", Gender.Male, true, 3, new List<string>() { "Java", "C#" }));
            result.Add(new Student(16,24100, "Bandingo", Gender.Male, true, 2, new List<string>() { "algorithms", "web programming" }));
            return result;
        }
    }
}
