// (c) Copyright 2002-2010 Telerik 
// This source is subject to the GNU General Public License, version 2
// See http://www.gnu.org/licenses/gpl-2.0.html. 
// All other rights reserved.
namespace Telerik.Web.Mvc.UI
{
    using System.Collections.Generic;
    using System.Linq;
    using Extensions;
    using Telerik.Web.Mvc.Infrastructure;
    
    class GridActionColumnSerializer : GridColumnSerializer
    {
        private readonly IGridActionColumn column;
        
        public GridActionColumnSerializer(IGridActionColumn column) : base(column)
        {
            this.column = column;
        }

        public override IDictionary<string, object> Serialize()
        {
            var result = base.Serialize();
            var commands = new List<IDictionary<string,object>>();
            
            column.Commands.Each(c =>
            {
                var command = new Dictionary<string, object>();

                FluentDictionary.For(command)
                    .Add("name", c.Name)
                    .Add("attr", c.HtmlAttributes.ToAttributeString(), () => c.HtmlAttributes.Any())
                    .Add("buttonType", c.ButtonType.ToString())
                    .Add("imageAttr", c.ImageHtmlAttributes.ToAttributeString(), () => c.ImageHtmlAttributes.Any());
                
                commands.Add(command);
            });
        
            if (commands.Any())
            {
                result["commands"] = commands;
            }

            return result;
        }
    }
}
