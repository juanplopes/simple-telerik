<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<IEnumerable<NavigationData>>" %>


<asp:content contentplaceholderid="MainContent" runat="server">

    <% Html.Telerik().TabStrip()
           .Name("TabStrip")
           .BindTo(Model,
                   (item, navigationData) =>
                   {
                       item.Text = navigationData.Text;
                       item.ImageUrl = navigationData.ImageUrl;
                       item.Url = navigationData.NavigateUrl;
                   })
           .ClientEvents(events =>
               events
                   .OnSelect(() =>
                   {%>
                       function(e) {
                           /* 
                               do not navigate
                               URLs are set only for the sake of the example
                           */
                           e.preventDefault();
                       }<%
                   } ))
           .Render();
    %>

</asp:content>
