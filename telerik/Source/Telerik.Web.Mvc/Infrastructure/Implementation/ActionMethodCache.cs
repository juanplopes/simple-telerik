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

    public class ActionMethodCache : CacheBase<RuntimeTypeHandle, IDictionary<string, IList<MethodInfo>>>, IActionMethodCache
    {
        private static readonly Type actionResultType = typeof(ActionResult);
        private static readonly Type nonActionAttributeType = typeof(NonActionAttribute);
        private static readonly Type actionNaneAttributeType = typeof(ActionNameAttribute);

        private readonly IControllerTypeCache controllerTypeCache;

        public ActionMethodCache(IControllerTypeCache controllerTypeCache)
        {
            Guard.IsNotNull(controllerTypeCache, "controllerTypeCache");

            this.controllerTypeCache = controllerTypeCache;
        }

        public IList<MethodInfo> GetActionMethods(RequestContext requestContext, string controllerName, string actionName)
        {
            Guard.IsNotNull(requestContext, "requestContext");
            Guard.IsNotNullOrEmpty(controllerName, "controllerName");
            Guard.IsNotNullOrEmpty(actionName, "actionName");

            IDictionary<string, IList<MethodInfo>> allActionMethods = GetAllActionMethods(requestContext, controllerName);
            IList<MethodInfo> actionMethods;

            return allActionMethods.TryGetValue(actionName, out actionMethods) ? actionMethods : null;
        }

        public IDictionary<string, IList<MethodInfo>> GetAllActionMethods(RequestContext requestContext, string controllerName)
        {
            Guard.IsNotNull(requestContext, "requestContext");
            Guard.IsNotNullOrEmpty(controllerName, "controllerName");

            Type controllerType = controllerTypeCache.GetControllerType(requestContext, controllerName);

            return GetOrCreate(controllerType.TypeHandle, () => GetInternal(controllerType));
        }

        private static IDictionary<string, IList<MethodInfo>> GetInternal(Type controllerType)
        {
            Func<MethodInfo, bool> isActionMethod = method => actionResultType.IsAssignableFrom(method.ReturnType) && !method.IsDefined(nonActionAttributeType, false);

            IDictionary<string, IList<MethodInfo>> actionMethods = new Dictionary<string, IList<MethodInfo>>(StringComparer.OrdinalIgnoreCase);

            foreach (MethodInfo method in controllerType.GetMethods(BindingFlags.Public | BindingFlags.Instance).Where(isActionMethod))
            {
                string actionName = method.GetCustomAttributes(actionNaneAttributeType, false)
                                          .OfType<ActionNameAttribute>()
                                          .Select(attribute => attribute.Name)
                                          .SingleOrDefault() ?? method.Name;

                IList<MethodInfo> methods;

                if (!actionMethods.TryGetValue(actionName, out methods))
                {
                    methods = new List<MethodInfo>();
                    actionMethods.Add(actionName, methods);
                }

                methods.Add(method);
            }

            return actionMethods;
        }
    }
}