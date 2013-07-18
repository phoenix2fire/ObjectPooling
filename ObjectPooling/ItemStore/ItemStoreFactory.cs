using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pooling
{
    public class ItemStoreFactory<T>
    {
        /// <summary>
        /// Creates the item store.
        /// </summary>
        /// <param name="mode">The mode.</param>
        /// <param name="capacity">The capacity.</param>
        /// <returns></returns>
        public static IItemStore<T> CreateItemStore(AccessMode mode, int capacity)
        {
            switch (mode)
            {
                case AccessMode.LIFO:
                    return new StackStore<T>(capacity);
                default:
                    return new QueueStore<T>(capacity);
            }
        }
    }
}
