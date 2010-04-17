<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage" %>
<asp:content contentPlaceHolderID="MainContent" runat="server">
   
    <%= Html.Telerik().Calendar()
            .Name("Calendar")
            .Selection(settings => 
                    settings
                        .Action("SelectAction", new { date = "{0}", theme = Request.QueryString["theme"] })
                        .Dates(new List<DateTime> {
                            DateTime.Today.AddDays(-25),
                            DateTime.Today.AddDays(-9),
                            DateTime.Today.AddDays(-1),
                            DateTime.Today.AddDays(2),
                            DateTime.Today.AddDays(3),
                            DateTime.Today.AddDays(10),
                            DateTime.Today.AddDays(35)
                        })
            )
            .Value(ViewData["date"] != null ? (DateTime)ViewData["date"] : DateTime.Today)
    %>
    
    <% if (ViewData["date"] != null) { %>
        <p>This date was clicked: <%= ViewData["date"]%></p>
    <% } %>
    
</asp:content>