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
    using System.Web.Mvc;
    using System.Web.Routing;

    using Extensions;

    public class AuthorizeAttributeCache : CacheBase<RuntimeTypeHandle, IDictionary<string, IEnumerable<AuthorizeAttribute>>>, IAuthorizeAttributeCache
    {
        private static readonly Type authorizeAttributeType = typeof(AuthorizeAttribute);

        private readonly IControllerTypeCache controllerTypeCache;
        private readonly IActionMethodCache actionMethodCache;

        public AuthorizeAttributeCache(IControllerTypeCache controllerTypeCache, IActionMethodCache actionMethodCache)
        {
            Guard.IsNotNull(controllerTypeCache, "controllerTypeCache");
            Guard.IsNotNull(actionMethodCache, "actionMethodCache");

            this.controllerTypeCache = controllerTypeCache;
            this.actionMethodCache = actionMethodCache;
        }

        public IEnumerable<AuthorizeAttribute> GetAuthorizeAttributes(RequestContext requestContext, string controllerName, string actionName)
        {
            Guard.IsNotNull(requestContext, "requestContext");
            Guard.IsNotNullOrEmpty(controllerName, "controllerName");
            Guard.IsNotNullOrEmpty(actionName, "actionName");

            Type controllerType = controllerTypeCache.GetControllerType(requestContext, controllerName);

            IDictionary<string, IEnumerable<AuthorizeAttribute>> map = GetOrCreate(controllerType.TypeHandle, () => GetInternal(controllerType, actionMethodCache.GetAllActionMethods(requestContext, controllerName)));

            IEnumerable<AuthorizeAttribute> attributes;

            map.TryGetValue(actionName, out attributes);

            return attributes ?? new List<AuthorizeAttribute>();
        }

        private static IDictionary<string, IEnumerable<AuthorizeAttribute>> GetInternal(ICustomAttributeProvider controllerType, IEnumerable<KeyValuePair<string, IList<MethodInfo>>> actionMethods)
        {
            IDictionary<string, IEnumerable<AuthorizeAttribute>> attributes = new Dictionary<string, IEnumerable<AuthorizeAttribute>>(StringComparer.OrdinalIgnoreCase);

            IEnumerable<AuthorizeAttribute> controllerAuthorizeAttribute = controllerType.GetCustomAttributes(authorizeAttributeType, true)
                                                                                         .OfType<AuthorizeAttribute>();

            foreach (KeyValuePair<string, IList<MethodInfo>> pair in actionMethods)
            {
                IList<AuthorizeAttribute> actionAttributes = new List<AuthorizeAttribute>(controllerAuthorizeAttribute);

                foreach (MethodInfo method in pair.Value)
                {
                    actionAttributes.AddRange(method.GetCustomAttributes(authorizeAttributeType, true).OfType<AuthorizeAttribute>());
                }

                attributes.Add(pair.Key, actionAttributes.OrderBy(a => a.Order));
            }

            return attributes;
        }
    }
}