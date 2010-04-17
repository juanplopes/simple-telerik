namespace Telerik.Web.Mvc.UI.UnitTest
{
    using System.IO;
    using System.Web.UI;

    using Moq;
    using Xunit;
    using System;

    public class DatePickerBuilderTests
    {
        private readonly DatePicker datePicker;
        private readonly DatePickerBuilder builder;

        public DatePickerBuilderTests()
        {
            datePicker = DatePickerTestHelper.CreateDatePicker(null);
            builder = new DatePickerBuilder(datePicker);
        }

        [Fact]
        public void Theme_should_set_Theme_property_of_datePicker()
        {
            const string theme = "theme";
            builder.Theme(theme);

            Assert.Equal(theme, datePicker.Theme);
        }

        [Fact]
        public void Theme_should_return_builder()
        {
            const string theme = "theme";
            var returnedBuilder = builder.Theme(theme);

            Assert.IsType(typeof(DatePickerBuilder), returnedBuilder);
        }

        [Fact]
        public void Effects_creates_fx_factory()
        {
            var fxFacCreated = false;

            builder.Effects(fx =>
            {
                fxFacCreated = fx != null;
            });

            Assert.True(fxFacCreated);
        }

        [Fact]
        public void ShowButton_should_set_EnableButton_property_of_datePicker()
        {
            const bool showButton = true;
            builder.ShowButton(showButton);

            Assert.Equal(showButton, datePicker.EnableButton);
        }

        [Fact]
        public void ShowButton_should_return_builder()
        {
            const bool showButton = true;
            var returnedBuilder = builder.ShowButton(showButton);

            Assert.IsType(typeof(DatePickerBuilder), returnedBuilder);
        }

        [Fact]
        public void ButtonTitle_should_set_ButtonTitle_property_of_datePicker()
        {
            const string buttonTitle = "button";
            builder.ButtonTitle(buttonTitle);

            Assert.Equal(buttonTitle, datePicker.ButtonTitle);
        }

        [Fact]
        public void ButtonTitle_should_return_builder()
        {
            const string buttonTitle = "button";
            var returnedBuilder = builder.ButtonTitle(buttonTitle);

            Assert.IsType(typeof(DatePickerBuilder), returnedBuilder);
        }

        [Fact]
        public void DateFormat_should_set_DateFormat_of_the_picker() 
        {
            const string dateFormat = "dd/MM/yyyy";

            builder.Format(dateFormat);

            Assert.Equal(dateFormat, datePicker.Format);
        }

        [Fact]
        public void DateFormat_should_return_builder()
        {
            const string dateFormat = "dd/MM/yyyy";
            var returnedBuilder = builder.Format(dateFormat);

            Assert.IsType(typeof(DatePickerBuilder), returnedBuilder);
        }

        [Fact]
        public void SelectedDate_should_set_SelectedDate_of_the_picker()
        {
            var date = DateTime.Now;
            builder.Value(date);

            Assert.Equal(date, datePicker.Value);
        }

        [Fact]
        public void SelectedDate_should_return_builder()
        {
            var returnedBuilder = builder.Value(DateTime.Now);

            Assert.IsType(typeof(DatePickerBuilder), returnedBuilder);
        }

        [Fact]
        public void SelectedDate_with_string_should_set_SelectedDate_of_the_picker()
        {
            var date = new DateTime(2000, 10, 10);
            builder.Value(date.ToShortDateString());

            Assert.Equal(date, datePicker.Value);
        }

        [Fact]
        public void SelectedDate_with_string_should_throw_exception_if_incorrect_string_is_passed()
        {
            Assert.Throws(typeof(ArgumentException), () => builder.Value("incorrect"));
        }

        [Fact]
        public void SelectedDate_with_string_should_return_builder()
        {
            var returnedBuilder = builder.Value(DateTime.Now.ToShortDateString());

            Assert.IsType(typeof(DatePickerBuilder), returnedBuilder);
        }

        [Fact]
        public void MinDate_should_set_MinDate_of_the_picker()
        {
            DateTime date = DateTime.Now;

            builder.MinDate(date);

            Assert.Equal(date, datePicker.MinDate);
        }

        [Fact]
        public void MinDate_should_return_builder()
        {
            var returnedBuilder = builder.MinDate(DateTime.Now);

            Assert.IsType(typeof(DatePickerBuilder), returnedBuilder);
        }

        [Fact]
        public void MinDate_with_string_should_set_MinDate_of_the_picker()
        {
            var date = new DateTime(2000, 10, 10);
            builder.MinDate(date.ToShortDateString());

            Assert.Equal(date, datePicker.MinDate);
        }

        [Fact]
        public void MinDate_with_string_should_throw_exception_if_incorrect_string_is_passed()
        {
            Assert.Throws(typeof(ArgumentException), () => builder.MinDate("incorrect"));
        }

        [Fact]
        public void MinDate_with_string_should_return_builder()
        {
            var returnedBuilder = builder.MinDate(DateTime.Now.ToShortDateString());

            Assert.IsType(typeof(DatePickerBuilder), returnedBuilder);
        }

        [Fact]
        public void MaxDate_should_set_MaxDate_of_the_picker()
        {
            DateTime date = DateTime.Now;
            builder.MaxDate(date);

            Assert.Equal(date, datePicker.MaxDate);
        }

        [Fact]
        public void MaxDate_should_return_builder()
        {
            var returnedBuilder = builder.MaxDate(DateTime.Now);

            Assert.IsType(typeof(DatePickerBuilder), returnedBuilder);
        }

        [Fact]
        public void MaxDate_with_string_should_set_MaxDate_of_the_picker()
        {
            var date = new DateTime(2000, 10, 10);
            builder.MaxDate(date.ToShortDateString());

            Assert.Equal(date, datePicker.MaxDate);
        }

        [Fact]
        public void MaxDate_with_string_should_throw_exception_if_incorrect_string_is_passed()
        {
            Assert.Throws(typeof(ArgumentException), () => builder.MaxDate("incorrect"));
        }

        [Fact]
        public void MaxDate_with_string_should_return_builder()
        {
            var returnedBuilder = builder.MaxDate(DateTime.Now.ToShortDateString());

            Assert.IsType(typeof(DatePickerBuilder), returnedBuilder);
        }

        [Fact]
        public void ClientEvents_should_set_events_of_the_datepicker()
        {
            Action<DatePickerClientEventsBuilder> clientEventsAction = eventBuilder => { eventBuilder.OnLoad("Load"); };

            builder.ClientEvents(clientEventsAction);

            Assert.NotNull(datePicker.ClientEvents.OnLoad);
        }

        [Fact]
        public void ClientEvents_should_return_builder()
        {
            Action<DatePickerClientEventsBuilder> clientEventsAction = eventBuilder => { eventBuilder.OnLoad("Load"); };

            var returnedBuilder = builder.ClientEvents(clientEventsAction);

            Assert.IsType(typeof(DatePickerBuilder), returnedBuilder);
        }

        [Fact]
        public void InputHtmlAttributes_should_set_InputHtmlAttributes_property_of_datePicker()
        {
            var attributes = new { @class = ".t-test"};

            builder.InputHtmlAttributes(attributes);

            Assert.Equal(".t-test", datePicker.InputHtmlAttributes["class"]);
        }

        [Fact]
        public void InputHtmlAttributes_should_return_builder()
        {
            var attributes = new { @class = ".t-test" };
            var returnedBuilder = builder.InputHtmlAttributes(attributes);

            Assert.IsType(typeof(DatePickerBuilder), returnedBuilder);
        }
    }
}
