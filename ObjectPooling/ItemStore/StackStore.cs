using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pooling
{
    class StackStore<T> : IItemStore<T>
    {
        private Stack<T> internalStack;

        /// <summary>
        /// Initializes a new instance of the <see cref="StackStore{T}"/> class.
        /// </summary>
        /// <param name="capacity">The capacity.</param>
        public StackStore(int capacity)
        {
            internalStack = new Stack<T>(capacity);
        }

        /// <summary>
        /// Fetches the current item instance.
        /// </summary>
        /// <returns></returns>
        public T Fetch()
        {
            return internalStack.Pop();
        }

        /// <summary>
        /// Stores the specified item.
        /// </summary>
        /// <param name="item">The item.</param>
        public void Store(T item)
        {
            internalStack.Push(item);
        }

        /// <summary>
        /// Gets the count.
        /// </summary>
        /// <value>
        /// The count.
        /// </value>
        public int Count
        {
            get { return internalStack.Count; }
        }
    }
}
