<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<Telerik.Web.Mvc.JavaScriptTest.Customer>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2>
        Selection</h2>
    <%= Html.Telerik().Grid(Model)
            .Name("Grid1")
            .Columns(columns =>
            {
                columns.Bound(c => c.Name);
                columns.Bound(c => c.BirthDate.Day);
            })
            .Ajax(settings => { })
            .Pageable()
            .Selectable()
            .ClientEvents(events => events.OnRowSelected("onRowSelected"))
    %>
    <%= Html.Telerik().Grid(Model)
            .Name("Grid2")
            .Columns(columns =>
            {
                columns.Bound(c => c.Name);
                columns.Bound(c => c.BirthDate.Day);
            })
            .Ajax(settings => { })
            .Pageable()
            
    %>

    <script type="text/javascript">
        function getGrid(selector) {
            return $(selector || '#Grid1').data('tGrid');
        }

        var rowSelected;
        
        function setUp() {
            rowSelected = null;
        }
        
        function tearDown() {
            $('#Grid1 tbody tr').removeClass('t-state-selected');
        }

        function test_clicking_a_row_should_select_it() {
            $('#Grid1 tbody tr:first').trigger('click');
            assertTrue($('#Grid1 tbody tr:first').hasClass('t-state-selected'));
        }

        function test_clicking_another_row_removes_selected_style() {
            $('#Grid1 tbody tr:first').trigger('click');
            $('#Grid1 tbody tr:nth-child(2)').trigger('click');
            assertFalse($('#Grid1 tbody tr:first').hasClass('t-state-selected'));
        }

        function test_selection_style_is_removed_after_binding() {
            $('#Grid1 tbody tr:first').trigger('click');
            getGrid().dataBind([{ BirthDate: new Date() }, { BirthDate: new Date()}]);
            assertEquals(0, $('#Grid1 tbody tr.t-state-selected').length);
        }

        function test_selection_is_disabled_by_default() {
            $('#Grid2 tbody tr:first').trigger('click');
            assertFalse($('#Grid2 tbody tr:first').hasClass('t-state-selected'));
        }
        
        function onRowSelected (e) {
            rowSelected = e.row;
        }
        
        function test_row_selected_is_raised() {
            $('#Grid1 tbody tr:first').trigger('click');
            assertEquals($('#Grid1 tbody tr:first')[0], rowSelected);
        }
        
     
    </script>

</asp:Content>
