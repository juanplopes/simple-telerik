<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<double>" %>

<%= Html.Telerik().NumericTextBox()
        .Name(ViewData.TemplateInfo.HtmlFieldPrefix)
        .Value(Model)
%>