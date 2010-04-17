<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<IEnumerable<Order>>" %>

<asp:content contentplaceholderid="maincontent" runat="server">

<% using (Html.Configurator("Group by ...")
              .PostTo("Grouping", "Grid")
              .Begin())
   { %>
    <ul>
        <li><%= Html.CheckBox("contactName", true, "Contact Name")%></li>
        <li><%= Html.CheckBox("orderDate", false, "Order Date")%></li>
        <li><%= Html.CheckBox("shipAddress", false, "Ship Address")%></li>
    </ul>
    <button class="t-button t-state-default" type="submit">Apply</button>
<%} %>

<%= Html.Telerik().Grid<Order>(Model)
        .Name("Grid")
        .Columns(columns =>
        {
            columns.Bound(o => o.OrderID).Width(100);
            columns.Bound(o => o.Customer.ContactName).Width(200);
            columns.Bound(o => o.ShipAddress);
            columns.Bound(o => o.OrderDate).Format("{0:MM/dd/yyyy}").Width(120);
        })
        .Groupable(settings => settings.Groups(groups =>
        {
            if ((bool)ViewData["orderDate"])
            {
                groups.Add(o => o.OrderDate);
            }
            
            if ((bool)ViewData["shipAddress"])
            {
                groups.Add(o => o.ShipAddress);
            }
            
            if ((bool)ViewData["contactName"])
            {
                groups.Add(o => o.Customer.ContactName);
            }
        }))
        .DataBinding(dataBinding => dataBinding.Ajax().Select("GroupingAjax", "Grid"))
        .Scrollable()
        .Sortable()
        .Pageable()
        .Filterable()
%>

</asp:content>
