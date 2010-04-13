// (c) Copyright 2002-2010 Telerik 
// This source is subject to the GNU General Public License, version 2
// See http://www.gnu.org/licenses/gpl-2.0.html. 
// All other rights reserved.

namespace Telerik.Web.Mvc.Infrastructure.Implementation
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Resources;
    using System.Threading;
    using System.Xml;

    using Extensions;

    public class LocalizationService : ILocalizationService
    {
        private static readonly IDictionary<string, ResourceBase> cache = new Dictionary<string, ResourceBase>(StringComparer.OrdinalIgnoreCase);
        private static readonly ReaderWriterLockSlim syncLock = new ReaderWriterLockSlim();

        private readonly ResourceBase resource;

        public LocalizationService(string resourceLocation, string resourceName, CultureInfo culture)
        {
            Guard.IsNotNullOrEmpty(resourceLocation, "resourceLocation");
            Guard.IsNotNullOrEmpty(resourceName, "resourceName");
            Guard.IsNotNull(culture, "culture");

            resource = DetectResource(resourceLocation, resourceName, culture);
        }

        public string One(string key)
        {
            return resource.GetByKey(key);
        }

        public bool IsDefault
        {
            get
            {
                return resource is EmbeddedResource;
            }
        }

        public IDictionary<string, string> All()
        {
            return resource.GetAll();
        }

        private static ResourceBase DetectResource(string resourceLocation, string resourceName, CultureInfo culture)
        {
            string cacheKey = resourceName + ":" + culture;

            ResourceBase resource;

            using (syncLock.ReadAndWrite())
            {
                if (!cache.TryGetValue(cacheKey, out resource))
                {
                    using (syncLock.Write())
                    {
                        if (!cache.TryGetValue(cacheKey, out resource))
                        {
                            resource = CreateResource(resourceName, culture, resourceLocation);

                            cache.Add(cacheKey, resource);
                        }
                    }
                }
            }

            return resource;
        }

        private static ResourceBase CreateResource(string resourceName, CultureInfo culture, string resourceLocation)
        {
            Func<string, string> fixResourceName = c => resourceName + ((c != null) ? "." + c : string.Empty) + ".resx";

            IVirtualPathProvider vpp = ServiceLocator.Current.Resolve<IVirtualPathProvider>();

            // First try the file path Resource.fr-CA.resx
            string fullResourcePath = vpp.CombinePaths(resourceLocation, fixResourceName(culture.ToString()));
            bool exists = vpp.FileExists(fullResourcePath);

            // If not found, try Resource.fr.resx
            if (!exists)
            {
                fullResourcePath = vpp.CombinePaths(resourceLocation, fixResourceName(culture.TwoLetterISOLanguageName));
                exists = vpp.FileExists(fullResourcePath);
            }

            // If nothing is found try Resource.resx
            if (!exists)
            {
                fullResourcePath = vpp.CombinePaths(resourceLocation, fixResourceName(null));
                exists = vpp.FileExists(fullResourcePath);
            }

            ResourceBase resource = exists ?
                                    new ResXResource(ServiceLocator.Current.Resolve<IPathResolver>().Resolve(fullResourcePath)) :
                                    new EmbeddedResource(resourceName, culture) as ResourceBase;

            return resource;
        }

        private abstract class ResourceBase
        {
            private bool isLoaded;

            protected ResourceBase()
            {
                CurrentResources = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
            }

            protected IDictionary<string, string> CurrentResources { get; private set; }

            public string GetByKey(string key)
            {
                LoadResources();

                return CurrentResources[key];
            }

            public IDictionary<string, string> GetAll()
            {
                LoadResources();

                return CurrentResources;
            }

            protected abstract void Load();

            private void LoadResources()
            {
                if (isLoaded)
                {
                    return;
                }

                Load();

                isLoaded = true;
            }
        }

        private sealed class EmbeddedResource : ResourceBase
        {
            private readonly string resourceName;
            private readonly CultureInfo culture;

            public EmbeddedResource(string resourceName, CultureInfo culture)
            {
                this.resourceName = resourceName;
                this.culture = culture;
            }

            protected override void Load()
            {
                ResourceManager rm = new ResourceManager("Telerik.Web.Mvc.Resources." + resourceName, GetType().Assembly);

                using (ResourceSet set = rm.GetResourceSet(culture ?? Culture.Current, true, true))
                {
                    var iterator = set.GetEnumerator();

                    while (iterator.MoveNext())
                    {
                        CurrentResources.Add(iterator.Key.ToString(), iterator.Value.ToString());
                    }
                }
            }
        }

        private sealed class ResXResource : ResourceBase
        {
            private static readonly XmlReaderSettings readerSettings = new XmlReaderSettings
                                                                           {
                                                                               IgnoreComments = true,
                                                                               IgnoreWhitespace = true,
                                                                               IgnoreProcessingInstructions = true,
                                                                               CloseInput = true
                                                                           };
            private readonly string resourceLocation;

            public ResXResource(string resourceLocation)
            {
                this.resourceLocation = resourceLocation;
            }

            protected override void Load()
            {
                using (XmlReader reader = XmlReader.Create(resourceLocation, readerSettings))
                {
                    while (reader.Read())
                    {
                        if (reader.LocalName.Equals("data", StringComparison.OrdinalIgnoreCase) && reader.HasAttributes)
                        {
                            string name = reader.GetAttribute("name");

                            if (!string.IsNullOrEmpty(name) && reader.ReadToDescendant("value"))
                            {
                                CurrentResources.Add(name, reader.ReadElementContentAsString());
                            }
                        }
                    }
                }
            }
        }
    }
}