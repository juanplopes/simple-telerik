<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage" %>
<asp:content contentPlaceHolderID="MainContent" runat="server">
   
    <%= Html.Telerik().TreeView()
            .Name("TreeView")
            .Items(item =>
            {
                item.Add()
                    .Text("Mail")
                    .ImageUrl("~/Content/TreeView/FirstLook/mail.gif")
                    .ImageHtmlAttributes(new { alt = "Mail" })
                    .Items(subItem =>
                    {
                        subItem.Add()
                               .Text("Personal Folders")
                               .ImageUrl("~/Content/TreeView/FirstLook/mailPersonalFolders.gif")
                               .ImageHtmlAttributes(new { alt = "Personal Folders" });

                        subItem.Add()
                               .Text("Deleted Items")
                               .ImageUrl("~/Content/TreeView/FirstLook/mailDeletedItems.gif")
                               .ImageHtmlAttributes(new { alt = "Deleted Items" });

                        subItem.Add()
                               .Text("Inbox")
                               .ImageUrl("~/Content/TreeView/FirstLook/mailInbox.gif")
                               .ImageHtmlAttributes(new { alt = "Inbox" });

                        subItem.Add()
                               .Text("My Mail")
                               .ImageUrl("~/Content/TreeView/FirstLook/mailFolder.gif")
                               .ImageHtmlAttributes(new { alt = "My Mail" });

                        subItem.Add()
                               .Text("Sent Items")
                               .ImageUrl("~/Content/TreeView/FirstLook/mailSent.gif")
                               .ImageHtmlAttributes(new { alt = "Sent Items" });

                        subItem.Add()
                               .Text("Outbox")
                               .ImageUrl("~/Content/TreeView/FirstLook/mailOutbox.gif")
                               .ImageHtmlAttributes(new { alt = "Outbox" });

                        subItem.Add()
                               .Text("Search Folders")
                               .ImageUrl("~/Content/TreeView/FirstLook/mailSearch.gif")
                               .ImageHtmlAttributes(new { alt = "Search Folders" });
                    });

                item.Add()
                    .Text("Contacts")
                    .ImageUrl("~/Content/TreeView/FirstLook/contacts.gif")
                    .ImageHtmlAttributes(new { alt = "Contacts" })
                    .Items((subItem) =>
                    {
                        subItem.Add()
                               .Text("My Contacts")
                               .ImageUrl("~/Content/TreeView/FirstLook/contactsItems.gif")
                               .ImageHtmlAttributes(new { alt = "Contact item" });

                        subItem.Add()
                               .Text("Address Cards")
                               .ImageUrl("~/Content/TreeView/FirstLook/contactsItems.gif")
                               .ImageHtmlAttributes(new { alt = "Contact item" });

                        subItem.Add()
                               .Text("Phone List")
                               .ImageUrl("~/Content/TreeView/FirstLook/contactsItems.gif")
                               .ImageHtmlAttributes(new { alt = "Contact item" });

                        subItem.Add()
                               .Text("Shared Contacts")
                               .ImageUrl("~/Content/TreeView/FirstLook/contactsItems.gif")
                               .ImageHtmlAttributes(new { alt = "Contact item" });
                    });

                item.Add()
                    .Text("Tasks")
                    .ImageUrl("~/Content/TreeView/FirstLook/tasks.gif")
                    .ImageHtmlAttributes(new { alt = "Tasks" })
                    .Items((subItem) =>
                    {
                        subItem.Add()
                               .Text("My Tasks")
                               .ImageUrl("~/Content/TreeView/FirstLook/tasksItems.gif")
                               .ImageHtmlAttributes(new { alt = "Task item" });

                        subItem.Add()
                               .Text("Shared Tasks")
                               .ImageUrl("~/Content/TreeView/FirstLook/tasksItems.gif")
                               .ImageHtmlAttributes(new { alt = "Task item" });

                        subItem.Add()
                               .Text("Active Tasks")
                               .ImageUrl("~/Content/TreeView/FirstLook/tasksItems.gif")
                               .ImageHtmlAttributes(new { alt = "Task item" });

                        subItem.Add()
                               .Text("Completed Tasks")
                               .ImageUrl("~/Content/TreeView/FirstLook/tasksItems.gif")
                               .ImageHtmlAttributes(new { alt = "Task item" });
                    });

                item.Add()
                    .Text("Notes")
                    .ImageUrl("~/Content/TreeView/FirstLook/notes.gif")
                    .ImageHtmlAttributes(new { alt = "Notes" })
                    .Items((subItem) =>
                    {
                        subItem.Add()
                               .Text("My Notes")
                               .ImageUrl("~/Content/TreeView/FirstLook/notesItems.gif")
                               .ImageHtmlAttributes(new { alt = "Note item" });

                        subItem.Add()
                               .Text("Notes List")
                               .ImageUrl("~/Content/TreeView/FirstLook/notesItems.gif")
                               .ImageHtmlAttributes(new { alt = "Note item" });

                        subItem.Add()
                               .Text("Shared Notes")
                               .ImageUrl("~/Content/TreeView/FirstLook/notesItems.gif")
                               .ImageHtmlAttributes(new { alt = "Note item" });

                        subItem.Add()
                               .Text("Archive")
                               .ImageUrl("~/Content/TreeView/FirstLook/notesItems.gif")
                               .ImageHtmlAttributes(new { alt = "Note item" });
                    });

                item.Add()
                    .Text("Folders List")
                    .ImageUrl("~/Content/TreeView/FirstLook/foldersList.gif")
                    .ImageHtmlAttributes(new { alt = "Folders List" })
                    .Items((subItem) =>
                    {
                        subItem.Add()
                               .Text("My Client.Net")
                               .ImageUrl("~/Content/TreeView/FirstLook/mailFolder.gif")
                               .ImageHtmlAttributes(new { alt = "Mail Outbox" });

                        subItem.Add()
                               .Text("My Profile")
                               .ImageUrl("~/Content/TreeView/FirstLook/mailFolder.gif")
                               .ImageHtmlAttributes(new { alt = "Mail Outbox" });

                        subItem.Add()
                               .Text("My Support Tickets")
                               .ImageUrl("~/Content/TreeView/FirstLook/mailFolder.gif")
                               .ImageHtmlAttributes(new { alt = "Mail Outbox" });

                        subItem.Add()
                               .Text("My Licenses")
                               .ImageUrl("~/Content/TreeView/FirstLook/mailFolder.gif")
                               .ImageHtmlAttributes(new { alt = "Mail Outbox" });
                    });
            })
    %>
		
</asp:content>