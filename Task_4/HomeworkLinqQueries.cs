using System;
using System.Collections.Generic;
using System.Linq;
using Task_1;


namespace Task_4
{
    public class HomeworkLinqQueries
    {
        public static string[] Linq1(int[] intArray)
        {
            var result = intArray.GroupBy(i => i).Select(t => new string[2] {t.Key.ToString(), t.Count().ToString()})
                .OrderBy(num => num[0]).Select(i => "Broj" + i[0] + " ponavlja se " + i[1] + " puta");

            return result.ToArray();
        }

        public static University[] Linq2_1(University[] universityArray) => universityArray.Where(u => u.Students.All(s => s.Gender == Gender.Male)).ToArray();

        public static University[] Linq2_2(University[] universityArray)
        {
            float average = universityArray.Select(s => new object[2] {s.Name, s.Students.Length})
                .Average(uni => (float) uni[1]);

            return universityArray.Where(uni => uni.Students.Length < average).ToArray();
        }

        public static Student[] Linq2_3(University[] universityArray) =>
            universityArray.SelectMany(t => t.Students).Distinct().ToArray();
    
        public static Student[] Linq2_4(University[] universityArray)
        {
            Student[] maleUniversitiesStudents = universityArray
                .Where(u => u.Students.All(s => s.Gender == Gender.Male)).SelectMany(s => s.Students).Distinct()
                .ToArray();

            Student[] femaleUniversititesStudents = universityArray
                .Where(u => u.Students.All(s => s.Gender == Gender.Female)).SelectMany(s => s.Students).Distinct()
                .ToArray();

            return maleUniversitiesStudents.Union(femaleUniversititesStudents).ToArray();


        }

        public static Student[] Linq2_5(University[] universityArray) => universityArray.SelectMany(uni => uni.Students)
            .GroupBy(s => s.Jmbag).Select(s => new object[2] {s, s.Count()}).Where(s => (int) s[1] > 1)
            .Select(s => (Student) s[0]).Distinct().ToArray();

    }
}