using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pooling
{
    class QueueStore<T> : IItemStore<T>
    {
        private Queue<T> internalQueue;

        /// <summary>
        /// Initializes a new instance of the <see cref="QueueStore{T}"/> class.
        /// </summary>
        /// <param name="capacity">The capacity.</param>
        public QueueStore(int capacity)
        {
            internalQueue = new Queue<T>(capacity);
        }

        /// <summary>
        /// Fetches the current item instance.
        /// </summary>
        /// <returns></returns>
        public T Fetch()
        {
            return internalQueue.Dequeue();
        }

        /// <summary>
        /// Stores the specified item.
        /// </summary>
        /// <param name="item">The item.</param>
        public void Store(T item)
        {
            internalQueue.Enqueue(item);
        }

        /// <summary>
        /// Gets the count.
        /// </summary>
        /// <value>
        /// The count.
        /// </value>
        public int Count
        {
            get
            {
                return internalQueue.Count;
            }
        }
    }
}
