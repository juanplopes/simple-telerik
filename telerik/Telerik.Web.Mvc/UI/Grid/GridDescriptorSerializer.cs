// (c) Copyright 2002-2010 Telerik 
// This source is subject to the GNU General Public License, version 2
// See http://www.gnu.org/licenses/gpl-2.0.html. 
// All other rights reserved.

namespace Telerik.Web.Mvc.UI
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;

    using Extensions;

    public static class GridDescriptorSerializer
    {
        private const string ColumnDelimiter = "~";
        private const string DirectionDelimiter = "-";
        private const string Ascending = "asc";
        private const string Descending = "desc";

        public static string Serialize<T>(IEnumerable<T> descriptors)
            where T : IDescriptor
        {
            if (!descriptors.Any())
            {
                return "~";
            }

            List<string> expressions = new List<string>();

            foreach (T descriptor in descriptors)
            {
                expressions.Add(Serialize(descriptor));
            }

            return string.Join(ColumnDelimiter, expressions.ToArray());
        }

        public static IList<T> Deserialize<T>(string from)
            where T : IDescriptor, new()
        {
            IList<T> result = new List<T>();

            if (!string.IsNullOrEmpty(from))
            {
                string[] components = from.Split(ColumnDelimiter.ToCharArray(), StringSplitOptions.RemoveEmptyEntries);

                if (components.Length > 0)
                {
                    foreach (string component in components)
                    {
                        string[] parts = component.Split(DirectionDelimiter.ToCharArray(), StringSplitOptions.RemoveEmptyEntries);

                        if (parts.Length == 2)
                        {
                            T descriptor = Deserialize<T>(parts);
                            result.Add(descriptor);
                        }
                        else if (parts.Length == 1)
                        {
                            if (parts[0] == "asc" || parts[0] == "desc")
                            {
                                T descriptor = new T
                                {
                                    SortDirection = parts[0].IsCaseInsensitiveEqual(Descending) ? ListSortDirection.Descending : ListSortDirection.Ascending
                                };

                                result.Add(descriptor);
                            }
                        }
                    }
                }
            }

            return result;
        }

        private static string Serialize<T>(T descriptor)
            where T : IDescriptor
        {
            return "{0}{1}{2}".FormatWith(descriptor.Member,
                            DirectionDelimiter,
                            descriptor.SortDirection == ListSortDirection.Ascending ? Ascending : Descending);
        }

        private static T Deserialize<T>(string[] from)
            where T : IDescriptor, new()
        {
            T descriptor = new T
            {
                Member = from[0],
                SortDirection = from[1].IsCaseInsensitiveEqual(Descending) ? ListSortDirection.Descending : ListSortDirection.Ascending
            };

            return descriptor;
        }
    }
}