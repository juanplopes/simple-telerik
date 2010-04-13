<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Examples.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content contentPlaceHolderID="MainContent" runat="server">

<h3>Select a customer to see his/her orders</h3>

<%= Html.Telerik().Grid((IEnumerable<Customer>)ViewData["Customers"])
        .Name("Customers")
        .DataKeys(dataKeys => dataKeys.Add(c => c.CustomerID))
        .Columns(columns =>
        {
            columns.Command(commands => commands.Select()).Title("Select");
            columns.Bound(c => c.CustomerID).Width(130);
            columns.Bound(c => c.CompanyName).Width(250);
            columns.Bound(c => c.ContactName);
            columns.Bound(c => c.Country).Width(200);
        })
        .Pageable()
        .Sortable()
        .RowAction(row => row.Selected = row.DataItem.CustomerID.Equals(ViewData["id"]))
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
        .Pageable()
        .Sortable()
%>

</asp:Content>


