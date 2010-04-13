<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Employee>" %>
<%= Html.Encode(Model.FirstName) %> <%= Html.Encode(Model.LastName) %>