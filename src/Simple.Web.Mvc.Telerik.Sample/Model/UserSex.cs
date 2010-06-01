using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;

namespace Simple.Web.Mvc.Telerik.Sample.Model
{
    public enum UserSex
    {
        [Description("Masculino")]
        Male = 1,
        [Description("Feminino")]
        Female
    }
}
