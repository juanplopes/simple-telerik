<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Examples.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content contentPlaceHolderID="MainContent" runat="server">

<h3>Customers</h3>

<%= Html.Telerik().Grid((IEnumerable<Customer>)ViewData["Customers"])
        .Name("Customers")
        .Columns(columns =>
        {
            columns.Bound(c => c.CustomerID).Width(130);
            columns.Bound(c => c.CompanyName).Width(250);
            columns.Bound(c => c.ContactName);
            columns.Bound(c => c.Country).Width(200);
        })
        .DataBinding(dataBinding => dataBinding.Ajax().Select("_SelectionClientSide_Customers", "Grid"))
        .Pageable()
        .Sortable()
        .Selectable()
        .ClientEvents(events => events.OnRowSelected("onRowSelected"))
        .RowAction(row => 
        {
            row.Selected = row.DataItem.CustomerID.Equals(ViewData["id"]);
        })
        
%>


<h3>Orders (<span id="customerID"><%= ViewData["id"] %></span>)</h3>

<%= Html.Telerik().Grid((IEnumerable<Order>)ViewData["Orders"])
        .Name("Orders")
        .Columns(columns=>
        {
            columns.Bound(c => c.OrderID).Width(100);
            columns.Bound(c => c.OrderDate).Width(200).Format("{0:dd/MM/yyyy}");
            columns.Bound(c => c.ShipAddress);
            columns.Bound(c => c.ShipCity).Width(200);
        })
        .DataBinding(dataBinding => dataBinding.Ajax().Select("_SelectionClientSide_Orders", "Grid", new { customerID = "ALFKI" }))
        .Pageable()
        .Sortable()
        
%>
<script type="text/javascript">
    function onRowSelected(e) {
        var ordersGrid = $('#Orders').data('tGrid');
        var customerID = e.row.cells[0].innerHTML; 
        
        // update ui text
        $('#customerID').text(customerID);

        // rebind the related grid
        ordersGrid.rebind({
            customerID: customerID
        });
    }
</script>
</asp:Content>


