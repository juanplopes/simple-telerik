using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Simple.Reflection;
using Simple.Entities;
using FluentNHibernate.Mapping;

namespace Simple.Web.Mvc.Telerik.Sample.Model
{
    [Serializable]
    public partial class TUser : Entity<TUser, ITUserService>
    {
        public virtual Int32? Id { get; set; }
        public virtual String Name { get; set; }
        public virtual DateTime? BirthDate { get; set; }
        public virtual Decimal? Wage { get; set; }
        public virtual Double? Height { get; set; }
        public virtual Double? Weight { get; set; }


        static TUser()
        {
            Identifiers.AddID(x => x.Id);
        }

        public TUser() { }
        public TUser(Int32? Id)
            : this()
        {
            this.Id = Id;
        }

        public class Map : ClassMap<TUser>
        {
            public Map()
            {
                Id(x => x.Id).GeneratedBy.Identity();
                Map(x => x.Name);
                Map(x => x.BirthDate);
                Map(x => x.Wage);
                Map(x => x.Height);
                Map(x => x.Weight);
            }
        }
        

    }
}

