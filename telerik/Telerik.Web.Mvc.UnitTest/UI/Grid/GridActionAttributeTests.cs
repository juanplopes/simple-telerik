namespace Telerik.Web.Mvc.UI.UnitTest.Grid
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.ComponentModel;
    using System.Web.Mvc;

    using Moq;
    using Xunit;
    
    public class GridActionAttributeTests
    {
        private readonly GridActionAttribute gridAttribute;
        private readonly Mock<ActionExecutedContext> filterExecutedContext;
        private readonly Mock<ActionExecutingContext> filterExecutingContext;
        private readonly IDictionary<string, ValueProviderResult> valueProvider;
        private readonly ViewDataDictionary viewData;

        public GridActionAttributeTests()
        {
            filterExecutedContext = new Mock<ActionExecutedContext>();

            NameValueCollection headers = new NameValueCollection();
            headers["X-Requested-With"] = "XMLHttpRequest";

            filterExecutedContext.SetupGet(c => c.HttpContext).Returns(TestHelper.CreateMockedHttpContext().Object);
            filterExecutedContext.SetupGet(c => c.HttpContext.Request.Headers).Returns(headers);
            filterExecutedContext.Object.Result = new ViewResult();

            filterExecutingContext = new Mock<ActionExecutingContext>();
            filterExecutingContext.SetupGet(c => c.HttpContext).Returns(TestHelper.CreateMockedHttpContext().Object);
            filterExecutingContext.SetupGet(c => c.HttpContext.Request.Headers).Returns(headers);

            IDictionary<string, object> actionParameters = new Dictionary<string, object>{ { "command", null } };
            
            filterExecutingContext.SetupGet(c => c.ActionParameters).Returns(actionParameters);

            valueProvider = new Dictionary<string, ValueProviderResult>();
            viewData = new ViewDataDictionary();

            ControllerBase controller = new ControllerTestDouble(valueProvider, viewData);

            filterExecutedContext.SetupGet(c => c.Controller).Returns(controller);
            filterExecutingContext.SetupGet(c => c.Controller).Returns(controller);

            gridAttribute = new GridActionAttribute();
        }

        [Fact]
        public void OnActionExecuted_updates_the_result()
        {
            IEnumerable dataSource =  new object[] { };
            filterExecutedContext.Object.Result = new JsonResult { Data = dataSource };
            gridAttribute.OnActionExecuted(filterExecutedContext.Object);

            JsonResult result = (JsonResult) filterExecutedContext.Object.Result;

            Assert.NotNull(result);
            Assert.NotNull(result.Data);
        }

        [Fact]
        public void OnActionExecuting_updates_the_grid_command_parameter()
        {
            valueProvider.Add(GridUrlParameters.CurrentPage, "1");
            valueProvider.Add(GridUrlParameters.OrderBy, "Name-asc");
            valueProvider.Add(GridUrlParameters.Filter, "Age~eq~1");
            valueProvider.Add(GridUrlParameters.PageSize, "42");

            gridAttribute.OnActionExecuting(filterExecutingContext.Object);

            GridCommand gridCommand = (GridCommand)filterExecutingContext.Object.ActionParameters["command"];

            Assert.Equal(1, gridCommand.Page);
            Assert.Equal("Name", gridCommand.SortDescriptors[0].Member);
            Assert.Equal("Age", ((FilterDescriptor)gridCommand.FilterDescriptors[0]).Member);
            Assert.Equal(42, gridCommand.PageSize);
        }
    }
}