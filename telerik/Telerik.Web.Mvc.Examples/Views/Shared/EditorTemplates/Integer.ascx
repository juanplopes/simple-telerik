<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<int>" %>

<%= Html.Telerik().IntegerTextBox()
        .Name(ViewData.TemplateInfo.HtmlFieldPrefix)
        .Value(Model)
%>