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
    public partial class TGroup : Entity<TGroup, ITGroupService>
    {
        public virtual Int32? Id { get; set; }
        public virtual String Name { get; set; }
        public virtual IList<TUser> Users { get; set; }

        static TGroup()
        {
            Identifiers.Add(x => x.Id);
        }

        public TGroup() { }
        public TGroup(Int32? Id)
            : this()
        {
            this.Id = Id;
        }

        public override string ToString()
        {
            return Name;
        }

        public class Map : ClassMap<TGroup>
        {
            public Map()
            {
                Id(x => x.Id).GeneratedBy.Identity();
                Map(x => x.Name);
                HasManyToMany(x => x.Users);
            }
        }
        

    }
}

