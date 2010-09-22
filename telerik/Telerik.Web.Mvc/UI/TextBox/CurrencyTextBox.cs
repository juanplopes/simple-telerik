// (c) Copyright 2002-2010 Telerik 
// This source is subject to the GNU General Public License, version 2
// See http://www.gnu.org/licenses/gpl-2.0.html. 
// All other rights reserved.

namespace Telerik.Web.Mvc.UI
{
    using System;
    using System.Web.Mvc;

    using Extensions;
    using Infrastructure;
    using Telerik.Web.Mvc.Resources;

    public class CurrencyTextBox : TextBoxBase<decimal>
    {
        public CurrencyTextBox(ViewContext viewContext, IClientSideObjectWriterFactory clientSideObjectWriterFactory, ITextboxBaseHtmlBuilderFactory<decimal> rendererFactory)
		: base(viewContext, clientSideObjectWriterFactory, rendererFactory)
        {
            ScriptFileNames.AddRange(new[] { "telerik.common.js", "telerik.textbox.js" });

            MinValue = 0;
            MaxValue = 1000;
            IncrementStep = 1;
            EmptyMessage = "Enter value";

            DecimalDigits = Culture.Current.NumberFormat.CurrencyDecimalDigits;
            NumberGroupSize = Culture.Current.NumberFormat.CurrencyGroupSizes[0];
            NegativePatternIndex = Culture.Current.NumberFormat.CurrencyNegativePattern;
            PositivePatternIndex = Culture.Current.NumberFormat.CurrencyPositivePattern;
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

            objectWriter.Append("val", this.Value);
            objectWriter.Append("step", this.IncrementStep);
            objectWriter.Append("minValue", this.MinValue);
            objectWriter.Append("maxValue", this.MaxValue);
            objectWriter.Append("symbol", this.CurrencySymbol);
            objectWriter.Append("digits", this.DecimalDigits);
            objectWriter.Append("separator", this.DecimalSeparator);
            objectWriter.AppendNullableString("groupSeparator", this.NumberGroupSeparator);
            objectWriter.Append("groupSize", this.NumberGroupSize);
            objectWriter.Append("positive", this.PositivePatternIndex);
            objectWriter.Append("negative", this.NegativePatternIndex);
            objectWriter.Append("text", this.EmptyMessage);
            objectWriter.Append("type", "currency");

            ClientEvents.SerializeTo(objectWriter);

            objectWriter.Complete();

            base.WriteInitializationScript(writer);
        }

        public int PositivePatternIndex { get; set; }

        public string CurrencySymbol { get; set; }


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
            if (MinValue > MaxValue)
            {
                throw new ArgumentException(TextResource.MinValueShouldBeLessThanMaxValue);
            }

            if ((Value != null) && (MinValue > Value || Value > MaxValue))
            {
                throw new ArgumentOutOfRangeException(TextResource.ValueOutOfRange);
            }

            if (PositivePatternIndex < 0 || PositivePatternIndex > 3) //currency positive patterns are 4.
            {
                throw new IndexOutOfRangeException(TextResource.PropertyShouldBeInRange.FormatWith("PositivePatternIndex", 0, 4));
            }

            if (NegativePatternIndex < 0 || NegativePatternIndex > 15) //currency negative patterns are 16.
            {
                throw new IndexOutOfRangeException(TextResource.PropertyShouldBeInRange.FormatWith("NegativePatternIndex", 0, 15));
            }
        }
    }
}
