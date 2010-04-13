<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>

<link href="<%= ResolveUrl("~/favicon.ico") %>" type="image/x-icon" rel="icon" />
<link href="<%= ResolveUrl("~/favicon.ico") %>" type="image/x-icon" rel="shortcut icon" />

<meta name="ProductId" content="697" />
<%= Html.ProductMetaTag() %>