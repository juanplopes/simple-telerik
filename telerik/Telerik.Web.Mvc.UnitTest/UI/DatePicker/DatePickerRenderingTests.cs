using Telerik.Web.Mvc.Infrastructure;
using Telerik.Web.Mvc.UI;
namespace Telerik.Web.Mvc.UI.UnitTest
{
    using Moq;
    using Xunit;

    using System.IO;
    using System.Web.UI;
    using System;
    using Telerik.Web.Mvc.Infrastructure;
    using System.Collections.Generic;

    public class DatePickerRenderingTests
    {
        private readonly DatePicker datePicker;
        private readonly Mock<IDatePickerHtmlBuilder> tagBuilder;
        private readonly Mock<IHtmlNode> rootTag;
        Mock<TextWriter> textWriter;

        public DatePickerRenderingTests()
        {
            textWriter = new Mock<TextWriter>();
            
            tagBuilder = new Mock<IDatePickerHtmlBuilder>();
            rootTag = new Mock<IHtmlNode>();
            rootTag.SetupGet(t => t.Children).Returns(() => new List<IHtmlNode>());

            tagBuilder.Setup(t => t.Build()).Returns(rootTag.Object);

            datePicker = DatePickerTestHelper.CreateDatePicker(tagBuilder.Object);
            datePicker.Name = "DatePicker";
        }

        [Fact]
        public void Render_should_output_DatePicker_start() 
        {
            tagBuilder.Setup(t => t.Build()).Returns(rootTag.Object);

            datePicker.Render();

            tagBuilder.Verify();
        }

        [Fact]
        public void Render_should_output_Input() 
        {
            tagBuilder.Setup(t => t.Build()).Returns(rootTag.Object);

            tagBuilder.Setup(r => r.InputTag()).Verifiable();

            datePicker.Render();

            tagBuilder.Verify();
        }

        [Fact]
        public void Render_should_output_Image_if_it_is_allowed()
        {
            datePicker.EnableButton = true;

            tagBuilder.Setup(t => t.Build()).Returns(rootTag.Object);

            tagBuilder.Setup(r => r.ButtonTag()).Verifiable();

            datePicker.Render();

            tagBuilder.Verify();
        }

        [Fact]
        public void Render_should_not_output_Image_if_it_is_disabled()
        {
            datePicker.EnableButton = false;

            tagBuilder.Setup(t => t.Build()).Returns(rootTag.Object);

            tagBuilder.Setup(r => r.ButtonTag()).Verifiable();

            datePicker.Render();

            tagBuilder.Verify(r => r.ButtonTag(), Times.Never());
        }

        [Fact]
        public void Render_should_call_objectWriter_start_method()
        {
            DatePickerTestHelper.clientSideObjectWriter.Setup(ow => ow.Start()).Verifiable();

            datePicker.WriteInitializationScript(textWriter.Object);

            DatePickerTestHelper.clientSideObjectWriter.Verify(ow => ow.Start());
        }

        [Fact]
        public void ObjectWriter_should_call_objectWriter_complete_method()
        {
            DatePickerTestHelper.clientSideObjectWriter.Setup(w => w.Complete());

            datePicker.WriteInitializationScript(textWriter.Object);

            DatePickerTestHelper.clientSideObjectWriter.Verify(w => w.Complete());
        }

        [Fact]
        public void ObjectWriter_should_append_Load_property_of_clientEvents()
        {
            datePicker.ClientEvents.OnLoad = () => { };

            DatePickerTestHelper.clientSideObjectWriter.Setup(w => w.Append("onLoad", datePicker.ClientEvents.OnLoad)).Verifiable();

            datePicker.WriteInitializationScript(textWriter.Object);

            DatePickerTestHelper.clientSideObjectWriter.Verify(w => w.Append("onLoad", datePicker.ClientEvents.OnLoad));
        }

        [Fact]
        public void ObjectWriter_should_append_Select_property_of_clientEvents()
        {
            datePicker.ClientEvents.OnChange = () => { };

            DatePickerTestHelper.clientSideObjectWriter.Setup(w => w.Append("onChange", datePicker.ClientEvents.OnChange)).Verifiable();

            datePicker.WriteInitializationScript(textWriter.Object);

            DatePickerTestHelper.clientSideObjectWriter.Verify(w => w.Append("onChange", datePicker.ClientEvents.OnChange));
        }

