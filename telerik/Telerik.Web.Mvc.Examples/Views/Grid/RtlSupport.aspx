<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<IEnumerable<Order>>" %>


<asp:Content contentPlaceHolderID="MainContent" runat="server">

<%= Html.Telerik().Grid<Order>(Model)
        .Name("Grid")
        .HtmlAttributes(new { @class = "t-grid-rtl" })
        .Columns(columns =>
		{
            columns.Bound(o => o.OrderID).Width(100);
            columns.Bound(o => o.Customer.ContactName).Width(200);
            columns.Bound(o => o.ShipAddress);
            columns.Bound(o => o.OrderDate).Format("{0:MM/dd/yyyy}").Width(120);
        })
        .DataBinding(dataBinding => dataBinding.Ajax().Select("_RtlSupport", "Grid"))
        .Scrollable()
        .Sortable()
        .Pageable()
        .Filterable()
%>

</asp:Content>