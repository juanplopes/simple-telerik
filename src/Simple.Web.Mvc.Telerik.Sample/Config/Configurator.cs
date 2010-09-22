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
    public class Configurator : ConfigDef
    {
        public Configurator() : base(Development, null) { }


        public override ConfigDef ConfigClient()
        {
            Config(x => x.DefaultHost());
            return this;
        }

        public override ConfigDef ConfigServer()
        {
            Config(x => x.NHibernate().FromXmlString(ConfigSource.NHibernate));
            Config(x => x.MappingFromAssemblyOf<Configurator>());
            return this;
        }

        protected override void InitLocations(FileLocator paths)
        {
        }

        public override FileInfo FindKeyFile()
        {
            throw new NotImplementedException();
        }
    }
}
