namespace Telerik.Web.Mvc.UI.UnitTest.Grid
{
    using Moq;

    using Xunit;
    using System;

    public class GridWorkingConditionsTests
    {
        private readonly Grid<Customer> grid;
        private readonly Mock<IGridHtmlBuilder<Customer>> renderer;
        private readonly Mock<IClientSideObjectWriter> objectWriter;
        private readonly Customer customer;

        public GridWorkingConditionsTests()
        {
            renderer = new Mock<IGridHtmlBuilder<Customer>>();
            objectWriter = new Mock<IClientSideObjectWriter>();
            grid = GridTestHelper.CreateGrid(null, renderer.Object, objectWriter.Object);

            customer = new Customer { Id = 1, Name = "John Doe" };
            grid.DataSource = new[] { customer };

            grid.Columns.Add(new GridBoundColumn<Customer, int>(grid, c => c.Id));
            grid.Columns.Add(new GridBoundColumn<Customer, string>(grid, c => c.Name));
        }

        [Fact]
        public void Should_throw_when_editing_enabled_but_no_data_keys_set()
        {
            grid.Editing.Enabled = true;

            Assert.Throws<NotSupportedException>(() => grid.Render());
        }

        [Fact]
        public void Should_throw_when_selection_enabled_but_no_data_keys_set()
        {
            grid.Selection.Enabled = true;

            Assert.Throws<NotSupportedException>(() => grid.Render());
        }

        [Fact]
        public void Should_throw_if_data_binding_is_not_configured_for_edit_command_ajax()
        {
            ConfigureEditing(g =>
            {
                g.Ajax.Enabled = true;
                grid.Columns.Add(new GridActionColumn<Customer>(grid)
                {
                    Commands =
                    {
                        new GridEditActionCommand<Customer>()
                    }
                });
            });
        }

        [Fact]
        public void Should_throw_if_data_binding_is_not_configured_for_delete_command_ajax()
        {
            ConfigureEditing(g =>
            {
                g.Ajax.Enabled = true;
                grid.Columns.Add(new GridActionColumn<Customer>(grid)
                {
                    Commands =
                    {
                        new GridDeleteActionCommand<Customer>()
                    }
                });
            });
        }
        
        [Fact]
        public void Should_throw_if_data_binding_is_not_configured_for_insert_command_ajax()
        {
            ConfigureEditing(g =>
            {
                g.Ajax.Enabled = true;
                g.ToolBar.Commands.Add(new GridToolBarInsertCommand<Customer>());
            });
        }

        [Fact]
        public void Should_throw_if_data_binding_is_not_configured_for_edit_command_web_service()
        {
            ConfigureEditing(g =>
            {
                g.WebService.Enabled = true;
                g.WebService.Select.Url = "#";
                grid.Columns.Add(new GridActionColumn<Customer>(grid)
                {
                    Commands =
                    {
                        new GridEditActionCommand<Customer>()
                    }
                });
            });
        }
        
        [Fact]
        public void Should_throw_if_data_binding_is_not_configured_for_insert_command_web_service()
        {
            ConfigureEditing(g =>
            {
                g.WebService.Enabled = true;
                g.WebService.Select.Url = "#";
                g.ToolBar.Commands.Add(new GridToolBarInsertCommand<Customer>());
            });
        }

        [Fact]
        public void Should_throw_if_data_binding_is_not_configured_for_delete_command_web_service()
        {
            ConfigureEditing(g =>
            {
                g.WebService.Enabled = true;
                g.WebService.Select.Url = "#";
                grid.Columns.Add(new GridActionColumn<Customer>(grid)
                {
                    Commands =
                    {
                        new GridDeleteActionCommand<Customer>()
                    }
                });
            });
        }

        [Fact]
        public void Should_throw_if_data_binding_is_not_configured_for_edit_command_server()
        {
            ConfigureEditing(g =>
            {
                grid.Columns.Add(new GridActionColumn<Customer>(grid)
                {
                    Commands =
                    {
                        new GridEditActionCommand<Customer>()
                    }
                });
            });
        }

        [Fact]
        public void Should_throw_if_data_binding_is_not_configured_for_delete_command_server()
        {
            ConfigureEditing(g =>
            {
                g.Columns.Add(new GridActionColumn<Customer>(grid)
                {
                    Commands =
                    {
                        new GridEditActionCommand<Customer>()
                    }
                });
            });
        }
        
        [Fact]
        public void Should_throw_if_data_binding_is_not_configured_for_insert_command_server()
        {
            ConfigureEditing(g =>
            {
                g.ToolBar.Commands.Add(new GridToolBarInsertCommand<Customer>());
            });
        }

        private void ConfigureEditing(Action<Grid<Customer>> configurator)
        {
            grid.Editing.Enabled = true;
            grid.DataKeys.Add(new GridDataKey<Customer, int>(c => c.Id));

            configurator(grid);

            Assert.Throws<NotSupportedException>(() => grid.Render());
        }

        [Fact]
        public void Should_throw_when_both_ajax_and_web_service_are_enabled()
        {
            grid.Ajax.Enabled = grid.WebService.Enabled = true;

            Assert.Throws<NotSupportedException>(() => grid.Render());
        }

        [Fact]
        public void Should_throw_when_using_templates_and_ajax()
        {
            grid.Ajax.Enabled = true;
            grid.Columns[0].Template = delegate { };

            Assert.Throws<NotSupportedException>(() => grid.Render());
        }

        [Fact]
        public void Should_throw_when_using_templates_and_web_service()
        {
            grid.WebService.Enabled = true;
            grid.Columns[0].Template = delegate { };

            Assert.Throws<NotSupportedException>(() => grid.Render());
        }

        [Fact]
        public void Should_throw_if_web_service_url_is_not_set()
        {
            grid.WebService.Enabled = true;
            Assert.Throws<ArgumentException>(() => grid.Render());
        }
    }
}
