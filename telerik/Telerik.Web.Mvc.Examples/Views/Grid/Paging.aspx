<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<IEnumerable<Order>>" %>
<asp:content contentPlaceHolderID="MainContent" runat="server">

<% using (Html.Configurator("The pager should contain...")
              .PostTo("Paging", "Grid")
              .Begin())
   { %>
    <ul>
        <li><%= Html.CheckBox("pageInput", false, "an <strong>input box</strong> for the page number")%></li>
        <li><%= Html.CheckBox("nextPrevious", true, "<strong>next page / previous page</strong> buttons")%></li>
        <li><%= Html.CheckBox("numeric", true, "a <strong>numeric pager</strong>")%></li>
    </ul>
    <button class="t-button t-state-default" type="submit">Apply</button>
<% } %>

<% 
    var pagerStyleFlags = new[] 
    { 
        new { Key = "pageInput", Value = GridPagerStyles.PageInput },
        new { Key = "nextPrevious", Value = GridPagerStyles.NextPrevious },
        new { Key = "numeric", Value = GridPagerStyles.Numeric }
    };

    GridPagerStyles pagerStyles = GridPagerStyles.NextPreviousAndNumeric;

    foreach (var pagerStyleFlag in pagerStyleFlags)
    {
        bool pagerStyle = (bool)ViewData[pagerStyleFlag.Key];
        if (pagerStyle == true)
        {
            pagerStyles |= pagerStyleFlag.Value;
        }
        else
        {
            pagerStyles &= ~pagerStyleFlag.Value;
        }
    }

    Html.Telerik().Grid(Model)
        .Name("Grid")
        .Columns(columns =>
        {
            columns.Bound(o => o.OrderID).Width(81);
            columns.Bound(o => o.Customer.ContactName).Width(200);
            columns.Bound(o => o.ShipAddress);
            columns.Bound(o => o.OrderDate).Format("{0:MM/dd/yyyy}").Width(100);
        })
        .DataBinding(dataBinding => dataBinding.Ajax().Select("_Paging", "Grid"))
        .Pageable(paging => paging.Style(pagerStyles))
        .Render();
%>
</asp:content>
