<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage" %>
<asp:Content contentPlaceHolderID="MainContent" runat="server">
   
    <% Html.Telerik().PanelBar()
            .Name("PanelBar")
            .HtmlAttributes(new {
                style = "width: 300px; float: right; margin-bottom: 30px;",
                @class = "t-panelbar-rtl"
            })
            .ExpandMode(PanelBarExpandMode.Multiple)
            .SelectedIndex(0)
            .Items(item =>
            {
                item.Add()
                    .Text("Mail")
                    .ImageUrl("~/Content/PanelBar/FirstLook/mail.gif")
                    .ImageHtmlAttributes(new { alt = "Mail" })
                    .Items(subItem =>
                    {
                        subItem.Add()
                               .Text("Personal Folders")
                               .ImageUrl("~/Content/PanelBar/FirstLook/mailPersonalFolders.gif")
                               .ImageHtmlAttributes(new { alt = "Personal Folders" });

                        subItem.Add()
                               .Text("Deleted Items")
                               .ImageUrl("~/Content/PanelBar/FirstLook/mailDeletedItems.gif")
                               .ImageHtmlAttributes(new { alt = "Deleted Items" });

                        subItem.Add()
                               .Text("Inbox")
                               .ImageUrl("~/Content/PanelBar/FirstLook/mailInbox.gif")
                               .ImageHtmlAttributes(new { alt = "Inbox" }).Enabled(false);

                        subItem.Add()
                               .Text("My Mail")
                               .ImageUrl("~/Content/PanelBar/FirstLook/mailFolder.gif")
                               .ImageHtmlAttributes(new { alt = "My Mail" });

                        subItem.Add()
                               .Text("Sent Items")
                               .ImageUrl("~/Content/PanelBar/FirstLook/mailSent.gif")
                               .ImageHtmlAttributes(new { alt = "Sent Items" });

                        subItem.Add()
                               .Text("Outbox")
                               .ImageUrl("~/Content/PanelBar/FirstLook/mailOutbox.gif")
                               .ImageHtmlAttributes(new { alt = "Outbox" });

                        subItem.Add()
                               .Text("Search Folders")
                               .ImageUrl("~/Content/PanelBar/FirstLook/mailSearch.gif")
                               .ImageHtmlAttributes(new { alt = "Search Folders" });
                    });

                item.Add()
                    .Text("Contacts")
                    .ImageUrl("~/Content/PanelBar/FirstLook/contacts.gif")
                    .ImageHtmlAttributes(new { alt = "Contacts" }).Enabled(false)
                    .Items((subItem) => 
                    {
                        subItem.Add()
                               .Text("My Contacts")
                               .ImageUrl("~/Content/PanelBar/FirstLook/contactsItems.gif")
                               .ImageHtmlAttributes(new { alt = "Contact item" });

                        subItem.Add()
                               .Text("Address Cards")
                               .ImageUrl("~/Content/PanelBar/FirstLook/contactsItems.gif")
                               .ImageHtmlAttributes(new { alt = "Contact item" });

                        subItem.Add()
                               .Text("Phone List")
                               .ImageUrl("~/Content/PanelBar/FirstLook/contactsItems.gif")
                               .ImageHtmlAttributes(new { alt = "Contact item" });

                        subItem.Add()
                               .Text("Shared Contacts")
                               .ImageUrl("~/Content/PanelBar/FirstLook/contactsItems.gif")
                               .ImageHtmlAttributes(new { alt = "Contact item" });
                    });

                item.Add()
                    .Text("Tasks")
                    .ImageUrl("~/Content/PanelBar/FirstLook/tasks.gif")
                    .ImageHtmlAttributes(new { alt = "Tasks" })
                    .Items((subItem) =>
                    {
                        subItem.Add()
                               .Text("My Tasks")
                               .ImageUrl("~/Content/PanelBar/FirstLook/tasksItems.gif")
                               .ImageHtmlAttributes(new { alt = "Task item" });

                        subItem.Add()
                               .Text("Shared Tasks")
                               .ImageUrl("~/Content/PanelBar/FirstLook/tasksItems.gif")
                               .ImageHtmlAttributes(new { alt = "Task item" });

                        subItem.Add()
                               .Text("Active Tasks")
                               .ImageUrl("~/Content/PanelBar/FirstLook/tasksItems.gif")
                               .ImageHtmlAttributes(new { alt = "Task item" });

                        subItem.Add()
                               .Text("Completed Tasks")
                               .ImageUrl("~/Content/PanelBar/FirstLook/tasksItems.gif")
                               .ImageHtmlAttributes(new { alt = "Task item" });
                    });

                item.Add()
                    .Text("Notes")
                    .ImageUrl("~/Content/PanelBar/FirstLook/notes.gif")
                    .ImageHtmlAttributes(new { alt = "Notes" })
                    .Items((subItem) =>
                    {
                        subItem.Add()
                               .Text("My Notes")
                               .ImageUrl("~/Content/PanelBar/FirstLook/notesItems.gif")
                               .ImageHtmlAttributes(new { alt = "Note item" });

                        subItem.Add()
                               .Text("Notes List")
                               .ImageUrl("~/Content/PanelBar/FirstLook/notesItems.gif")
                               .ImageHtmlAttributes(new { alt = "Note item" });

                        subItem.Add()
                               .Text("Shared Notes")
                               .ImageUrl("~/Content/PanelBar/FirstLook/notesItems.gif")
                               .ImageHtmlAttributes(new { alt = "Note item" });

                        subItem.Add()
                               .Text("Archive")
                               .ImageUrl("~/Content/PanelBar/FirstLook/notesItems.gif")
                               .ImageHtmlAttributes(new { alt = "Note item" });
                    });

                item.Add()
                    .Text("Folders List")
                    .ImageUrl("~/Content/PanelBar/FirstLook/foldersList.gif")
                    .ImageHtmlAttributes(new { alt = "Folders List" })
                    .Items((subItem) =>
                    {
                        subItem.Add()
                               .Text("My Client.Net")
							   .ImageUrl("~/Content/PanelBar/FirstLook/mailFolder.gif")
                               .ImageHtmlAttributes(new { alt = "Mail Outbox" });

                        subItem.Add()
                               .Text("My Profile")
							   .ImageUrl("~/Content/PanelBar/FirstLook/mailFolder.gif")
                               .ImageHtmlAttributes(new { alt = "Mail Outbox" });

                        subItem.Add()
                               .Text("My Support Tickets")
							   .ImageUrl("~/Content/PanelBar/FirstLook/mailFolder.gif")
                               .ImageHtmlAttributes(new { alt = "Mail Outbox" });

                        subItem.Add()
                               .Text("My Licenses")
							   .ImageUrl("~/Content/PanelBar/FirstLook/mailFolder.gif")
                               .ImageHtmlAttributes(new { alt = "Mail Outbox" });
                    });
            })
            .Render(); 
    %>
		
</asp:Content>
