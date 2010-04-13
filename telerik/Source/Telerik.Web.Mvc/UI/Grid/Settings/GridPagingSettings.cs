// (c) Copyright 2002-2010 Telerik 
// This source is subject to the GNU General Public License, version 2
// See http://www.gnu.org/licenses/gpl-2.0.html. 
// All other rights reserved.

namespace Telerik.Web.Mvc.UI
{
    using System.Diagnostics;

    using Infrastructure;

    public class GridPagingSettings
    {
        private int pageSize = 10;
        private int total = 0;

        public GridPagingSettings()
        {
            Style = GridPagerStyles.NextPreviousAndNumeric;
        }

        public bool Enabled
        {
            get;
            set;
        }

        public int PageSize 
        {
            [DebuggerStepThrough]
            get
            {
                return pageSize;
            }

            [DebuggerStepThrough]
            set
            {
                Guard.IsNotZeroOrNegative(value, "value");
                pageSize = value;
            }
        }

        public GridPagerStyles Style
        {
            get;
            set;
        }

        public GridPagerPosition Position 
        { 
            get; 
            set; 
        }

        public int Total
        {
            [DebuggerStepThrough]
            get
            {
                return total;
            }
            [DebuggerStepThrough]
            set
            {
                Guard.IsNotNegative(value, "value");

                total = value;
            }
        }
    }
}