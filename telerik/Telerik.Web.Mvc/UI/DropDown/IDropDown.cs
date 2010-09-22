namespace Telerik.Web.Mvc.UI
{
    using System.Collections.Generic;

    public interface IDropDown : IDataBoundDropDown
    {
        IList<DropDownItem> Items { get; }

        int SelectedIndex { get; set; }

        IDictionary<string, object> DropDownHtmlAttributes { get; }

        Effects Effects { get; }

        DropDownClientEvents ClientEvents { get; }
    }
}
