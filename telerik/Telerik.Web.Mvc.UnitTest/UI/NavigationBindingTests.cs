namespace Telerik.Web.Mvc.UI.UnitTest
{
    using Xunit;

    public class NavigationBindingTests
    {
        private readonly NavigationBindingTestDouble binding;

        public NavigationBindingTests()
        {
            binding = new NavigationBindingTestDouble();
        }
    }

    public class NavigationBindingTestDouble : NavigationBinding<NavigationItemTestDouble, TestObject> 
    {   }
}
