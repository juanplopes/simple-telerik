namespace Telerik.Web.Mvc.UI.UnitTest
{
    using System;
    using System.Web.Mvc;
    using Moq;
using Xunit;

    public class InputClientEventsBuilderTests
    {
        private TextboxBaseClientEventsBuilder builder;
        private TextboxBaseClientEvents clientEvents;
        private ViewContext viewContext;


        public InputClientEventsBuilderTests()
        {
            clientEvents = new TextboxBaseClientEvents();
            viewContext = new ViewContext();
            builder = new TextboxBaseClientEventsBuilder(clientEvents, viewContext);
        }

        [Fact]
        public void OnChange_method_with_Action_param_should_set_OnChange_property()
        {
            Action param = () => { };

            builder.OnChange(param);

            Assert.NotNull(clientEvents.OnChange);
        }

        [Fact]
        public void OnChange_method_with_string_param_should_set_OnChange_property()
        {
            const string param = "my_method()";

            builder.OnChange(param);

            Assert.NotNull(clientEvents.OnChange);
        }

        [Fact]
        public void OnChange_method_with_Action_param_should_return_builder()
        {
            Action param = () => { };

            var returned = builder.OnChange(param);

            Assert.IsType(typeof(TextboxBaseClientEventsBuilder), returned);
        }

        [Fact]
        public void OnChange_method_with_string_param_should_return_builder()
        {
            const string param = "my_method()";

            var returned = builder.OnChange(param);

            Assert.IsType(typeof(TextboxBaseClientEventsBuilder), returned);
        }

        [Fact]
        public void Loaded_with_Action_param_should_set_Loaded_property()
        {
            Action param = () => { };

            builder.OnLoad(param);

            Assert.NotNull(clientEvents.OnLoad);
        }

        [Fact]
        public void Loaded_with_String_param_should_set_Loaded_property()
        {
            const string param = "my_method()";

            builder.OnLoad(param);

            Assert.NotNull(clientEvents.OnLoad);
        }

        [Fact]
        public void Loaded_with_Action_should_return_builder()
        {
            Action param = () => { };

            var returned = builder.OnLoad(param);

            Assert.IsType(typeof(TextboxBaseClientEventsBuilder), returned);
        }

        [Fact]
        public void Loaded_with_String_should_return_builder()
        {
            const string param = "my_method()";

            var returned = builder.OnLoad(param);

            Assert.IsType(typeof(TextboxBaseClientEventsBuilder), returned);
        }
    }
}
