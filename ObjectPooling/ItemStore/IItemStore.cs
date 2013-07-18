using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pooling
{
    public interface IItemStore<T>
    {
        /// <summary>
        /// Fetches the current item instance.
        /// </summary>
        /// <returns></returns>
        T Fetch();

        /// <summary>
        /// Stores the specified item.
        /// </summary>
        /// <param name="item">The item.</param>
        void Store(T item);

        /// <summary>
        /// Gets the count.
        /// </summary>
        /// <value>
        /// The count.
        /// </value>
        int Count { get; }
    }
}
