namespace Telerik.Web.Mvc.UI
{
    using System.Collections.Generic;
using System.Web.Mvc;

    public interface IDropDownRenderable
    {
        string Id { get; }

        string Name { get; }

        ViewContext ViewContext { get; }

        IDictionary<string, object> HtmlAttributes { get; }

        IList<DropDownItem> Items { get; }

        int SelectedIndex { get; }
    }
}
