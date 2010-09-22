// (c) Copyright 2002-2010 Telerik 
// This source is subject to the GNU General Public License, version 2
// See http://www.gnu.org/licenses/gpl-2.0.html. 
// All other rights reserved.

namespace Telerik.Web.Mvc.UI
{
    using System;
    using System.Collections.Generic;
    using System.Web;

    public class GridBindingSettings : IClientSerializable
    {
        private readonly IGrid grid;

        public GridBindingSettings(IGrid grid)
        {
            this.grid = grid;
            Select = new GridRequestSettings();
            Insert = new GridRequestSettings();
            Update = new GridRequestSettings();
            Delete = new GridRequestSettings();
        }

        public bool Enabled
        {
            get;
            set;
        }

        public GridRequestSettings Select
        {
            get;
            private set;
        }

        public GridRequestSettings Insert
        {
            get;
            private set;
        }

        public GridRequestSettings Update
        {
            get;
            private set;
        }

        public GridRequestSettings Delete
        {
            get;
            private set;
        }
        
        public void SerializeTo(string key, IClientSideObjectWriter writer)
        {
            if (Enabled)
            {
                Func<string,string> encoder = (string url) => grid.IsSelfInitialized ? HttpUtility.UrlDecode(url) : url;

                var urlBuilder = new GridUrlBuilder(grid);
                
                var urls = new Dictionary<string, string>();

                urls["selectUrl"] = encoder(urlBuilder.Url(Select));

                if (Insert.HasValue())
                {
                    urls["insertUrl"] = encoder(urlBuilder.Url(Insert));
                }

                if (Update.HasValue())
                {
                    urls["updateUrl"] = encoder(urlBuilder.Url(Update));
                }

                if (Delete.HasValue())
                {
                    urls["deleteUrl"] = encoder(urlBuilder.Url(Delete));
                }
                
                writer.AppendObject(key, urls);
            }
        }
    }
}
