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
            var result = intArray.GroupBy(i => i).Select(t => new string[2] { t.Key.ToString(), t.Count().ToString() })
                .OrderBy(num => num[0]).Select(i => "broj " + i[0] + " ponavlja se " + i[1] + " puta");

            return result.ToArray();
        }

        public static University[] Linq2_1(University[] universityArray) => universityArray.Where(u => u.Students.All(s => s.Gender == Gender.Male)).ToArray();

        public static University[] Linq2_2(University[] universityArray)
        {
            float average = universityArray.Select(s => new object[2] { s.Name, s.Students.Length })
                .Average(uni => float.Parse(uni[1].ToString()));

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

        public static Student[] Linq2_5(University[] universityArray)
        {
            List<string> moreThanOneCollegeJmbagsList = universityArray.SelectMany(t => t.Students).GroupBy(x => x.Jmbag).Select(x => new
            {
                x.Key,
                Count = x.Count()
            }).Where(t => (int)t.Count > 1).Select(i => i.Key).ToList();

            return universityArray.SelectMany(uni => uni.Students).Distinct().Where(s => moreThanOneCollegeJmbagsList.Contains(s.Jmbag)).ToArray();
        }
    }
}