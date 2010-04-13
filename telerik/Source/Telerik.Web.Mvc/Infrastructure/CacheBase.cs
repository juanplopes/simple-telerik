// (c) Copyright 2002-2010 Telerik 
// This source is subject to the GNU General Public License, version 2
// See http://www.gnu.org/licenses/gpl-2.0.html. 
// All other rights reserved.

namespace Telerik.Web.Mvc.Infrastructure
{
    using System;
    using System.Collections.Generic;
    using System.Threading;

    using Extensions;

    public abstract class CacheBase<TKey, TValue> : DisposableBase
    {
        private readonly IDictionary<TKey, TValue> cache;
        private readonly ReaderWriterLockSlim syncLock = new ReaderWriterLockSlim();

        protected CacheBase(IEqualityComparer<TKey> comparer)
        {
            cache = new Dictionary<TKey, TValue>(comparer);
        }

        protected CacheBase() : this(null)
        {
        }

        protected override void DisposeCore()
        {
            cache.Clear();
            syncLock.Dispose();

            base.DisposeCore();
        }

        protected TValue GetOrCreate(TKey key, Func<TValue> factory)
        {
            Guard.IsNotNull(key, "key");
            Guard.IsNotNull(factory, "factory");

            TValue value;

            using (syncLock.ReadAndWrite())
            {
                if (!cache.TryGetValue(key, out value))
                {
                    using (syncLock.Write())
                    {
                        if (!cache.TryGetValue(key, out value))
                        {
                            value = factory();
                            cache.Add(key, value);
                        }
                    }
                }
            }

            return value;
        }
    }
}