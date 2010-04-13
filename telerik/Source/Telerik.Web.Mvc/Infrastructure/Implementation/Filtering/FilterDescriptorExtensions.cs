// (c) Copyright 2002-2010 Telerik 
// This source is subject to the GNU General Public License, version 2
// See http://www.gnu.org/licenses/gpl-2.0.html. 
// All other rights reserved.
namespace Telerik.Web.Mvc.Infrastructure.Implementation
{
    internal static class FilterDescriptorExtensions
    {
        internal static bool IsActive(this FilterDescriptor filter)
        {
            object value = filter.Value;
            if (value == null)
            {
                return false;
            }

            string valueAsString = value as string;
            return valueAsString == null || !string.IsNullOrEmpty(valueAsString);
        }
    }
}