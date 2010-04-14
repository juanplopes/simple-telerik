using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Simple;
using Simple.IO;
using System.Reflection;
using Simple.Services;
using System.IO;
using FluentNHibernate.Cfg.Db;

namespace Simple.Web.Mvc.Telerik.Sample.Config
{
    public class Default : ConfigDef
    {
        public Default() : base(Development) { }

        public override void ConfigClient()
        {
            Config(x => x.DefaultHost());
        }

        public override void ConfigServer()
        {
            Config(x => x.NHibernate().FromXmlString(ConfigSource.NHibernate));
            Config(x => x.MappingFromAssemblyOf<Default>());
        }

        protected override void InitLocations(FileLocator paths)
        {
        }
    }
}
