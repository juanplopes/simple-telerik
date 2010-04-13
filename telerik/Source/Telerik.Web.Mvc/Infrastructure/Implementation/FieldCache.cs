// (c) Copyright 2002-2010 Telerik 
// This source is subject to the GNU General Public License, version 2
// See http://www.gnu.org/licenses/gpl-2.0.html. 
// All other rights reserved.

namespace Telerik.Web.Mvc.Infrastructure.Implementation
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    public class FieldCache : CacheBase<RuntimeTypeHandle, IEnumerable<FieldInfo>>, IFieldCache
    {
        private const BindingFlags Flags = BindingFlags.Public | BindingFlags.Instance | BindingFlags.GetField | BindingFlags.SetField;

        public IEnumerable<FieldInfo> GetFields(Type type)
        {
            Guard.IsNotNull(type, "type");

            return GetOrCreate(type.TypeHandle, () => type.GetFields(Flags).Where(field => !field.IsInitOnly));
        }
    }
}