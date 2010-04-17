// (c) Copyright 2002-2010 Telerik 
// This source is subject to the GNU General Public License, version 2
// See http://www.gnu.org/licenses/gpl-2.0.html. 
// All other rights reserved.

namespace Telerik.Web.Mvc.Infrastructure.Implementation
{
    using System;
    using System.Reflection;

    public class ReflectedAuthorizeAttributeCache : CacheBase<RuntimeTypeHandle, ConstructorInfo>, IReflectedAuthorizeAttributeCache
    {
        private readonly IAuthorizeAttributeBuilder builder;

        public ReflectedAuthorizeAttributeCache(IAuthorizeAttributeBuilder builder)
        {
            Guard.IsNotNull(builder, "builder");

            this.builder = builder;
        }

        public IAuthorizeAttribute GetAttribute(Type attributeType)
        {
            Guard.IsNotNull(attributeType, "attributeType");

            ConstructorInfo ctor = GetOrCreate(attributeType.TypeHandle, () => builder.Build(attributeType));
            IAuthorizeAttribute attribute = ctor.Invoke(new object[0]) as IAuthorizeAttribute;

            return attribute;
        }
    }
}