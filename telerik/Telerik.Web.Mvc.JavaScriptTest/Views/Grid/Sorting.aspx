<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<Telerik.Web.Mvc.JavaScriptTest.Customer>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2>
        Sorting</h2>
    <%= Html.Telerik().Grid(Model)
            .Name("Grid1")
            .Columns(columns => {
                columns.Bound(c => c.Name);
                columns.Bound(c => c.BirthDate.Day);
            })
            .Sortable()
            .Pageable(pager => pager.PageSize(1))
    %>
    <%= Html.Telerik().Grid(Model)
            .Name("Grid2")
            .Columns(columns => {
                columns.Bound(c => c.Name);
                columns.Bound(c => c.BirthDate.Day);
            })
            .Sortable(sorting => sorting.OrderBy(columns => columns
                .Add(c => c.Name)))
            .Pageable(pager => pager.PageSize(1))
    %>

    <script type="text/javascript">
        var gridElement;

        function setUp() {
            gridElement = document.createElement("div");
            gridElement.id = "tempGrid";
        }

        function getGrid(selector) {
            return $(selector).data("tGrid");
        }

        function test_clicking_the_header_calls_order_by() {
            var grid = getGrid("#Grid1");
            var columnIndex = 0;
            var orderBy = grid.sort;
            grid.sort = function(index) {
                columnIndex = index;
            }

            $("th:nth-child(1)", grid.element).trigger("click");

            assertEquals(0, columnIndex);

            grid.sort = orderBy;
        }

        function test_sort_expression_should_return_column() {
            var grid = createGrid(gridElement, { columns: [{ member: "c1" }, { member: "c2"}] });

            assertEquals("c1-asc", grid.sortExpr(0));
        }

        function test_sort_expression_multiple_columns() {
            var grid = createGrid(gridElement, { columns: [{ member: "c1" }, { member: "c2"}] });
            grid.sortExpr(1);

            assertEquals("c2-asc~c1-asc", grid.sortExpr(0));
        }

        function test_sort_expression_correctly_updates_sorting_order() {
            var grid = createGrid(gridElement, { columns: [{ member: "c1" }, { member: "c2"}] });

            assertEquals("c1-asc", grid.sortExpr(0));
            assertEquals("c1-desc", grid.sortExpr(0));
            assertEquals("", grid.sortExpr(0));
        }

        function test_sort_is_undefined_by_default() {
            var grid = createGrid(gridElement, { columns: [{ member: "c1" }, { member: "c2"}] });
            assertUndefined(grid.sortMode);
        }

        function test_sort_is_serialized() {
            var grid = getGrid("#Grid1");
            assertEquals("single", grid.sortMode);
        }

        function test_sort_expression_supports_single_sort_mode() {
            var grid = createGrid(gridElement, { sortMode: "single", columns: [{ member: "c1" }, { member: "c2"}] });

            grid.sortExpr(0);
            assertEquals("c2-asc", grid.sortExpr(1));
            assertEquals(null, grid.columns[0].order);
        }

        function test_sort_expression_correctly_changes_sort_direction_in_single_sort_mode() {
            var grid = createGrid(gridElement, { sortMode: "single", columns: [{ member: "c1" }, { member: "c2"}] });

            grid.sortExpr(0);
            assertEquals("c1-desc", grid.sortExpr(0));
        }

        function createGrid(gridElement, options) {
            options = $.extend({}, $.fn.tGrid.defaults, options);
            return new $.telerik.grid(gridElement, options);
        }

        function test_order_serialized_for_sorted_columns() {
            var grid = getGrid("#Grid2");
            assertEquals("asc", grid.columns[0].order);
        }
    </script>

</asp:Content>
