using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Simple.Web.Mvc.Telerik.Sample.Model
{
    public class UserSample
    {
        public static void Init()
        {
            string[] names = new string[] { "Aaron", "Abbott", "Abel", "Abner", "Abraham", "Adam", "Addison", "Adler", "Adley", "Adrian", "Aedan", "Aiken", "Alan", "Alastair", "Albern", "Albert", "Albion", "Alden", "Aldis", "Aldrich", "Alexander", "Alfie", "Alfred", "Algernon", "Alston", "Alton", "Alvin", "Ambrose", "Amery", "Amos", "Andrew", "Angus", "Ansel", "Anthony", "Archer", "Archibald", "Arlen", "Arnold", "Arthur", "Arvel", "Atwater", "Atwood", "Aubrey", "Austin", "Avery", "Axel", "Baird", "Baldwin", "Barclay", "Barnaby", "Baron", "Barrett", "Barry", "Bartholomew", "Basil" };
            Random random = new Random();

            for (int i = 0; i < 500; i++)
            {
                string name = names[random.Next(names.Length)] + " " + names[random.Next(names.Length)] + " " + names[random.Next(names.Length)];
                DateTime birthdate = new DateTime(random.Next(1940, 2001), random.Next(1, 13), random.Next(1, 29));
                decimal wage = Convert.ToDecimal(random.Next(0, 6001));
                double height = Convert.ToDouble((random.Next(120, 210))) / Convert.ToDouble(100);
                double weight = (random.Next(500, 1500)) / 10;

                AddUser(name, birthdate, wage, height, weight);
            }

        }

        public static void AddUser(string name, DateTime birthDate, decimal wage, double height, double weight)
        {
            TUser user = new TUser();
            user.BirthDate = birthDate;
            user.Height = height;
            user.Name = name;
            user.Wage = wage;
            user.Weight = weight;

            user.SaveOrUpdate();
        }
    }
}
