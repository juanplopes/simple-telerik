// (c) Copyright 2002-2010 Telerik 
// This source is subject to the GNU General Public License, version 2
// See http://www.gnu.org/licenses/gpl-2.0.html. 
// All other rights reserved.

namespace Telerik.Web.Mvc.UI
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Web.Mvc;
    using System.Web.UI;
    using Telerik.Web.Mvc.Extensions;
    using Telerik.Web.Mvc.UI.Fluent;
    using Telerik.Web.Mvc.UI.Html;
    using System.Collections.Generic;
    using Telerik.Web.Mvc.Infrastructure;

    public class Editor : ViewComponentBase
    {
        public Editor(ViewContext viewContext, IClientSideObjectWriterFactory clientSideObjectWriterFactory)
            : base(viewContext, clientSideObjectWriterFactory)
        {
            ScriptFileNames.AddRange(new[] {
                "telerik.common.js", 
                "telerik.list.js", 
                "telerik.combobox.js", 
                "telerik.draganddrop.js", 
                "telerik.window.js", 
                "telerik.editor.js" 
            });

            DefaultToolGroup = new EditorToolGroup();

            ClientEvents = new EditorClientEvents();

            StyleSheets = new WebAssetItemGroup("default", false) { DefaultPath = WebAssetDefaultSettings.StyleSheetFilesPath };

            Localization = new EditorLocalization();

            Template = new HtmlTemplate();

            new EditorToolFactory(DefaultToolGroup)
                .Bold().Italic().Underline().Strikethrough()
                .Separator()
                .FontName()
                .FontSize()
                .FontColor().BackColor()
                .Separator()
                .JustifyLeft().JustifyCenter().JustifyRight().JustifyFull()
                .Separator()
                .InsertUnorderedList().InsertOrderedList()
                .Separator()
                .Outdent().Indent()
                .Separator()
                .FormatBlock()
                .Separator()
                .CreateLink().Unlink()
                .Separator()
                .InsertImage();
        }

        public EditorClientEvents ClientEvents
        {
            get;
            private set;
        }

        public EditorToolGroup DefaultToolGroup
        {
            get;
            private set;
        }

        public WebAssetItemGroup StyleSheets
        {
            get;
            private set;
        }

        public HtmlTemplate Template
        {
            get;
            private set;
        }

        public string Value
        {
            get
            {
                return Template.Html;
            }
            set
            {
                Template.Html = value;
            }
        }
        
        public Action Content 
        {
            get
            {
                return Template.Content;
            }

            set
            {
                Template.Content = value;
            }
        }

        public bool? Encode
        {
            get;
            set;
        }

        public EditorLocalization Localization
        {
            get;
            set;
        }

        public override void WriteInitializationScript(TextWriter writer)
        {
            IClientSideObjectWriter objectWriter = ClientSideObjectWriterFactory.Create(Id, "tEditor", writer);

            objectWriter.Start();

            ClientEvents.SerializeTo(objectWriter);

            DefaultToolGroup.Tools.OfType<IEditorListTool>().Each(tool =>
            {
                if (!tool.Items.SequenceEqual(EditorDefaultOptions.Get(tool.Identifier)))
                {
                    objectWriter.AppendCollection(tool.Identifier, tool.Items);
                }
            });

            if (Encode.HasValue && !Encode.Value)
            {
                objectWriter.Append("encoded", Encode.Value);
            }

            if (StyleSheets.Items.Any())
            {
                bool isSecured = ViewContext.HttpContext.Request.IsSecureConnection;
                bool canCompress = ViewContext.HttpContext.Request.CanCompress();

                IWebAssetItemMerger assetItemMerger = ServiceLocator.Current.Resolve<IWebAssetItemMerger>();

                IList<string> mergedGroup = assetItemMerger.MergeGroup("text/css", WebAssetHttpHandler.DefaultPath, isSecured, canCompress, StyleSheets);

                objectWriter.AppendCollection("stylesheets", mergedGroup);
            }

            Localization.SerializeTo("localization", objectWriter);

            objectWriter.Complete();

            base.WriteInitializationScript(writer);
        }

        protected override void WriteHtml(HtmlTextWriter writer)
        {
            new EditorHtmlBuilder(this)
                .Build()
                .WriteTo(writer);

            base.WriteHtml(writer);
        }
    }
}
