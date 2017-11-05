using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Task_1
{
    public class Student
    {
        public string Name { get; set; }
        public string Jmbag { get; set; }
        public Gender Gender { get; set; }

        public Student(string name, string jmbag)
        {
            Name = name;
            Jmbag = jmbag;
        }

        public override bool Equals(Object obj)
        {
            return Jmbag == (obj as Student)?.Jmbag;
        }

        public override int GetHashCode()
        {
            return Jmbag.GetHashCode();
        }

        public static bool operator ==(Student s1, Student s2) => s1.Equals(s2);

        public static bool operator !=(Student s1, Student s2) => !s1.Equals(s2);
    }


    public enum Gender
    {
        Male,
        Female
    }

}




