<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<IEnumerable<Order>>" %>
<asp:Content contentPlaceHolderID="MainContent" runat="server">

<% using (Html.Configurator("Sort by ...")
              .PostTo("Sorting", "Grid")
              .Begin())
   { %>
    <ul>
        
        <li><%= Html.CheckBox("orderId", true, "Order ID")%></li>
        <li><%= Html.CheckBox("contactName", false, "Contact Name")%></li>
        <li><%= Html.CheckBox("shipCountry", false, "Ship Country")%></li>
        <li><%= Html.CheckBox("orderDate", false, "Order Date")%></li>
    </ul>
    <button class="t-button t-state-default" type="submit">Apply</button>
<% } %>

<%= Html.Telerik().Grid(Model)
        .Name("Grid")
        .Columns(columns =>
        {
            columns.Bound(o => o.OrderID).Width(120);
            columns.Bound(o => o.Customer.ContactName).Width(200);
            columns.Bound(o => o.ShipCountry);
            columns.Bound(o => o.OrderDate).Format("{0:MM/dd/yyyy}").Width(100);
        })
		.DataBinding(dataBinding => dataBinding.Ajax().Select("_Sorting", "Grid"))
        .Sortable(sorting => sorting
                .SortMode(GridSortMode.MultipleColumn)
                .OrderBy(order =>
                {
                    if ((bool)ViewData["orderId"])
                    {
                        order.Add(o => o.OrderID);
                    }

                    if ((bool)ViewData["contactName"])
                    {
                        order.Add(o => o.Customer.ContactName);
                    }

                    if ((bool)ViewData["shipCountry"])
                    {
                        order.Add(o => o.ShipCountry);
                    }

                    if ((bool)ViewData["orderDate"])
                    {
                        order.Add(o => o.OrderDate);
                    }
                })
        )
        .Pageable()
%>

</asp:Content>
