<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage" %>
<asp:Content contentPlaceHolderID="MainContent" runat="server">
    
<%= Html.Telerik().Menu()
       .Name("Menu")
       .BindTo("sample")
%>
    
</asp:Content>