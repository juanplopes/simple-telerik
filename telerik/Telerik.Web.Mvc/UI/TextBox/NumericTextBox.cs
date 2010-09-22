// (c) Copyright 2002-2010 Telerik 
// This source is subject to the GNU General Public License, version 2
// See http://www.gnu.org/licenses/gpl-2.0.html. 
// All other rights reserved.

namespace Telerik.Web.Mvc.UI
{
    using System;
    using System.Collections.Generic;
    using System.Web.Mvc;
    using Extensions;
    using Infrastructure;
    using Telerik.Web.Mvc.Resources;

    public class NumericTextBox<T> : TextBoxBase<T> where T: struct
    {
        public NumericTextBox(ViewContext viewContext, IClientSideObjectWriterFactory clientSideObjectWriterFactory, ITextboxBaseHtmlBuilderFactory<T> rendererFactory)
		: base(viewContext, clientSideObjectWriterFactory, rendererFactory)
        {
            ScriptFileNames.AddRange(new[] { "telerik.common.js", "telerik.textbox.js" });

            MinValue = (T)Convert.ChangeType(-100, typeof(T));
            MaxValue = (T)Convert.ChangeType(100, typeof(T));
            IncrementStep = (T)Convert.ChangeType(1, typeof(T)); ;
            EmptyMessage = "Enter value";

            DecimalDigits = Culture.Current.NumberFormat.NumberDecimalDigits;
            NumberGroupSize = Culture.Current.NumberFormat.NumberGroupSizes[0];
            NegativePatternIndex = Culture.Current.NumberFormat.NumberNegativePattern;
        }

        public int DecimalDigits
        {
            get;
            set;
        }

        public string DecimalSeparator
        {
            get;
            set;
        }


        public override void WriteInitializationScript(System.IO.TextWriter writer)
        {
            IClientSideObjectWriter objectWriter = ClientSideObjectWriterFactory.Create(Id, "tTextBox", writer);

            objectWriter.Start();

            objectWriter.AppendObject("val", this.Value);
            objectWriter.AppendObject("step", this.IncrementStep);
            objectWriter.AppendObject("minValue", this.MinValue);
            objectWriter.AppendObject("maxValue", this.MaxValue);
            objectWriter.Append("digits", this.DecimalDigits);
            objectWriter.Append("separator", this.DecimalSeparator);
            objectWriter.AppendNullableString("groupSeparator", this.NumberGroupSeparator);
            objectWriter.Append("groupSize", this.NumberGroupSize);
            objectWriter.Append("negative", this.NegativePatternIndex);
            objectWriter.Append("text", this.EmptyMessage);
            objectWriter.Append("type", "numeric");

            ClientEvents.SerializeTo(objectWriter);

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
            base.WriteHtml(writer);
        }

        private void VerifySettings()
        {
            if (Nullable.Compare<T>(MinValue, MaxValue) == 1)
            {
                throw new ArgumentException(TextResource.MinValueShouldBeLessThanMaxValue);
            }

            if ((Value != null) && (Nullable.Compare<T>(MinValue, MaxValue) == 1 || Nullable.Compare<T>(MinValue, MaxValue) == 1))
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
