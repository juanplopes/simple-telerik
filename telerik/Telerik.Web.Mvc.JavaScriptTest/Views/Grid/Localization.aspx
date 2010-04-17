<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<Telerik.Web.Mvc.JavaScriptTest.Customer>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2>
        Selection</h2>
    <%= Html.Telerik().Grid(Model)
            .Name("Grid1")
            .DataKeys(keys => keys.Add(c => c.Name))
            .ToolBar(toolbar => toolbar.Insert())
            .Columns(columns =>
            {
                columns.Bound(c => c.Name);
                columns.Bound(c => c.BirthDate.Day);
                columns.Command(command =>
                {
                    command.Delete();
                    command.Select();
                    command.Edit();
                });
            })
            .DataBinding(dataBinding => dataBinding.Ajax()
                    .Select("Select", "Controller")
                    .Insert("Insert", "Controller")
                    .Delete("Delete", "Controller")
                    .Update("Update", "Controller"))
            .Pageable(paging => paging.Style(GridPagerStyles.NextPreviousAndNumeric | GridPagerStyles.PageInput | GridPagerStyles.Status))
            .Filterable()
            .Selectable()
            .Sortable()
            .Groupable()
    %>

    <script type="text/javascript">
        var grid;
        
        function setUp() {
            grid = getGrid('#Grid1');
            grid.ajaxRequest = function() { }
        }

        function getGrid(selector) {
            return $(selector || "#Grid1").data("tGrid");
        }

        function test_input_paging_text() {
            grid.localization.page = 'stranica';
            grid.localization.pageOf = 'ot';
            
            var $pager = $('.t-page-i-of-n');
            grid.updatePager();
            assertTrue($pager.html().indexOf('stranica') > -1);
            assertTrue($pager.html().indexOf('ot') > -1);
        }

        function test_pager_status() {
            grid.localization.displayingItems = 'Pokazvame zapisi {0} - {1} ot {2}';
            grid.updatePager();
            var $status = $('.t-status-text');
            assertEquals('Pokazvame zapisi 1 - 10 ot 20', $status.html());
        }

        function test_edit_localization() {
            grid.localization.edit = "redaktirai";
            grid.editRow($('.t-grid-edit:first', grid.element).closest('tr'));
            grid.cancel();

            assertEquals('redaktirai', $('.t-grid-edit:first').text());
        }

        function test_delete_localization() {
            grid.localization['delete'] = "iztrii";
            grid.editRow($('.t-grid-edit:first', grid.element).closest('tr'));
            grid.cancel();

            assertEquals('iztrii', $('.t-grid-delete:first').text());
        }

        function test_update_localization() {
            grid.localization['update'] = "zapazi";
            grid.editRow($('.t-grid-edit:first', grid.element).closest('tr'));
            assertEquals('zapazi', $('.t-grid-update:first').text());
            grid.cancel();
        }
        
        function test_cancel_localization() {
            grid.localization['cancel'] = "otkaz";
            grid.editRow($('.t-grid-edit:first', grid.element).closest('tr'));
            assertEquals('otkaz', $('.t-grid-cancel:first').text());
            grid.cancel();
        }

        function test_insert_localization() {
            grid.localization['insert'] = "dobavi";
            grid.addRow();
            assertEquals('dobavi', $('.t-grid-insert:first').text());
            grid.cancel();
        }

        function test_select_localization() {
            grid.localization['select'] = "izberi";
            grid.editRow($('.t-grid-edit:first', grid.element).closest('tr'));
            grid.cancel();
            assertEquals('izberi', $('.t-grid-select:first').text());
        }

        function test_filter_localization() {
            grid.localization['filter'] = "filtrirai";
            grid.localization['filterClear'] = "mahni filter";
            grid.localization['filterShowRows'] = "pokazhi redove, koito";
            grid.localization['filterAnd'] = "i";
            grid.localization['filterStringEq'] = "sushtoto kato";
            $('th:contains(Name) .t-filter').click();
            assertEquals('filtrirai', $('.t-filter-button:visible').text());
            assertEquals('mahni filter', $('.t-clear-button:visible').text());
            assertEquals('pokazhi redove, koito', $('.t-filter-help-text:visible:first').text());
            assertEquals('i', $('.t-filter-help-text:visible:eq(1)').text());
            assertEquals('sushtoto kato', $('select:visible option:first').text());
        }

        function test_grouping_localization() {
            grid.localization.groupHint = 'grupirane';
            grid.group('Name');
            grid.unGroup('Name');
            assertEquals('grupirane', $('.t-grouping-header').text());
        }
    </script>
</asp:Content>
