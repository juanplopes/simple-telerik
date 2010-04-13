namespace Telerik.Web.Mvc.Examples
{
    using System.Collections.Generic;
    using System.Data.Linq;
    using System.Linq;
    using System.Web.Mvc;

    using Models;

    [AutoPopulateSourceCode]
    [PopulateProductSiteMap(SiteMapName = "examples", ViewDataKey = "telerik.mvc.examples")]
    public partial class GridController : Controller
    {
        private static IEnumerable<Order> GetOrders()
        {
            NorthwindDataContext northwind = new NorthwindDataContext();

            DataLoadOptions loadOptions = new DataLoadOptions();

            loadOptions.LoadWith<Order>(o => o.Customer);
            northwind.LoadOptions = loadOptions;

            return northwind.Orders;
        }
        
        private static IEnumerable<OrderDto> GetOrderDto()
        {
            NorthwindDataContext northwind = new NorthwindDataContext();

            DataLoadOptions loadOptions = new DataLoadOptions();

            loadOptions.LoadWith<Order>(o => o.Customer);
            northwind.LoadOptions = loadOptions;

            return northwind.Orders.Select(order => new OrderDto
                {
                    ContactName = order.Customer.ContactName,
                    OrderDate = order.OrderDate,
                    OrderID = order.OrderID,
                    ShipAddress = order.ShipAddress
                });
        }

        private static IEnumerable<Order> GetOrdersForCustomer(string customerId)
        {
            NorthwindDataContext northwind = new NorthwindDataContext();

            return from order in northwind.Orders
                   where order.CustomerID == customerId
                   select order;
        }

        private static IQueryable<Customer> GetCustomers()
        {
            NorthwindDataContext northwind = new NorthwindDataContext();
            return northwind.Customers;
        }
    }
}