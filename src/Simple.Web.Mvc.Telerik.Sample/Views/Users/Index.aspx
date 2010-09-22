<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IPage<TUser>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Index
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>
        Index</h2>
    <%=  Html.Telerik().Grid(Model)
        .Name("Grid")
        .EnableCustomBinding(true)
        .PrefixUrlParameters(false)
        .Sortable(sort=>sort.SortMode(GridSortMode.MultipleColumn))
        .Filterable()
        .Groupable()
        .Localizable("pt-BR")
        .Pageable(paging => paging.PageSize(30).Style(GridPagerStyles.NextPreviousAndNumeric).Total(Model.TotalCount))
        .Scrollable(scrolling => scrolling.Height(350))
        .Columns(columns =>
        {
            columns.Bound(o => o.Name).Title("Nome");
            columns.Bound(o => o.BirthDate).Title("Data de Nascimento").Format("{0:dd/MM/yyyy}");
            columns.Bound(o => o.Wage).Title("Salário").Format("{0:C2}");
            columns.Bound(o => o.Height).Title("Altura").Format("{0:F2}");
            columns.Bound(o => o.Weight).Title("Peso").Format("{0:F2}");
            columns.Bound(o => o.Sex).Title("Sexo");
        })
            

    %>
</asp:Content>
