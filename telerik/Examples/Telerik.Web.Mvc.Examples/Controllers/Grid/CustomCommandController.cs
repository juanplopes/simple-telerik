namespace Telerik.Web.Mvc.Examples
{
    using System.Web.Mvc;
    using System.Collections;
    using System.Linq;
    using Telerik.Web.Mvc.Extensions;
    using Telerik.Web.Mvc.Examples.Models;
    using System.Collections.Generic;
    using System.IO;
    using System.Text;

    public partial class GridController : Controller
    {
        public ActionResult CustomCommand()
        {
            return View(GetOrders());
        }

        [GridAction]
        public ActionResult _CustomCommand()
        {
            return View(new GridModel(GetOrders()));
        }

        public ActionResult ExportCsv(int page, string orderBy, string filter)
        {
            IEnumerable orders = GetOrders().AsQueryable().ToGridModel(page, 10, orderBy, string.Empty, filter).Data;

            MemoryStream output = new MemoryStream();
            StreamWriter writer = new StreamWriter(output, Encoding.UTF8);

            writer.Write("OrderID,");
            writer.Write("ContactName,");
            writer.Write("ShipAddress,");
            writer.Write("OrderDate");
            writer.WriteLine();

            foreach (Order order in orders)
            {
                writer.Write(order.OrderID);
                writer.Write(",");

                writer.Write("\"");
                writer.Write(order.Customer.ContactName);
                writer.Write("\"");
                writer.Write(",");

                writer.Write("\"");
                writer.Write(order.ShipAddress);
                writer.Write("\"");
                writer.Write(",");

                writer.Write(order.OrderDate.Value.ToShortDateString());
                writer.WriteLine();
            }

            writer.Flush();
            output.Position = 0;
            
            return File(output, "text/comma-separated-values", "Orders.csv");
        }
    }
}