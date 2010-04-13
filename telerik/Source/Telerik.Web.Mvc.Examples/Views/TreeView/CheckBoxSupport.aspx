<%@ Page Title="" Language="C#" Inherits="System.Web.Mvc.ViewPage<IEnumerable<Employee>>" %>
<asp:content contentPlaceHolderID="maincontent" runat="server">
<% using (Html.BeginForm("CheckBoxSupport", "TreeView", FormMethod.Post))
{ %>
        <% List<TreeViewItemModel> checkedNodes = ViewData["checkedNodes"] as List<TreeViewItemModel>; %>
        
        <%= Html.Telerik().TreeView()
            .Name("TreeView")
            .ShowCheckBox(true)
            .BindTo(Model, mappings =>
            {
                mappings.For<Employee>(binding => binding
                        .ItemDataBound((item, employee) =>
                        {
                            item.Text = employee.FirstName + " " + employee.LastName;
                            item.Value = employee.EmployeeID.ToString();

                            if (checkedNodes != null)
                            {
                                item.Checked =
                                    checkedNodes
                                        .Where(e => e.Value.Equals(employee.EmployeeID.ToString()))
                                        .FirstOrDefault() != null;
                            }

                            item.Expanded = true;
                        })
                        .Children(category => category.Employees));
                
                mappings.For<Employee>(binding => binding
                        .ItemDataBound((item, employee) =>
                        {
                            item.Text = employee.FirstName + " " + employee.LastName;
                            item.Value = employee.EmployeeID.ToString();

                            if (checkedNodes != null)
                            {
                                item.Checked =
                                    checkedNodes
                                        .Where(e => e.Value.Equals(employee.EmployeeID.ToString()))
                                        .FirstOrDefault() != null;
                            }
                            
                        }));
            })
        %>
    <p>
        <button type="submit" class="t-button t-state-default">Post!</button>
    </p>
    
    <% if (checkedNodes != null && checkedNodes.Count > 0){%>
        <p>
            <strong>You have selected the following items:</strong><br />
            <span><%= ViewData["message"]%></span>
        </p>
    <% } %>
<%}%>

</asp:content>