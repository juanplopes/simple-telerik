namespace Telerik.Web.Mvc.UI
{
    using Extensions;

    using System.Web.Mvc;
    using System.Linq;
    using System.Collections.Generic;
    using System.Web.Routing;

    public class DropDownList : ViewComponentBase, IDropDown, IDropDownRenderable
    {
        private readonly IList<IEffect> defaultEffects = new List<IEffect> { new SlideAnimation() };

        public DropDownList(ViewContext viewContext, IClientSideObjectWriterFactory clientSideObjectWriterFactory, IUrlGenerator urlGenerator)
            : base(viewContext, clientSideObjectWriterFactory)
        {
            ScriptFileNames.AddRange(new[] { "telerik.common.js", "telerik.list.js" });

            UrlGenerator = urlGenerator;

            ClientEvents = new DropDownClientEvents();
            DataBinding = new DropDownListDataBindingConfiguration();
            DropDownHtmlAttributes = new RouteValueDictionary();
            
            Effects = new Effects();
            defaultEffects.Each(el => Effects.Container.Add(el));

            Items = new List<DropDownItem>();
            SelectedIndex = 0;
        }

        public IUrlGenerator UrlGenerator 
        {
            get; 
            set; 
        }

        public DropDownClientEvents ClientEvents
        {
            get;
            private set;
        }

        public IDropDownDataBindingConfiguration DataBinding 
        { 
            get; 
            private set; 
        }

        public IDictionary<string, object> DropDownHtmlAttributes
        {
            get;
            private set;
        }

        public Effects Effects
        {
            get;
            set;
        }

        /// <summary>
        /// Gets the items of the treeview.
        /// </summary>
        public IList<DropDownItem> Items
        {
            get;
            private set;
        }

        public int SelectedIndex
        {
            get;
            set;
        }

        public override void WriteInitializationScript(System.IO.TextWriter writer)
        {
            IClientSideObjectWriter objectWriter = ClientSideObjectWriterFactory.Create(Id, "tDropDownList", writer);

            objectWriter.Start();

            if (!defaultEffects.SequenceEqual(Effects.Container))
            {
                objectWriter.Serialize("effects", Effects);
            }

            ClientEvents.SerializeTo(objectWriter);

            DataBinding.Ajax.SerializeTo("ajax", objectWriter, this);
            DataBinding.WebService.SerializeTo("ws", objectWriter, this);

            if(Items.Any())
            {
                objectWriter.AppendCollection("data", Items);
            }

            objectWriter.Append("index", SelectedIndex, 0);

            if (DropDownHtmlAttributes.Any()) 
            {
                objectWriter.Append("dropDownAttr", DropDownHtmlAttributes.ToAttributeString());
            }

            objectWriter.Complete();

            base.WriteInitializationScript(writer);
        }

        protected override void WriteHtml(System.Web.UI.HtmlTextWriter writer)
        {
            if (Items.Any())
            {
                this.PrepareItemsAndDefineSelectedIndex();
            }

            IDropDownHtmlBuilder builder = new DropDownListHtmlBuilder(this);

            IHtmlNode rootTag = builder.Build();

            builder.InnerContentTag().AppendTo(rootTag);

            builder.HiddenInputTag().AppendTo(rootTag);

            rootTag.WriteTo(writer);

            base.WriteHtml(writer);
        }
    }
}
