<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<EditableProduct>" %>
<asp:content contentPlaceHolderID="MainContent" runat="server">
    
    <% using (Html.BeginForm())
       { %>
        <div id="field-list" class="t-group">
            <h3>Product Details</h3>
            <ul>
                <li>
                    <span class="label">Product ID</span><%= Html.DisplayFor(m => m.ProductID) %>
                    <%= Html.HiddenFor(m => m.ProductID) %>
                </li>
                <li>
                    <%= Html.LabelFor(m => m.ProductName) %><%= Html.EditorFor(m => m.ProductName) %>
                </li>
                <li>
                    <%= Html.LabelFor(m => m.UnitPrice) %><%= Html.EditorFor(m => m.UnitPrice) %>
                </li>
                <li>
                    <%= Html.LabelFor(m => m.UnitsInStock) %><%= Html.EditorFor(m => m.UnitsInStock) %>
                </li>
            </ul>
            <button type="submit" class="t-button t-state-default">Save</button>
        </div>
    <% } %>
</asp:content>

<asp:content contentPlaceHolderId="HeadContent" runat="server">

    <style type="text/css">        
        #field-list
        {
            display: inline-block; *display: inline; zoom: 1;
            overflow: hidden;
            border-width: 1px;
            border-style: solid;
            padding: 0 2em 2em;
        } 
        
        #field-list h3
        {
            border-bottom: 1px solid;
        }
        
        #field-list ul
        {
            margin: 0; padding: 0;
        }
        
        #field-list li
        {
            list-style-type: none;
            padding-bottom: 5px;
        }
        
        #field-list label,
        #field-list .label
        {
            display: inline-block; *display: inline; zoom: 1;
            width: 80px; text-align: right; padding-right: 5px;
            padding-top: 2px;
        }
        
        #field-list label
        {
            vertical-align: top;
        }
        
        #field-list input,
        #field-list textarea
        {
            font: normal 12px Tahoma;
            width: 130px;
        }
        
        #field-list button
        {
            margin: 19px 0 0 160px;
            width: 60px;
        }
    </style>
</asp:content>