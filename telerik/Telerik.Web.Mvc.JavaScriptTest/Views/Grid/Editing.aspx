<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<Telerik.Web.Mvc.JavaScriptTest.Customer>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2>
        Editing</h2>
    <%= Html.Telerik().Grid(Model)
            .Name("Grid1")
            .DataKeys(keys => keys.Add(c => c.Name))
            .ToolBar(toolbar => toolbar.Insert())
            .Columns(columns => 
                {
                    columns.Bound(c => c.Name).Format("<strong>{0}</strong>");
                    columns.Bound(c => c.BirthDate).Format("{0:d}");
                    columns.Bound(c => c.ReadOnly);
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

    <%= Html.Telerik().Grid(Model)
            .Name("Grid2")
            .DataKeys(keys => keys.Add(c => c.Name))
            .ToolBar(toolbar => toolbar.Insert())
            .Columns(columns => 
                {
                    columns.Bound(c => c.Name);
                    columns.Bound(c => c.BirthDate).Format("{0:d}");
                    columns.Command(commands =>
                    {
                        commands.Edit();
                        commands.Delete();
                    });
                })
            .DataBinding(binding => binding.Server()
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

        function addNew() {
            $('#Grid1 .t-grid-add').trigger('click');
        }
        
        function edit() {
            $('#Grid1 tbody tr:first').find('.t-grid-edit').trigger('click');
        }

        function cancel() {
            $('#Grid1 tbody tr:first').find('.t-grid-cancel').trigger('click');
        }

        function update() {
            $('#Grid1 tbody tr:first').find('.t-grid-update').trigger('click');
        }

        function insert() {
            $('#Grid1 tbody tr:first').find('.t-grid-insert').trigger('click');
        }
        
        function tearDown() {
            getGrid().cancel();
            getGrid().editing = { confirmDelete: true };
        }

        function test_edit_for_does_not_exist_by_default() {
            assertEquals(0, $('#Grid1form').length);
        }

        function test_clicking_edit_creates_edit_form() {
            edit();
            assertEquals(1, $('#Grid1form').length);
        }

        function test_clicking_cancel_removes_the_edit_form() {
            edit();
            cancel();
            assertEquals(0, $('#Grid1form').length);
        }

        function test_clicking_edit_puts_the_row_in_edit_mode() {
            edit();

            assertEquals('Customer1', $('#Grid1 tbody tr').find(':input').first().val());
            assertEquals('1/1/1980', $('#Grid1 tbody tr').find(':input').eq(1).val());
        }

        function test_clicking_cancel_restores_original_data() {
            edit();
            $('#Grid1 tbody tr').find(':input').val('test');
            cancel();
            edit();
            assertEquals('Customer1', $('#Grid1 tbody tr').find(':input').first().val());
        }

        function test_data_keys_serialized() {
            assertEquals('Customer1', getGrid().data[0].Name);
            assertEquals('id', getGrid().dataKeys.Name);
        }

        function test_clicking_update_posts_data() {
            
            var called = false;

            getGrid().sendValues = function(values, url) {
                called = true;
                assertEquals('Customer1', values.Name);
                assertEquals('Customer1', values.id);
                assertEquals('updateUrl', url);
            }
            
            edit();
            update();

            assertTrue(called);
        }

        function test_create_row_creates_edit_form() {
            getGrid().addRow();

            assertEquals(1, $('#Grid1form').length);
        }

        function test_create_row_creates_empty_edit_form() {
            getGrid().addRow();

            assertEquals('', $('#Grid1 tbody tr:first :input').first().val());
        }

        function test_clicking_insert_posts_data() {
            var called = false;

            getGrid().sendValues = function(values, url) {
                called = true;
                assertEquals('test', values.Name);
                assertUndefined(values.id);
                assertEquals('insertUrl', url);
            }

            getGrid().addRow();
            
            $('#Grid1 tbody tr:first :input').val('test');
            
            insert();

            assertTrue(called);
        }

        function test_clicking_delete_posts_data() {
            var called = false;
            getGrid().sendValues = function(values, url) {
                called = true;
                assertEquals('Customer1', values.id);
                assertEquals('deleteUrl', url);
            }

            window.confirm = function() {
                return true;
            }
            $('#Grid1 tbody tr:first .t-grid-delete').trigger('click');

            assertTrue(called);
        }

        function test_no_confirmation() {
            getGrid().editing = { confirmDelete: false };

            var called = false;
            window.confirm = function() {
                called = true;
            }
            $('#Grid1 tbody tr:first .t-grid-delete').trigger('click');

            assertFalse(called);
        }

        function test_cancelling_confirm_on_delete_does_not_post_data() {
            var called = false;
            
            getGrid().sendValues = function(values, url) {
                called = true;
            }

            window.confirm = function() {
                return false;
            }

            $('#Grid1 tbody tr:first .t-grid-delete').trigger('click');

            assertFalse(called);
        }

        function test_validation() {

            var called = false;
            getGrid().sendValues = function(values, url) {
                called = true;
            }

            getGrid().addRow();
            insert();

            assertFalse(called);
            assertEquals(1, $('#Grid1 .field-validation-error').length);
        }

        function test_datepicker_instantiation() {
            addNew();

            assertNotUndefined($('div[id^=BirthDate]').data('tDatePicker'));
        }

        function test_confirm_shown_for_server_binding() {
            var grid = getGrid('#Grid2');
            var called = false;
            window.confirm = function() {
                called = true;
                return false;
            }

            $('#Grid2 tbody tr:first .t-grid-delete').trigger('click');

            assertTrue(called);
        }

        function test_read_only_column() {
            var grid = getGrid();
            assertTrue(grid.columns[2].readonly);
        }

        function test_read_only_column_shown_in_edit_mode() {
            edit();

            assertEquals("0", $('#Grid1form tr:first td:eq(2)').text());
        }
    </script>

</asp:Content>
