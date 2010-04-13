<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<IEnumerable<Employee>>" %>

<asp:content contentplaceholderid="MainContent" runat="server">
    
    <div class="pane">
        <h3>Reorder nodes</h3>
        <%= Html.Telerik().TreeView()
                .Name("TreeView")
                .DragAndDrop(settings => settings
                    .DropTargets(".drop-container")
                )
                .ClientEvents(events => events
                    .OnNodeDrop("onNodeDrop")
                )
                .BindTo(Model, mappings =>
                {
                    mappings.For<Employee>(binding => binding
                            .ItemDataBound((item, employee) =>
                            {
                                item.Text = String.Format("{0} {1}", employee.FirstName, employee.LastName);
                                item.ImageUrl = Url.Content("~/Content/TreeView/Common/contact.png");
                                item.Expanded = true;
                            })
                            .Children(employee => employee.Employees));
                })
                .HtmlAttributes(new { @class = "t-group" })
        %>
    </div>
    
    <div class="pane">
        <h3>Team Bravo</h3>
        <div class="drop-container t-group"></div>
        
        <h3>Team Charlie</h3>
        <div class="drop-container t-group"></div>
    </div>
    
    <script type="text/javascript">
        function onNodeDrop(e) {
            var dropContainer = $(e.dropTarget).closest('.drop-container');
            
            if (dropContainer.length > 0) {
                $('<div><strong>' + $(e.item).text() + '</strong></div>')
                    .hide().appendTo(dropContainer).slideDown('fast');

                e.preventDefault();
            }
        }
    </script>

</asp:content>

<asp:content contentplaceholderid="HeadContent" runat="server">
    <style type="text/css">
        #TreeView, .drop-container
        {
            border-width: 1px;
            border-style: solid;
            width: 24em;
            float: left;
        }
        
        #TreeView
        {
            height: 24em;
            padding: .5em;
        }
        
        .drop-container
        {
            height: 8em;
            overflow: auto;
            margin-bottom: 1em;
            padding: .70em;
        }
        .pane
        {
            float: left;
            margin: -2em 6em 2em 0;
        }
    </style>
</asp:content>
