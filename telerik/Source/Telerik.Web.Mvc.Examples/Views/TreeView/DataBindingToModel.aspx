<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<IEnumerable<Category>>" %>


<asp:content contentplaceholderid="MainContent" runat="server">

<%= Html.Telerik().TreeView()
        .Name("TreeView")
        .BindTo(Model, mappings =>
        {
            mappings.For<Category>(binding => binding
                    .ItemDataBound((item, category) =>
                    {
                        item.Text = category.CategoryName;
                    })
                    .Children(category => category.Products));
            
            mappings.For<Product>(binding => binding
                    .ItemDataBound((item, product) =>
                    {
                        item.Text = product.ProductName;
                    }));
        })
%>

</asp:content>
