<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<DateTime>" %>

<%= Html.Telerik().DatePicker()
        .Name(ViewData.TemplateInfo.HtmlFieldPrefix)
        .HtmlAttributes(new { id = ViewData.TemplateInfo.HtmlFieldPrefix + DateTime.Now.Millisecond.ToString()})
        .Value(Model > DateTime.MinValue? Model : DateTime.Today)
%>