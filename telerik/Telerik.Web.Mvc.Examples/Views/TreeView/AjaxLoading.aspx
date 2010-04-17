<%@ Page Title="" Language="C#" Inherits="System.Web.Mvc.ViewPage<IEnumerable<Employee>>" %>

<asp:content contentPlaceHolderID="MainContent" runat="server">

<h3 style="margin-top: 0;">Ajax</h3>
<%= Html.Telerik().TreeView()
        .Name("AjaxTreeView")
        .BindTo(Model, (item, employee) =>
        {
            // bind initial data - can be omitted if there is none
            item.Text = employee.FirstName + " " + employee.LastName;
            item.Value = employee.EmployeeID.ToString();
            item.LoadOnDemand = employee.Employees.Count > 0;
        })
        .DataBinding(dataBinding => dataBinding
                .Ajax().Select("_AjaxLoading", "TreeView")
        )
%>

<h3>ASMX Web Service</h3>
<%= Html.Telerik().TreeView()
        .Name("AsmxTreeView")
        .BindTo(Model, (item, employee) =>
        {
            // bind initial data - can be omitted if there is none
            item.Text = employee.FirstName + " " + employee.LastName;
            item.Value = employee.EmployeeID.ToString();
            item.LoadOnDemand = employee.Employees.Count > 0;
        })
        .DataBinding(dataBinding => dataBinding
            .WebService().Select("~/Models/Employees.asmx/GetEmployees")
        )
%>

<h3>WCF Web Service</h3>
<%= Html.Telerik().TreeView()
        .Name("WcfTreeView")
        .BindTo(Model, (item, employee) =>
        {
            // bind initial data - can be omitted if there is none
            item.Text = employee.FirstName + " " + employee.LastName;
            item.Value = employee.EmployeeID.ToString();
            item.LoadOnDemand = employee.Employees.Count > 0;
        })
        .DataBinding(dataBinding => dataBinding
            .WebService().Select("~/Models/Employees.svc/GetEmployees")
        )
%>

</asp:content>
