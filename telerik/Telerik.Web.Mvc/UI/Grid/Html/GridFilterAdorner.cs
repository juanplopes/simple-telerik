namespace Telerik.Web.Mvc.UI
{
    using System.Linq;
    using Telerik.Web.Mvc;
    using Telerik.Web.Mvc.Extensions;
    using Telerik.Web.Mvc.Infrastructure;

    public class GridFilterAdorner : IHtmlAdorner
    {
        private readonly IGridBoundColumn column;

        public GridFilterAdorner(IGridBoundColumn column)
        {
            this.column = column;
        }

        public void ApplyTo(IHtmlNode target)
        {
            var filtered = column.Grid.DataProcessor.FilterDescriptors.SelectRecursive(filter =>
            {
                CompositeFilterDescriptor compositeDescriptor = filter as CompositeFilterDescriptor;

                if (compositeDescriptor != null)
                {
                    return compositeDescriptor.FilterDescriptors;
                }

                return null;
            })
            .Where(filter => filter is FilterDescriptor)
            .OfType<FilterDescriptor>()
            .Any(filter => filter.Member.IsCaseInsensitiveEqual(column.Member));

            var wrapper = new HtmlTag("div")
                .AddClass("t-grid-filter", "t-state-default")
                .ToggleClass("t-active-filter", filtered);

            wrapper.AppendTo(target);

            var icon = new HtmlTag("span").AddClass("t-icon", "t-filter");
            icon.AppendTo(wrapper);
        }
    }
}
