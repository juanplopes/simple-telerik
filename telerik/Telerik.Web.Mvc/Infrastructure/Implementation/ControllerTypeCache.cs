// (c) Copyright 2002-2010 Telerik 
// This source is subject to the GNU General Public License, version 2
// See http://www.gnu.org/licenses/gpl-2.0.html. 
// All other rights reserved.

namespace Telerik.Web.Mvc.Infrastructure.Implementation
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Reflection;
    using System.Text;
    using System.Web.Compilation;
    using System.Web.Mvc;
    using System.Web.Routing;

    using Extensions;

    public class ControllerTypeCache : IControllerTypeCache
    {
        private readonly object syncLock = new object();
        private IDictionary<string, ILookup<string, Type>> cache;

        private Func<IEnumerable<Assembly>> referencedAssemblies = () => BuildManager.GetReferencedAssemblies().Cast<Assembly>().Where(assembly => !assembly.GlobalAssemblyCache);

        public Func<IEnumerable<Assembly>> ReferencedAssemblies
        {
            [DebuggerStepThrough]
            get
            {
                return referencedAssemblies;
            }

            [DebuggerStepThrough]
            set
            {
                Guard.IsNotNull(value, "value");

                referencedAssemblies = value;
            }
        }

        public Type GetControllerType(RequestContext requestContext, string controllerName)
        {
            Guard.IsNotNull(requestContext, "requestContext");
            Guard.IsNotNullOrEmpty(controllerName, "controllerName");

            object routeNamespacesAsObject;
            Type match;

            if (requestContext != null && requestContext.RouteData.DataTokens.TryGetValue("Namespaces", out routeNamespacesAsObject))
            {
                IEnumerable<string> routeNamespacesAsStrings = routeNamespacesAsObject as IEnumerable<string>;

                if (routeNamespacesAsStrings != null)
                {
                    HashSet<string> routeNamespaces = new HashSet<string>(routeNamespacesAsStrings, StringComparer.OrdinalIgnoreCase);

                    match = GetControllerTypeWithinNamespaces(controllerName, routeNamespaces);

                    if (match != null)
                    {
                        return match;
                    }
                }
            }

            HashSet<string> defaultNamespaces = new HashSet<string>(ControllerBuilder.Current.DefaultNamespaces, StringComparer.OrdinalIgnoreCase);

            match = GetControllerTypeWithinNamespaces(controllerName, defaultNamespaces);

            return match ?? GetControllerTypeWithinNamespaces(controllerName, null);
        }

        private Type GetControllerTypeWithinNamespaces(string controllerName, IEnumerable<string> namespaces)
        {
            EnsureInitialized();

            IList<Type> matchingTypes = GetControllerTypes(controllerName, namespaces);

            switch (matchingTypes.Count)
            {
                case 1:
                    {
                        return matchingTypes[0];
                    }

                case 0:
                    {
                        return null;
                    }

                default:
                    {
                        StringBuilder controllerTypes = new StringBuilder();

                        foreach (Type matchedType in matchingTypes)
                        {
                            controllerTypes.AppendLine();
                            controllerTypes.Append(matchedType.FullName);
                        }

                        throw new InvalidOperationException("The controller name '{0}' is ambiguous between the following types:{1}".FormatWith(controllerName, controllerTypes.ToString()));
                    }
            }
        }

        private void EnsureInitialized()
        {
            if (cache == null)
            {
                lock (syncLock)
                {
                    if (cache == null)
                    {
                        IList<Type> controllerTypes = GetAllControllerTypes();

                        IEnumerable<IGrouping<string, Type>> groupedByName = controllerTypes.GroupBy(t => t.Name.Substring(0, t.Name.Length - "Controller".Length), StringComparer.OrdinalIgnoreCase);

                        cache = groupedByName.ToDictionary(g => g.Key, g => g.ToLookup(t => t.Namespace ?? string.Empty, StringComparer.OrdinalIgnoreCase), StringComparer.OrdinalIgnoreCase);
                    }
                }
            }
        }

        private IList<Type> GetControllerTypes(string controllerName, IEnumerable<string> namespaces)
        {
            IList<Type> matchingTypes = new List<Type>();

            ILookup<string, Type> namespaceLookup;

            if (cache.TryGetValue(controllerName, out namespaceLookup))
            {
                if (namespaces != null)
                {
                    foreach (string ns in namespaces)
                    {
                        matchingTypes.AddRange(namespaceLookup[ns]);
                    }
                }
                else
                {
                    foreach (IGrouping<string, Type> namespaceGroup in namespaceLookup)
                    {
                        matchingTypes.AddRange(namespaceGroup);
                    }
                }
            }

            return matchingTypes;
        }

        private IList<Type> GetAllControllerTypes()
        {
            IList<Type> controllerTypes = new List<Type>();

            Func<Type, bool> isController = type => type != null &&
                                                    type.Name.EndsWith("Controller", StringComparison.OrdinalIgnoreCase) &&
                                                    !type.IsAbstract &&
                                                    typeof(IController).IsAssignableFrom(type);

            foreach (Assembly assembly in ReferencedAssemblies())
            {
                Type[] types;

                try
                {
                    types = assembly.GetTypes();
                }
                catch (ReflectionTypeLoadException rte)
                {
                    types = rte.Types;
                }

                controllerTypes.AddRange(types.Where(isController));
            }

            return controllerTypes;
        }
    }
}