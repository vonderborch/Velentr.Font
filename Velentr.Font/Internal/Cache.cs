using System;
using System.Collections.Generic;

namespace Velentr.Font.Internal
{
    internal class Cache<K, V>
    {
        /// <summary>
        /// The cached objects
        /// </summary>
        private Dictionary<K, V> objects;

        /// <summary>
        /// The queue
        /// </summary>
        private Queue<K> queue;

        /// <summary>
        /// The maximum cache size
        /// </summary>
        private int maxCacheSize;

        /// <summary>
        /// Gets or sets the maximum size of the cache.
        /// </summary>
        /// <value>
        /// The maximum size of the cache.
        /// </value>
        /// <exception cref="System.ArgumentOutOfRangeException">Max Cache Size must be 0 or greater!</exception>
        public int MaxCacheSize
        {
            get => maxCacheSize;
            set
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException("Max Cache Size must be 0 or greater!");
                }

                ResizeCache(value);
                maxCacheSize = value;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Cache{K, V}"/> class.
        /// </summary>
        /// <param name="maxCacheSize">Maximum size of the cache.</param>
        public Cache(int maxCacheSize)
        {
            objects = new Dictionary<K, V>(maxCacheSize);
            queue = new Queue<K>(maxCacheSize);
            this.maxCacheSize = maxCacheSize;
        }

        /// <summary>
        /// Gets the cache item if it exists.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="value">The value to return.</param>
        /// <returns>Whether we were able to retrieve the value or not</returns>
        public bool TryGetItem(K key, out V value)
        {
            if (objects.TryGetValue(key, out value))
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Adds the item to cache.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        /// <param name="overrideWhenKeyExists">if set to <c>true</c> [override when key exists].</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentException">Key already exists in cache!</exception>
        public bool AddItemToCache(K key, V value, bool overrideWhenKeyExists = false)
        {
            // Validate we don't already have this object in the Cache
            if (objects.ContainsKey(key) && !overrideWhenKeyExists)
            {
                if (!overrideWhenKeyExists)
                {
                    throw new ArgumentException("Key already exists in cache!");
                }
                else
                {
                    objects[key] = value;
                    return false;
                }
            }

            // Add the item to the cache, if we can
            if (maxCacheSize > 0)
            {
                objects.Add(key, value);
                queue.Enqueue(key);
                if (queue.Count > maxCacheSize)
                {
                    var keyToRemove = queue.Dequeue();
                    objects.Remove(keyToRemove);
                }
            }

            return true;
        }

        /// <summary>
        /// Resizes the cache.
        /// </summary>
        /// <param name="maxSize">The maximum size.</param>
        private void ResizeCache(int maxSize)
        {
            var tempCachedObjects = new Dictionary<K, V>(objects);
            objects = new Dictionary<K, V>(maxSize);
            foreach (var item in tempCachedObjects)
            {
                objects.Add(item.Key, item.Value);
            }

            var tempQueue = new Queue<K>(queue);
            queue = new Queue<K>(maxSize);
            while (queue.Count > 0)
            {
                queue.Enqueue(tempQueue.Dequeue());
            }
        }
    }
}
