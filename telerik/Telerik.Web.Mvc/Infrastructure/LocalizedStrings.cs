// (c) Copyright 2002-2010 Telerik 
// This source is subject to the GNU General Public License, version 2
// See http://www.gnu.org/licenses/gpl-2.0.html. 
// All other rights reserved.

namespace Telerik.Web.Mvc.Infrastructure
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Globalization;
    using Implementation;
    
    public abstract class LocalizedStrings
    {
        private readonly string resourceLocation;
        private readonly string resourceName;
        private readonly CultureInfo culture;

        private ILocalizationService localization;

        private ILocalizationService Localization
        {
            get 
            {
                return localization = localization ?? new LocalizationService(resourceLocation, resourceName, culture);
            }
        }

        protected LocalizedStrings(string resourceLocation, string resourceName, CultureInfo culture)
        {
            if (!string.IsNullOrEmpty(resourceLocation))
            {
                Guard.IsNotVirtualPath(resourceLocation, "resourceLocation");
            }

            Guard.IsNotNullOrEmpty(resourceName, "resourceName");

            this.resourceLocation = string.IsNullOrEmpty(resourceLocation) ? "~/App_GlobalResources" : resourceLocation;
            this.resourceName = resourceName;
            this.culture = culture ?? Culture.CurrentUI;
        }

        protected virtual string GetValue(string key)
        {
            return Localization.One(key);
        }

        public IDictionary<string, string> ToJson()
        {
            return Localization.All().ToDictionary(k => k.Key[0].ToString(CultureInfo.CurrentCulture).ToLower() + k.Key.Substring(1), k => k.Value);
        }

        public bool IsDefault
        {
            get
            {
                return Localization.IsDefault;
            }
        }
    }
}