using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using Microsoft.VisualBasic.CompilerServices;

namespace test_03
{
    class Person
    {
        string name;
        int age;
        public Person(string name, int age)
        {
            this.name = name;
            this.age = age;
        }

        public string Name
        {
            get => name;
            set => name = value;
        }

        public int Age
        {
            get => age;
            set => age = value;
        }

        public override string ToString()
        {
            return name + " " + age;
        }

        public static bool operator ==(Person d1, Person d2)
        {
            bool r = false;
            if (d1.name == d2.name && d1.age == d2.age ) 
                r = true;
            return r;
        }
        public static bool operator !=(Person d1, Person d2)
        {
            return d1.name != d2.name || d1.age != d2.age;
        }
        public static bool operator >(Person person1, Person person2)
        {
            return person1.Age > person2.Age;
        }
        public static bool operator <(Person person1, Person person2)
        {
            return person1.age < person2.Age;
        }


    }
    class MyInt :IComparer
    {
        Person[] m;
        int count;
        public MyInt(int capacity)
        {
            this.m = new Person[capacity];
            count = 0;
        }
        public void Add(Person item)
        {
            if (count == m.Length)
            {
                Person[] temp = new Person[m.Length + 1];
                Array.Copy(m, temp, m.Length);
                m = temp;
            }
            m[count] = item;
            count++;
        }
        public int Capacity
        {
            get { return m.Length; }
        }
        public int Count
        {
            get { return count; }
        }
        public Person this[int index]
        {
            get { return m[index]; }
        }
        public IEnumerator GetEnumerator()
        {
            for (int i = 0; i< count; i++)
            {
                yield return m[i];
            }
        }
        public bool Contains(Person item)
        {
            for (int i = 0; i<count; i++)
            {
                if (m[i] == item) return true;
            }
            return false;
        }
        public void Sort()
        {
            Person temp;
            for (int i = 0; i < m.Length; i++)
            {
                for (int j = i+1; j < m.Length; j++)
                {
                    if (m[i].Age > m[j].Age)
                    {
                        temp = m[i];
                        m[i] = m[j];
                        m[j] = temp;
                    }
                }   
            }
            //Array.Sort(m, 0, count, this);
        }

        public void SortByOpertator()
        {
            Person temp;
            for (int i = 0; i < m.Length; i++)
            {
                for (int j = i + 1; j < m.Length; j++)
                {
                    if (m[i] > m[j])
                    {
                        temp = m[i];
                        m[i] = m[j];
                        m[j] = temp;
                    }
                }
            }
        }

        public void Sort(IComparer comparer)
        {
            Person temp;
            for (int i = 0; i < m.Length; i++)
            {
                for (int j = i + 1; j < m.Length; j++)
                {
                    if (comparer.Compare(m[i], m[j])>0)
                    {
                        temp = m[i];
                        m[i] = m[j];
                        m[j] = temp;
                    }
                }
            }
            //Array.Sort(m,0,count, comparer);
        }
        public void Sort(Comparison<Person> comparison)
        {
            Person temp;
            for (int i = 0; i < m.Length; i++)
            {
                for (int j = i + 1; j < m.Length; j++)
                {
                    if (comparison.Invoke(m[i],m[j])>0)
                    {
                        temp = m[i];
                        m[i] = m[j];
                        m[j] = temp;
                    }
                }
            }
        }


        public int Compare(object? x, object? y)
        {
            var _x = (Person)x;
            var _y = (Person)y;
            return _x.Age.CompareTo(_y.Age);
        }
    }
    class Program
    {

        static void Main(string[] args)
        {
            MyInt arr = new MyInt(2);
            Person Roman = new Person("Roman", 21);
            Person Stas = new Person("Stas", 24);
            Person Andrey = new Person("Andrey", 25);
            Person Igor = new Person("Igor", 20);

            arr.Add(Roman);
            arr.Add(Stas);
            arr.Add(Andrey);
            arr.Add(Igor);

            int Helper(Person person1, Person person2)
            {
                return person1.Age.CompareTo(person2.Age);
            }

        //arr.Sort2((a,b)=> a.Age.CompareTo(b.Age));
            arr.Sort(Helper);

            foreach (var el in arr)
            {
                Console.WriteLine(el);
            }

            
        }
    }
}
