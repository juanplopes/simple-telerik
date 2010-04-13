// (c) Copyright 2002-2010 Telerik 
// This source is subject to the GNU General Public License, version 2
// See http://www.gnu.org/licenses/gpl-2.0.html. 
// All other rights reserved.

namespace Telerik.Web.Mvc.Configuration
{
    using System.Configuration;
    using System.Diagnostics;

    /// <summary>
    /// Web asset item configuration collection.
    /// </summary>
    [ConfigurationCollection(typeof(WebAssetItemConfigurationElement))]
    public class WebAssetItemConfigurationElementCollection : ConfigurationElementCollection
    {
        /// <summary>
        /// Gets the <see cref="Telerik.Web.Mvc.Configuration.WebAssetItemConfigurationElement"/> with the specified source.
        /// </summary>
        /// <value></value>
        public new WebAssetItemConfigurationElement this[string source]
        {
            [DebuggerStepThrough]
            get
            {
                return BaseGet(source) as WebAssetItemConfigurationElement;
            }
        }

        /// <summary>
        /// Adds the specified element.
        /// </summary>
        /// <param name="element">The element.</param>
        [DebuggerStepThrough]
        public void Add(WebAssetItemConfigurationElement element)
        {
            BaseAdd(element);
        }

        /// <summary>
        /// When overridden in a derived class, creates a new <see cref="T:System.Configuration.ConfigurationElement"/>.
        /// </summary>
        /// <returns>
        /// A new <see cref="T:System.Configuration.ConfigurationElement"/>.
        /// </returns>
        [DebuggerStepThrough]
        protected override ConfigurationElement CreateNewElement()
        {
            return new WebAssetItemConfigurationElement();
        }

        /// <summary>
        /// Gets the element key for a specified configuration element when overridden in a derived class.
        /// </summary>
        /// <param name="element">The <see cref="T:System.Configuration.ConfigurationElement"/> to return the key for.</param>
        /// <returns>
        /// An <see cref="T:System.Object"/> that acts as the key for the specified <see cref="T:System.Configuration.ConfigurationElement"/>.
        /// </returns>
        [DebuggerStepThrough]
        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((WebAssetItemConfigurationElement) element).Source;
        }
    }
}