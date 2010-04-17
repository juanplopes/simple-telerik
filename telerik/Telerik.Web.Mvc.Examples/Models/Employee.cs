namespace Telerik.Web.Mvc.Examples.Models
{
    using System;
    using System.ComponentModel;

    [TypeConverter(typeof(EmployeeConverter))]
    public partial class Employee
    {
    }

    public class EmployeeConverter : TypeConverter
    {
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            return (sourceType == typeof(string)) ? true : base.CanConvertFrom(context, sourceType);
        }
    }
}