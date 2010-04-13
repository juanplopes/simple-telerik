<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<decimal>" %>

<%= Html.Telerik().CurrencyTextBox()
        .Name(ViewData.TemplateInfo.HtmlFieldPrefix)
        .MinValue(0)
        .Value(Model)
%>