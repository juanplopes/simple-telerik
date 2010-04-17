<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage" %>
<asp:Content contentPlaceHolderID="MainContent" runat="server">
    
<%= Html.Telerik().TreeView()
       .Name("TreeView")
       .BindTo("sample")
%>
    
</asp:Content>