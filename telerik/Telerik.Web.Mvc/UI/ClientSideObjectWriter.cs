// (c) Copyright 2002-2010 Telerik 
// This source is subject to the GNU General Public License, version 2
// See http://www.gnu.org/licenses/gpl-2.0.html. 
// All other rights reserved.

namespace Telerik.Web.Mvc.UI
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Web.Script.Serialization;

    using Extensions;
    using Infrastructure;

    /// <summary>
    /// Class used to build initialization script of jQuery plugin.
    /// </summary>
    public class ClientSideObjectWriter : IClientSideObjectWriter
    {
        private readonly string id;
        private readonly string type;
        private readonly TextWriter writer;

        private bool hasStarted;
        private bool appended;

        /// <summary>
        /// Initializes a new instance of the <see cref="ClientSideObjectWriter"/> class.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <param name="type">The type.</param>
        /// <param name="textWriter">The text writer.</param>
        public ClientSideObjectWriter(string id, string type, TextWriter textWriter)
        {
            Guard.IsNotNullOrEmpty(id, "id");
            Guard.IsNotNullOrEmpty(type, "type");
            Guard.IsNotNull(textWriter, "textWriter");

            this.id = id;
            this.type = type;
            writer = textWriter;
        }

        /// <summary>
        /// Starts writing this instance.
        /// </summary>
        /// <returns></returns>
        public IClientSideObjectWriter Start()
        {
            if (hasStarted)
            {
                throw new InvalidOperationException(Resources.TextResource.YouCannotCallStartMoreThanOnce);
            }

            writer.Write("jQuery('#{0}').{1}(".FormatWith(id, type));
            hasStarted = true;

            return this;
        }

        /// <summary>
        /// Appends the specified key value pair to the end of this instance.
        /// </summary>
        /// <param name="keyValuePair">The key value pair.</param>
        /// <returns></returns>
        public IClientSideObjectWriter Append(string keyValuePair)
        {
            EnsureStart();

            if (!string.IsNullOrEmpty(keyValuePair))
            {
                writer.Write(appended ? ", " : "{");
                writer.Write(keyValuePair);

                if (!appended)
                {
                    appended = true;
                }
            }

            return this;
        }

        /// <summary>
        /// Appends the specified name and value to the end of this instance.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public IClientSideObjectWriter Append(string name, string value)
        {
            if (!string.IsNullOrEmpty(name) && !string.IsNullOrEmpty(value))
            {
                string formattedValue = QuoteString(value);

                Append("{0}:'{1}'".FormatWith(name, formattedValue));
            }

            return this;
        }

        /// <summary>
        /// Appends the specified name and nullable value to the end of this instance.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public IClientSideObjectWriter AppendNullableString(string name, string value)
        {

            if (!string.IsNullOrEmpty(name) && value != null)
            {
                string formattedValue = QuoteString(value);

                Append("{0}:'{1}'".FormatWith(name, formattedValue));
            }

            return this;
        }

        /// <summary>
        /// Appends the specified name and value to the end of this instance.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public IClientSideObjectWriter Append(string name, int value)
        {
            if (!string.IsNullOrEmpty(name))
            {
                Append("{0}:{1}".FormatWith(name, value));
            }

            return this;
        }

        /// <summary>
        /// Appends the specified name and value to the end of this instance.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="value">The value.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns></returns>
        public IClientSideObjectWriter Append(string name, int value, int defaultValue)
        {
            if (value != defaultValue)
            {
                Append(name, value);
            }

            return this;
        }

        /// <summary>
        /// Appends the specified name and value to the end of this instance.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public IClientSideObjectWriter Append(string name, int? value)
        {
            if (value.HasValue)
            {
                Append(name, value.Value);
            }

            return this;
        }

        /// <summary>
        /// Appends the specified name and value to the end of this instance.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public IClientSideObjectWriter Append(string name, double value) 
        {
            if (!string.IsNullOrEmpty(name))
            {
                Append("{0}:'{1}'".FormatWith(name, value));
            }

            return this;
        }

        /// <summary>
        /// Appends the specified name and value to the end of this instance.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public IClientSideObjectWriter Append(string name, double? value) 
        {
            if (value.HasValue)
            {
                Append(name, value.Value);
            }

            return this;
        }

        /// <summary>
        /// Appends the specified name and value to the end of this instance.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public IClientSideObjectWriter Append(string name, decimal value) 
        {
            if (!string.IsNullOrEmpty(name))
            {
                Append("{0}:'{1}'".FormatWith(name, value));
            }

            return this;
        }

        /// <summary>
        /// Appends the specified name and value to the end of this instance.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public IClientSideObjectWriter Append(string name, decimal? value) 
        {
            if (value.HasValue)
            {
                Append(name, value.Value);
            }

            return this;
        }

        /// <summary>
        /// Appends the specified name and value to the end of this instance.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="value">if set to <c>true</c> [value].</param>
        /// <returns></returns>
        public IClientSideObjectWriter Append(string name, bool value)
        {
            if (!string.IsNullOrEmpty(name))
            {
                Append("{0}:{1}".FormatWith(name, value.ToString(Culture.Invariant).ToLower(Culture.Invariant)));
            }

            return this;
        }

        /// <summary>
        /// Appends the specified name and value to the end of this instance.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="value">if set to <c>true</c> [value].</param>
        /// <param name="defaultValue">if set to <c>true</c> [default value].</param>
        /// <returns></returns>
        public IClientSideObjectWriter Append(string name, bool value, bool defaultValue)
        {
            if (value != defaultValue)
            {
                Append(name, value);
            }

            return this;
        }

        /// <summary>
        /// Appends the specified name and only the date of the passed <seealso cref="DateTime"/>.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public IClientSideObjectWriter AppendDateOnly(string name, DateTime value)
        {
            if (!string.IsNullOrEmpty(name) && (value != DateTime.MinValue))
            {
                string dateValue = "new $.telerik.datetime({0},{1},{2})".FormatWith(value.Year.ToString("0000", Culture.Invariant), (value.Month - 1).ToString("00", Culture.Invariant), value.Day.ToString("00", Culture.Invariant));

                Append("{0}:{1}".FormatWith(name, dateValue));
            }

            return this;
        }

        /// <summary>
        /// Appends the specified name and only the date of the passed <seealso cref="DateTime"/>.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public IClientSideObjectWriter AppendDateOnly(string name, DateTime? value)
        {
            if (!string.IsNullOrEmpty(name) && (value != null && value != DateTime.MinValue))
            {
                string dateValue = "new $.telerik.datetime({0},{1},{2})".FormatWith(value.Value.Year.ToString("0000", Culture.Invariant), (value.Value.Month - 1).ToString("00", Culture.Invariant), value.Value.Day.ToString("00", Culture.Invariant));

                Append("{0}:{1}".FormatWith(name, dateValue));
            }

            return this;
        }

        /// <summary>
        /// Appends the specified name and only the date of the passed <seealso cref="IEnumerable<DateTime>"/>.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public IClientSideObjectWriter AppendDatesOnly(string name, IEnumerable<DateTime> collection)
        {
            if (collection.Count() > 0)
            {
                List<DateTime> dates = collection.ToList();
                dates.Sort();

                StringBuilder builder = new StringBuilder();

                int year = -1;
                int month = -1;
                bool yearAppended = false;
                bool monthAppended = false;
                
                foreach (DateTime date in dates) 
                {
                    if (year != date.Year)
                    {
                        if (yearAppended)
                        {
                            if (monthAppended) 
                            {
                                builder.Append("]");
                            }
                            builder.Append("}");
                            builder.Append(",");
                            yearAppended = false;
                        }
                        builder.Append("'");
                        builder.Append(date.Year);
                        builder.Append("':{");

                        monthAppended = false;
                    }
                    if (month != date.Month)
                    {
                        if (monthAppended)
                        {
                            builder.Append("]");
                            builder.Append(",");
                            monthAppended = false;
                        }
                        builder.Append("'");
                        builder.Append(date.Month - 1);
                        builder.Append("':[");
                    }

                    if (year == date.Year && month == date.Month)
                    {
                        builder.Append(",");
                    }
                    builder.Append(date.Day);

                    if (month != date.Month)
                    {
                        month = date.Month;
                        monthAppended = true;
                    }

                    if (year != date.Year)
                    {
                        year = date.Year;
                        yearAppended = true;
                    }
                }
                builder.Append("]}");
                Append("{0}:{{{1}}}".FormatWith(name, builder.ToString()));
            }

            return this;
        }

        /// <summary>
        /// Appends the specified name and value to the end of this instance.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public IClientSideObjectWriter Append(string name, DateTime value)
        {
            if (!string.IsNullOrEmpty(name) && (value != DateTime.MinValue))
            {
                string dateValue = "new $.telerik.datetime({0},{1},{2},{3},{4},{5},{6})".FormatWith(value.Year.ToString("0000", Culture.Invariant), (value.Month - 1).ToString("00", Culture.Invariant), value.Day.ToString("00", Culture.Invariant), value.Hour.ToString("00", Culture.Invariant), value.Minute.ToString("00", Culture.Invariant), value.Second.ToString("00", Culture.Invariant), value.Millisecond.ToString("000", Culture.Invariant));

                Append("{0}:{1}".FormatWith(name, dateValue));
            }

            return this;
        }

        /// <summary>
        /// Appends the specified name and value to the end of this instance.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public IClientSideObjectWriter Append(string name, DateTime? value)
        {
            if (value.HasValue)
            {
                Append(name, value.Value);
            }

            return this;
        }

        /// <summary>
        /// Appends the specified name and value to the end of this instance.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="action">The action.</param>
        /// <returns></returns>
        public IClientSideObjectWriter Append(string name, Action action)
        {
            if (!string.IsNullOrEmpty(name) && (action != null))
            {
                Append("{0}:".FormatWith(name));
                action();
            }

            return this;
        }

        /// <summary>
        /// Appends the specified name and value to the end of this instance.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="values">The values.</param>
        /// <returns></returns>
        public IClientSideObjectWriter Append(string name, IList<string> values)
        {
            if (!string.IsNullOrEmpty(name) && !values.IsNullOrEmpty())
            {
                List<string> stringValues = new List<string>(values.Count);

                foreach (string value in values)
                {
                    stringValues.Add("'{0}'".FormatWith(QuoteString(value)));
                }

                Append("{0}:[{1}]".FormatWith(name, string.Join(",", stringValues.ToArray())));
            }

            return this;
        }

        /// <summary>
        /// Appends the specified name and value to the end of this instance.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="values">The values.</param>
        /// <returns></returns>
        public IClientSideObjectWriter Append(string name, IList<int> values)
        {
            if (!string.IsNullOrEmpty(name) && !values.IsNullOrEmpty())
            {
                List<string> stringValues = new List<string>();

                foreach (int value in values)
                {
                    stringValues.Add(value.ToString(Culture.Invariant));
                }

                Append("{0}:[{1}]".FormatWith(name, string.Join(",", stringValues.ToArray())));
            }

            return this;
        }

        /// <summary>
        /// Appends the specified name and value to the end of this instance.
        /// </summary>
        /// <typeparam name="TEnum">The type of the enum.</typeparam>
        /// <param name="name">The name.</param>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public IClientSideObjectWriter Append<TEnum>(string name, TEnum value) where TEnum : IComparable, IFormattable
        {
            if (!string.IsNullOrEmpty(name))
            {
                ClientSideEnumValueAttribute valueAttribute = value.GetType().GetField(value.ToString())
                                                                             .GetCustomAttributes(true)
                                                                             .OfType<ClientSideEnumValueAttribute>()
                                                                             .FirstOrDefault();

                if (valueAttribute != null)
                {
                    Append("{0}:{1}".FormatWith(name, valueAttribute.Value));
                }
            }

            return this;
        }

        /// <summary>
        /// Appends the specified name and value to the end of this instance.
        /// </summary>
        /// <typeparam name="TEnum">The type of the enum.</typeparam>
        /// <param name="name">The name.</param>
        /// <param name="value">The value.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns></returns>
        public IClientSideObjectWriter Append<TEnum>(string name, TEnum value, TEnum defaultValue) where TEnum : IComparable, IFormattable
        {
            if (!value.Equals(defaultValue))
            {
                Append(name, value);
            }

            return this;
        }

        public IClientSideObjectWriter AppendCollection<T>(string name, IEnumerable<T> value)
        {
            return Append("{0}:{1}".FormatWith(name, new JavaScriptSerializer().Serialize(value)));
        }

        public IClientSideObjectWriter AppendObject(string name, object value)
        {
            return Append("{0}:{1}".FormatWith(name, new JavaScriptSerializer().Serialize(value)));
        }

        /// <summary>
        /// Completes this instance.
        /// </summary>
        public void Complete()
        {
            EnsureStart();

            if (appended)
            {
                writer.Write("}");
            }

            writer.Write(");");

            hasStarted = false;
            appended = false;
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity", Justification = "Needs refactoring")]
        private static string QuoteString(string value)
        {
            StringBuilder result = new StringBuilder();

            if (!string.IsNullOrEmpty(value))
            {
                int startIndex = 0;
                int count = 0;

                for (int i = 0; i < value.Length; i++)
                {
                    char c = value[i];

                    if (c == '\r' || c == '\t' || c == '\"' || c == '\'' || c == '<' || c == '>' ||
                        c == '\\' || c == '\n' || c == '\b' || c == '\f' || c < ' ')
                    {
                        if (count > 0)
                        {
                            result.Append(value, startIndex, count);
                        }

                        startIndex = i + 1;
                        count = 0;
                    }

                    switch (c)
                    {
                        case '\r':
                            result.Append("\\r");
                            break;
                        case '\t':
                            result.Append("\\t");
                            break;
                        case '\"':
                            result.Append("\\\"");
                            break;
                        case '\\':
                            result.Append("\\\\");
                            break;
                        case '\n':
                            result.Append("\\n");
                            break;
                        case '\b':
                            result.Append("\\b");
                            break;
                        case '\f':
                            result.Append("\\f");
                            break;
                        case '\'':
                        case '>':
                        case '<':
                            AppendAsUnicode(result, c);
                            break;
                        default:
                            if (c < ' ')
                            {
                                AppendAsUnicode(result, c);
                            }
                            else
                            {
                                count++;
                            }

                            break;
                    }
                }

                if (result.Length == 0)
                {
                    result.Append(value);
                }
                else if (count > 0)
                {
                    result.Append(value, startIndex, count);
                }
            }

            return result.ToString();
        }

        private static void AppendAsUnicode(StringBuilder builder, char c)
        {
            builder.Append("\\u");
            builder.AppendFormat(Culture.Invariant, "{0:x4}", (int) c);
        }

        private void EnsureStart()
        {
            if (!hasStarted)
            {
                throw new InvalidOperationException(Resources.TextResource.YouMustHaveToCallStartPriorCallingThisMethod);
            }
        }
    }
}