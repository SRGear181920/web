using System;
using System.Collections.Generic;
using System.IO;

namespace HHRU
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Vacancy> lowSalary = new List<Vacancy>();
            List<Vacancy> highSalary = new List<Vacancy>();
            var path = "https://api.hh.ru";
            Console.WriteLine("Подождите, идёт процесс получения вакансий...");
            
            var client = new Client(path, 100, 2000);
            foreach (var vacancy in client.Vacancies)
            {
                if (vacancy.Salary.GetSalary() < 15000)
                    lowSalary.Add(vacancy);
                else if (vacancy.Salary.GetSalary() >= 120000)
                    highSalary.Add(vacancy);
            }
            Console.WriteLine("Вакансии с зарплатой до 15000:\n");
            foreach (var vacancy in lowSalary) Console.WriteLine(vacancy + "\n");
            Console.WriteLine("Вакансии с зарплатой от 120000:\n");
            foreach (var vacancy in highSalary) Console.WriteLine(vacancy + "\n");
            Console.ReadKey();
        }
    }
}
