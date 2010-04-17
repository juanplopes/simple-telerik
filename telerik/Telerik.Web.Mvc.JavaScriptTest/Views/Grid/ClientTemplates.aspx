<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<Telerik.Web.Mvc.JavaScriptTest.Customer>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2>
        Editing</h2>
    <%= Html.Telerik().Grid(Model)
            .Name("Grid1")
            .DataKeys(keys => keys.Add(c => c.Name))
            .Columns(columns => 
                {
                    columns.Bound(c => c.Name)
                        .ClientTemplate("<strong><#= Name #></strong>");
                    
                    columns.Bound(c => c.BirthDate);
                    
                    columns.Command(commands =>
                    {
                        commands.Edit();
                        commands.Delete();
                    });
                })
            .DataBinding(binding => binding.Ajax()
                .Select("Select", "Dummy")
                .Insert("Insert", "Dummy")
                .Delete("Delete", "Dummy")
                .Update("Update", "Dummy")
            )
            .Pageable(pager => pager.PageSize(10))
    %>

    <script type="text/javascript">

        function getGrid(selector) {
            return $(selector || "#Grid1").data("tGrid");
        }

        function test_client_template_is_serialized() {
            assertEquals('<strong><#= Name #></strong>', getGrid().columns[0].template);
        }

        function test_template_is_applied() {
            var grid = getGrid();
            var column = grid.columns[0];
            assertEquals("<strong>test</strong>", grid.displayFor(column)({ Name: 'test' }));
        }

        function test_value_of_serialized_date() {
            var grid = getGrid();
            var column = grid.columns[1];
            assertTrue(grid.valueFor(column)(grid.data[0]) instanceof Date);
        }
    </script>

</asp:Content>