        [Fact]
        public void ObjectWriter_should_append_PopUpOpen_property_of_clientEvents()
        {
            datePicker.ClientEvents.OnOpen = () => { };

            DatePickerTestHelper.clientSideObjectWriter.Setup(w => w.Append("onOpen", datePicker.ClientEvents.OnOpen)).Verifiable();

            datePicker.WriteInitializationScript(textWriter.Object);

            DatePickerTestHelper.clientSideObjectWriter.Verify(w => w.Append("onOpen", datePicker.ClientEvents.OnOpen));
        }

        [Fact]
        public void ObjectWriter_should_append_PopUpClose_property_of_clientEvents()
        {
            datePicker.ClientEvents.OnClose = () => { };

            DatePickerTestHelper.clientSideObjectWriter.Setup(w => w.Append("onClose", datePicker.ClientEvents.OnClose)).Verifiable();

            datePicker.WriteInitializationScript(textWriter.Object);

            DatePickerTestHelper.clientSideObjectWriter.Verify(w => w.Append("onClose", datePicker.ClientEvents.OnClose));
        }

        [Fact]
        public void ObjectWriter_should_append_DateFormat_property()
        {            
            datePicker.Format = "dd/MM/yyyy";

            DatePickerTestHelper.clientSideObjectWriter.Setup(w => w.Append("format", "dd/MM/yyyy")).Verifiable();

            datePicker.WriteInitializationScript(textWriter.Object);

            DatePickerTestHelper.clientSideObjectWriter.Verify(w => w.Append("format", "dd/MM/yyyy"));
        }

        [Fact]
        public void ObjectWriter_should_append_SelectedDate_property()
        {
            DateTime? date = new DateTime(2000, 12, 20);

            datePicker.Value = date;

            DatePickerTestHelper.clientSideObjectWriter.Setup(w => w.AppendDateOnly("selectedDate", date)).Verifiable();

            datePicker.WriteInitializationScript(textWriter.Object);

            DatePickerTestHelper.clientSideObjectWriter.Verify(w => w.AppendDateOnly("selectedDate", date));
        }

        [Fact]
        public void ObjectWriter_should_append_MinDate_property()
        {
            DateTime date = new DateTime(1900, 12, 20);

            datePicker.MinDate = date;

            DatePickerTestHelper.clientSideObjectWriter.Setup(w => w.AppendDateOnly("minDate", date)).Verifiable();

            datePicker.WriteInitializationScript(textWriter.Object);

            DatePickerTestHelper.clientSideObjectWriter.Verify(w => w.AppendDateOnly("minDate", date));
        }

        [Fact]
        public void ObjectWriter_should_append_MaxDate_property()
        {
            DateTime date = new DateTime(2100, 12, 20);

            datePicker.MaxDate = date;

            DatePickerTestHelper.clientSideObjectWriter.Setup(w => w.AppendDateOnly("maxDate", date)).Verifiable();

            datePicker.WriteInitializationScript(textWriter.Object);

            DatePickerTestHelper.clientSideObjectWriter.Verify(w => w.AppendDateOnly("maxDate", date));
        }

        [Fact]
        public void Render_should_throw_exception_if_selectedDate_is_out_of_limits() 
        {
            datePicker.MinDate = DateTime.Now.AddMonths(1);
            datePicker.Value = DateTime.Now;

            Assert.Throws(typeof(ArgumentOutOfRangeException), () => datePicker.Render());
        }

        [Fact]
        public void Render_should_throw_exception_if_minDate_is_bigger_than_maxDate()
        {
            DateTime date = DateTime.Now;
            datePicker.MaxDate = date;
            datePicker.MinDate = date.AddMonths(1);

            Assert.Throws(typeof(ArgumentException), () => datePicker.Render());
        }

        [Fact]
        public void Render_should_not_throw_exception_if_value_is_equal_to_maxDate() 
        {
            DateTime date = DateTime.Today;
            datePicker.Value = date;
            datePicker.MaxDate = date;

            Assert.DoesNotThrow(() => datePicker.Render());
        }

        [Fact]
        public void Render_should_not_throw_exception_if_value_is_equal_to_minDate()
        {
            DateTime date = DateTime.Today;
            datePicker.Value = date;
            datePicker.MinDate = date;

            Assert.DoesNotThrow(() => datePicker.Render());
        }

        [Fact]
        public void MinDate_should_set_throw_exception_if_less_than_minDate()
        {
            DateTime date = DateTime.Now;
            datePicker.MinDate = date;
            datePicker.MaxDate = date.AddMonths(-1);

            Assert.Throws(typeof(ArgumentException), () => datePicker.Render());
        }
    }
}
