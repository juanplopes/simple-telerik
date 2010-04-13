// (c) Copyright 2002-2010 Telerik 
// This source is subject to the GNU General Public License, version 2
// See http://www.gnu.org/licenses/gpl-2.0.html. 
// All other rights reserved.

namespace Telerik.Web.Mvc.UI
{
    using System;

    public class GridClientEvents
    {
        public Action OnError
        {
            get;
            set;
        }

        public Action OnDataBinding
        {
            get;
            set;
        }

        public Action OnDataBound
        {
            get;
            set;
        }

        public Action OnRowDataBound
        {
            get;
            set;
        }

        public Action OnLoad
        {
            get;
            set;
        }

        public Action OnRowSelected
        {
            get;
            set;
        }
    }
}