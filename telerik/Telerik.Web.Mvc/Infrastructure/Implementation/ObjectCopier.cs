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

    using Extensions;

    public class ObjectCopier : IObjectCopier
    {
        private readonly IFieldCache fieldCache;
        private readonly IPropertyCache propertyCache;

        public ObjectCopier(IFieldCache fieldCache, IPropertyCache propertyCache)
        {
            Guard.IsNotNull(fieldCache, "fieldCache");
            Guard.IsNotNull(propertyCache, "propertyCache");

            this.fieldCache = fieldCache;
            this.propertyCache = propertyCache;
        }

        public void Copy(object source, object destination, params string[] excludedMembers)
        {
            Guard.IsNotNull(source, "source");
            Guard.IsNotNull(destination, "destination");

            bool hasExcludedMembers = ((excludedMembers != null) && (excludedMembers.Length > 0));
            Type sourceType = source.GetType();

            IEnumerable<FieldInfo> fields = fieldCache.GetFields(sourceType);

            if (hasExcludedMembers)
            {
                fields = fields.Where(field => !excludedMembers.Any(name => field.Name.IsCaseInsensitiveEqual(name)));
            }

            foreach (FieldInfo field in fields)
            {
                field.SetValue(destination, field.GetValue(source));
            }

            IEnumerable<PropertyInfo> properties = propertyCache.GetProperties(sourceType);

            if (hasExcludedMembers)
            {
                properties = properties.Where(property => !excludedMembers.Any(name => property.Name.IsCaseInsensitiveEqual(name)));
            }

            foreach (PropertyInfo property in properties)
            {
                if (property.GetIndexParameters().Length == 0)
                {
                    property.SetValue(destination, property.GetValue(source, null), null);
                }
            }
        }
    }
}