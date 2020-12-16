using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lab8
{
    class Program
    {
        static void Main()
        {
            // STRINGS
            //
            //
            Console.WriteLine("Initialize string list");
            ListOfArrayList<string> list = new ListOfArrayList<string>(4)
            {
                "A", "B", "C", "D",
                "E", "F", "G", "H",
                "I", "J", "K", "L",
                "M", "N", "O", "P"
            };

            list.Insert(4, "X");
            list.RemoveAt(6);
            list.Remove("F");
            list.Remove("G");

            Console.WriteLine($"After some operations:\n{list}");

            // This loop uses GetEnumerator() method
            Console.WriteLine($"\nUsing foreach loop:");
            foreach (var item in list)
            {
                Console.Write(item + " ");
            }
            Console.WriteLine("\n");

            List<string> otherList = new List<string>() { "Z", "Z", "Z" };
            list += otherList;

            Console.WriteLine("After appending list with three letters Z:");
            Console.WriteLine(list);

            Console.WriteLine("Using [] operator:");
            Console.WriteLine($"Item at index 3: {list[3]}");
            list[3] = "Q";
            Console.WriteLine($"Index of Q: {list.IndexOf("Q")}");
            Console.WriteLine($"Index of Ź: {list.IndexOf("Ź")}");

            Console.WriteLine("\nTrim demonstration:");
            // Leave only 4 elements in list
            int itemsToRemove = list.Count - 4;
            for (int i = 0; i < itemsToRemove; i++)
            {
                list.RemoveAt(0);
            }
            Console.WriteLine(list);

            // RemoveAt doesn't trim so we have to do it manually 
            list.Trim();


            // INTS 
            //
            //
            Console.WriteLine("Int demonstration:");
            ListOfArrayList<int> intList = new ListOfArrayList<int>(6);
            intList.Add(11);
            intList.Add(12);
            intList.Add(13);
            intList.Add(14);
            intList.Add(15);
            intList.Add(16);
            intList.Add(21);
            intList.Add(22);
            intList.Add(23);
            intList.Add(24);
            intList.Add(25);
            intList.Add(26);
            intList.Add(31);
            intList.Add(32);
            intList.Add(33);

            Console.WriteLine(intList);
            intList.Insert(14, 99);
            intList.Remove(11);
            intList.Remove(23);
            Console.WriteLine(intList);
            intList.RemoveAt(13);
            intList.RemoveAt(9);
            Console.WriteLine(intList);


            
            // STUDENTS
            //
            //
            Console.WriteLine("Students demonstration: ");
            ListOfArrayList<Student> studentList = new ListOfArrayList<Student>(2);
            var st1 = new Student();
            st1.FirstName = "A";
            st1.LastName = "B";
            var st2 = new Student();
            st2.FirstName = "A";
            st2.LastName = "C";
            var st3 = new Student();
            st3.FirstName = "X";
            st3.LastName = "Z";
            studentList.Add(st1);
            studentList.Add(st2);
            studentList.Add(st3);

            Console.WriteLine(studentList);
            Console.WriteLine(studentList.Remove(new Student(){FirstName = "X", LastName = "Z"}));
            Console.WriteLine(studentList.Remove(st3));
            studentList.Add(st3);
            studentList.Insert(2, st2);
            Console.WriteLine(studentList);
            Console.WriteLine("Index of new student: " + studentList.IndexOf(new Student() { FirstName = "X", LastName = "Z" }));
            Console.WriteLine("Index of st3: " + studentList.IndexOf(st3));
        }
    }

    // Klasa nie musi implementować IEnumerable, bo jest on implementowany przez IList
    public class ListOfArrayList<T> : IList<T>
    {
        public static readonly int DefaultArraySize = 16;

        private int _arraySize;
        public int ArraySize
        {
            get { return _arraySize;}
            private set { _arraySize = Math.Max(1, value); }
        }

        private int _count = 0;
        public int Count
        {
            get { return _count; }
        }

        public bool IsReadOnly
        {
            get => false;
        }

        private List<ArrayList> items;

        public ListOfArrayList(int constArraySize)
        {
            ArraySize = constArraySize;
            items = new List<ArrayList>();
        }

        public ListOfArrayList() : this(DefaultArraySize) { }

        public void Add(T item)
        {
            if (item == null) return;
            int newItemArrayIndex = Count / ArraySize;
            ArrayList newItemArray;
            if (newItemArrayIndex == items.Count)
            {
                newItemArray = new ArrayList(ArraySize);
                items.Add(newItemArray);
            }
            else
            {
                newItemArray = items[newItemArrayIndex];
            }

            newItemArray.Add(item);
            _count++;
        }

        public void Clear()
        {
            items.Clear();
        }

        public bool Contains(T item)
        {
            return items.Any(arr => arr.Contains(item));
        }

        public bool Remove(T item)
        {
            if (Contains(item))
            {
                RemoveAt(IndexOf(item));
                return true;
            }

            return false;
        }

        public int IndexOf(T item)
        {
            for (int i = 0; i < items.Count; i++)
            {
                int index = items[i].IndexOf(item);
                if (index != -1)
                {
                    return i * ArraySize + index;
                }
            }

            return -1;
        }

        // This implementation can be improved
        public void Insert(int index, T item)
        {
            int arrIndex = index / ArraySize;
            int indexInArr = index % ArraySize;
            T temp;

            if (items[arrIndex].Count == ArraySize)
            {
                temp = (T)items[arrIndex][ArraySize - 1];
                items[arrIndex].RemoveAt(ArraySize - 1);
                items[arrIndex].Insert(indexInArr, item);
            }
            else
            {
                items[arrIndex].Insert(indexInArr, item);
                return;
            }

            bool stopOffsettingFlag = true;
            while (stopOffsettingFlag)
            {
                arrIndex++;
                if (items.Count == arrIndex)
                {
                    items.Add(new ArrayList(ArraySize));
                }

                if (items[arrIndex].Count == ArraySize)
                {
                    T tempTemp = (T)items[arrIndex][ArraySize - 1];
                    items[arrIndex].RemoveAt(ArraySize - 1);
                    items[arrIndex].Insert(0, temp);
                    temp = tempTemp;
                }
                else
                {
                    // Item will be inserted even if ArrayList.Count == 0
                    items[arrIndex].Insert(0, temp);
                    stopOffsettingFlag = false;
                }
            }

            _count++;
        }

        public void RemoveAt(int index)
        {
            int arrIndex = index / ArraySize;
            int indexInArr = index % ArraySize;

            items[arrIndex].RemoveAt(indexInArr);

            for (int i = arrIndex; i < items.Count - 1; i++)
            {
                if (items[i+1].Count > 0)
                {
                    items[i].Add(items[i + 1][0]);
                    items[i + 1].RemoveAt(0);
                }
                else
                {
                    break;
                }
            }

            _count--;
        }

        public T this[int index]
        {
            get { return (T)items[index / ArraySize][index % ArraySize]; }
            set { items[index / ArraySize][index % ArraySize] = value; }
        }

        public void Trim()
        {
            items = items.Where(arr => arr.Count > 0).ToList();
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            foreach (var arr in items)
            {
                if (arr.Count == 0) break;
                
                foreach (var item in arr)
                {
                    sb.Append(item);
                    sb.Append(" ");
                }

                sb.Append("\n");
            }

            return sb.ToString();
        }

        public IEnumerator<T> GetEnumerator()
        {
            foreach (ArrayList arr in items.Where(arr => arr.Count > 0))
            {
                foreach (T item in arr)
                {
                    yield return item;
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public static ListOfArrayList<T> operator+(ListOfArrayList<T> collection, IEnumerable<T> otherCollection)
        {
            foreach (T item in otherCollection)
            {
                collection.Add(item);
            }

            return collection;
        }


        // WILL NOT BE IMPLEMENTED
        public void CopyTo(T[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }
    }

    class Student
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        private int age;
        public int Age
        {
            get => age;
            set => age = Math.Max(0, value);
        }
        public int[] Grades { get; set; }

        public override string ToString()
        {
            return $"{FirstName} {LastName}";
        }
    }
}
