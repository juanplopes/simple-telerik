using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Simple.Web.Mvc.Telerik.Sample.Model
{
    public class GroupSample
    {
        public static void Init()
        {
            var userCount = TUser.Count();
            var r = new Random();
            for (int i = 0; i < 10; i++)
            {
                var skip = r.Next(userCount - 100);
                var take = r.Next(100);
                new TGroup()
                {
                    Name = "grupo" + i,
                    Users = TUser.ListAll(skip, take)
                }.Save();
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
