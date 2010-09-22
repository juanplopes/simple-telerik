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
    using Infrastructure;

    class GridBoundColumnSerializer : GridColumnSerializer
    {
        private readonly IGridBoundColumn column;

        public GridBoundColumnSerializer(IGridBoundColumn column)
            : base(column)
        {
            this.column = column;
        }

        public override IDictionary<string, object> Serialize()
        {
            var result = base.Serialize();

            FluentDictionary.For(result)
                .Add("title", column.Title)
                .Add("member", column.Member)
                .Add("type", column.MemberType.ToJavaScriptType())
                .Add("format", column.Format, () => column.Format.HasValue())
                .Add("groupable", column.Groupable, true);
#if MVC2
            FluentDictionary.For(result)
                .Add("readonly", column.ReadOnly, false)
                .Add("editor", column.EditorHtml, () => column.Grid.Editing.Enabled && column.Grid.IsClientBinding && !column.ReadOnly);
#endif
            SerializeFilters(result);

            SerializeOrder(result);

            SerializeValues(result);
            
            return result;
        }
        
        private void SerializeOrder(IDictionary<string, object> result)
        {
            SortDescriptor sortDescriptor = column.Grid
                .DataProcessor
                .SortDescriptors
                .FirstOrDefault(s => s.Member == column.Member);
            
            if (sortDescriptor != null)
            {
                result["order"] = sortDescriptor.SortDirection == ListSortDirection.Ascending ? "asc" : "desc";
            }
        }
        
        private void SerializeValues(IDictionary<string, object> result)
        {
            if (column.MemberType != null && column.MemberType.IsEnum)
            {
                var values = new Dictionary<string, object>();
                
                foreach (var value in Enum.GetValues(column.MemberType))
                {
                    values[Enum.GetName(column.MemberType, value)] = value;
                }

                result["values"] = values;
            }
        }

        private void SerializeFilters(IDictionary<string, object> result)
        {
            var filtersForTheColumn = column.Grid.DataProcessor.FilterDescriptors.SelectRecursive(descriptor =>
            {
                var compositeDescriptor = descriptor as CompositeFilterDescriptor;
                return compositeDescriptor != null ? compositeDescriptor.FilterDescriptors : null;
            })
           .OfType<FilterDescriptor>()
           .Where(descriptor => descriptor.Member == column.Member);

            if (filtersForTheColumn.Any())
            {
                var filters = new List<IDictionary<string, object>>();

                filtersForTheColumn.Each(filter =>
                {
                    filters.Add(new Dictionary<string, object>
                    {
                        {"operator", filter.Operator.ToToken()},
                        {"value", filter.Value}
                    });
                });

                result["filters"] = filters;
            }
        }
    }
}
