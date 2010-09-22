// (c) Copyright 2002-2010 Telerik 
// This source is subject to the GNU General Public License, version 2
// See http://www.gnu.org/licenses/gpl-2.0.html. 
// All other rights reserved.

namespace Telerik.Web.Mvc.UI
{
    using System.Collections.Generic;
    using System.Linq;
    using Telerik.Web.Mvc.Extensions;
    using Telerik.Web.Mvc.Infrastructure;

    class GridColumnSerializer : IGridColumnSerializer
    {
        private readonly IGridColumn column;
        
        public GridColumnSerializer(IGridColumn column)
        {
            this.column = column;
        }

        public virtual IDictionary<string, object> Serialize()
        {
            IDictionary<string, object> result = new Dictionary<string, object>();
            FluentDictionary.For(result)
                  .Add("attr", column.HtmlAttributes.ToAttributeString(), () => column.HtmlAttributes.Any());
            
            if (column.ClientTemplate.HasValue() && column.Grid.IsClientBinding)                  
            {
                string template = column.Grid.IsSelfInitialized ? column.ClientTemplate.Replace("<", "%3c").Replace(">", "%3e") : column.ClientTemplate;
                
                result.Add("template", template);
            }

            return result;
        }
    }
}
