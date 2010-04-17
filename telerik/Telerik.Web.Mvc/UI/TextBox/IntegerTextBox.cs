using Telerik.Web.Mvc.Infrastructure;
// (c) Copyright 2002-2010 Telerik 
// This source is subject to the GNU General Public License, version 2
// See http://www.gnu.org/licenses/gpl-2.0.html. 
// All other rights reserved.

namespace Telerik.Web.Mvc.UI
{
    using System;
    using System.Linq;
    using System.Web.Mvc;
    using System.Collections.Generic;
  
    using Extensions;
    using Infrastructure;
    using Telerik.Web.Mvc.Resources;

    public class IntegerTextBox : TextBoxBase<int>
    {
        public IntegerTextBox(ViewContext viewContext, IClientSideObjectWriterFactory clientSideObjectWriterFactory, ITextboxBaseHtmlBuilderFactory<int> rendererFactory)
		: base(viewContext, clientSideObjectWriterFactory, rendererFactory)
        {
            ScriptFileNames.AddRange(new[] { "telerik.common.js", "telerik.textbox.js" });

            MinValue = -100;
            MaxValue = 100;
            IncrementStep = 1;
            EmptyMessage = "Enter value";

            NumberGroupSize = Culture.Current.NumberFormat.NumberGroupSizes[0];
            NegativePatternIndex = Culture.Current.NumberFormat.NumberNegativePattern;
        }

        public override void WriteInitializationScript(System.IO.TextWriter writer)
        {
            IClientSideObjectWriter objectWriter = ClientSideObjectWriterFactory.Create(Id, "tTextBox", writer);

            objectWriter.Start();

            objectWriter.Append("val", this.Value);
            objectWriter.Append("step", this.IncrementStep);
            objectWriter.Append("minValue", this.MinValue);
            objectWriter.Append("maxValue", this.MaxValue);
            objectWriter.Append("digits", 0);
            objectWriter.AppendNullableString("groupSeparator", this.NumberGroupSeparator);
            objectWriter.Append("groupSize", this.NumberGroupSize);
            objectWriter.Append("negative", this.NegativePatternIndex);
            objectWriter.Append("text", this.EmptyMessage);
            objectWriter.Append("type", "numeric");
            
            objectWriter.Append("onLoad", ClientEvents.OnLoad);
            objectWriter.Append("onChange", ClientEvents.OnChange);

            objectWriter.Complete();

            base.WriteInitializationScript(writer);
        }

        protected override void WriteHtml(System.Web.UI.HtmlTextWriter writer)
        {
            Guard.IsNotNull(writer, "writer");

            VerifySettings();

            ITextBoxBaseHtmlBuilder renderer = rendererFactory.Create(this);

            IHtmlNode rootTag = renderer.Build("t-numerictextbox");

            rootTag.Children.Add(renderer.InputTag());

            if (Spinners)
            {
                rootTag.Children.Add(renderer.UpButtonTag());
                rootTag.Children.Add(renderer.DownButtonTag());
            }

            rootTag.WriteTo(writer);
        }

        private void VerifySettings()
        {
            if (MinValue > MaxValue)
            {
                throw new ArgumentException(TextResource.MinValueShouldBeLessThanMaxValue);
            }

            if ((Value != null) && (MinValue > Value || Value > MaxValue))
            {
                throw new ArgumentOutOfRangeException(TextResource.ValueOutOfRange);
            }

            if (NegativePatternIndex < 0 || NegativePatternIndex > 4) 
            {
                throw new IndexOutOfRangeException(TextResource.PropertyShouldBeInRange.FormatWith("NegativePatternIndex", 0, 4));
            }
        }
    }
}
