<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<Telerik.Web.Mvc.JavaScriptTest.Customer>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Grouping</h2>
    <%= Html.Telerik().Grid(Model)
            .Name("Grid1")
            .Columns(columns =>
            {
                columns.Bound(c => c.Name);
                columns.Bound(c => c.BirthDate);
                columns.Bound(c => c.Active);
            })
            .Scrollable()
            .Groupable(grouping => grouping.Groups(groups => 
                {
                    groups.Add(c => c.Name);
                    groups.Add(c => c.BirthDate);
                }))
            .DataBinding(dataBinding => dataBinding.Ajax().Select("Foo", "Bar"))
        %>
    <%= Html.Telerik().Grid(Model)
            .Name("Grid2")
            .Columns(columns =>
            {
                columns.Bound(c => c.Name);
                columns.Bound(c => c.BirthDate);
                columns.Bound(c => c.Active);
                columns.Bound(c => c.Active).Title("Ungroupable").Groupable(false);
            })
            .Scrollable()
            .Groupable()
            .DataBinding(dataBinding => dataBinding.Ajax().Select("Foo", "Bar"))
        %>
    
        <%= Html.Telerik().Grid(Model)
            .Name("Grid3")
            .Columns(columns =>
            {
                columns.Bound(c => c.Name);
                columns.Bound(c => c.BirthDate).Hidden(true);
                columns.Bound(c => c.Active);
            })
            .Scrollable()
            .Groupable()
            .DataBinding(dataBinding => dataBinding.Ajax().Select("Foo", "Bar"))
        %>
    <%= Html.Telerik().Grid(Model)
            .Name("Grid4")
            .Columns(columns =>
            {
                columns.Bound(c => c.Name);
                columns.Bound(c => c.BirthDate);
                columns.Bound(c => c.Active);
            })
            .Scrollable()
            .Groupable(grouping => grouping.Groups(groups => 
                {
                    groups.Add(c => c.Name);
                    groups.Add(c => c.BirthDate);
                }))
            .DataBinding(dataBinding => dataBinding.Ajax().Select("Foo", "Bar"))
        %>        
    <script type="text/javascript">
    
        function getGrid(selector) {
            return $(selector).data("tGrid");
        }

        function setUp() {
            getGrid('#Grid1').ajaxRequest = function() {};
            getGrid('#Grid2').ajaxRequest = function() {};
            getGrid('#Grid3').ajaxRequest = function() {};
            getGrid('#Grid4').ajaxRequest = function() {};
        }

        function test_ungrouping_removes_grouping_columns() {
            var grid = getGrid('#Grid1');
            grid.unGroup('Birth Date');
            grid.normalizeColumns();
            
            assertEquals(4, $('table:first col', grid.element).length)
            assertEquals(1, $('tr:has(th):first .t-groupcell', grid.element).length)
        }

        function test_ungrouping_removes_grouping_columns_hidden() {
            var grid = getGrid('#Grid4');
            grid.unGroup('Birth Date');
            grid.normalizeColumns();

            assertEquals(4, $('table:first col', grid.element).length)
            assertEquals(1, $('tr:has(th):first .t-groupcell', grid.element).length)
        }
        
        function test_grouping_creates_grouping_columns() {
            var grid = getGrid('#Grid2');
            grid.group('Name');
            grid.normalizeColumns();

            assertEquals(5, $('table:first col', grid.element).length)
            assertEquals(1, $('tr:has(th):first .t-groupcell', grid.element).length)
        }
        
        function test_grouping_creates_grouping_hidden() {
            var grid = getGrid('#Grid3');
            grid.group('Name');
            grid.normalizeColumns();

            assertEquals(4, $('table:first col', grid.element).length)
            assertEquals(1, $('tr:has(th):first .t-groupcell', grid.element).length)
        }

        function test_ungroupable_columns_groupable_serialized() {
            var grid = getGrid('#Grid2');
            assertFalse(grid.columns[grid.columns.length - 1].groupable);
        }

        function test_cannot_drag_ungroupable_column() {
            var grid = getGrid('#Grid2');

            assertFalse($.telerik.draganddrop.grouping.shouldDrag.call(grid, $('th:contains(Ungroupable)', grid.element)));
        }
    </script>        
</asp:Content>
