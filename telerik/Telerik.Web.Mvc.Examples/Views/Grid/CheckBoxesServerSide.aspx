<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<IEnumerable<Order>>" %>


<asp:content contentplaceholderid="MainContent" runat="server">

<% Html.BeginForm(); %>

<% 
    int[] checkedRecords = (int[])ViewData["checkedRecords"];
    
    Html.Telerik().Grid(Model)
        .Name("Grid")
        .Columns(columns =>
        {
            columns.Template(o =>
            {
                %>
                    <input name="checkedRecords" type="checkbox" value="<%= o.OrderID %>" 
                    <% if (checkedRecords.Contains(o.OrderID)) {
                        
                        %> checked="checked" <%
                      } %>
                   />
                <%
            }).Title("Check").Width(50).HtmlAttributes(new { style = "text-align:center" });
            columns.Bound(o => o.OrderID).Width(100);
            columns.Bound(o => o.Customer.ContactName).Width(200);
            columns.Bound(o => o.ShipAddress);
            columns.Bound(o => o.OrderDate).Format("{0:MM/dd/yyyy}").Width(120);
        })
        .Pageable()
        .Render();
%>

<p>
    <button class="t-button t-state-default">Display checked orders</button>
</p>

<% Html.EndForm(); %>

<% if (checkedRecords.Any()) { %>
       
      <h3>Checked Orders</h3>
      <%= Html.Telerik().Grid<Order>()
              .Name("CheckedOrders")
              .BindTo((IEnumerable<Order>)ViewData["checkedOrders"])
              .Columns(columns =>
              {
                    columns.Bound(o => o.OrderID).Width(100);
                    columns.Bound(o => o.Customer.ContactName).Width(200);
                    columns.Bound(o => o.ShipAddress);
                    columns.Bound(o => o.OrderDate).Format("{0:MM/dd/yyyy}").Width(120);
              })
              .Footer(false)
      %>
           
<%}%>
</asp:content>
