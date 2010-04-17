// (c) Copyright 2002-2010 Telerik 
// This source is subject to the GNU General Public License, version 2
// See http://www.gnu.org/licenses/gpl-2.0.html. 
// All other rights reserved.
namespace Telerik.Web.Mvc.UI.Fluent
{
    using Infrastructure;

    public class GridDataKeyBuilder<TModel, TValue> : IHideObjectMembers
        where TModel : class
    {
        private readonly GridDataKey<TModel, TValue> dataKey;

        public GridDataKeyBuilder(GridDataKey<TModel, TValue> dataKey)
        {
            this.dataKey = dataKey;
        }

        public void RouteKey(string value)
        {
            Guard.IsNotNullOrEmpty(value, "value");

            dataKey.RouteKey = value;
        }
    }
}
