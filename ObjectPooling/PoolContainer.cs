using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace Pooling
{
    public class PoolContainer<T> : IDisposable where T: IComplexContruct
    {
        #region Private Members

        private bool isDisposed;
        private Func<PoolContainer<T>, T> factory;
        private IItemStore<T> itemStore;
        private SemaphoreSlim semaphore;

        #endregion

        #region CTOR
        /// <summary>
        /// Initializes a new instance of the <see cref="PoolContainer{T}"/> class.
        /// </summary>
        /// <param name="size">The size.</param>
        /// <param name="factory">The factory.</param>
        public PoolContainer(int size, Func<PoolContainer<T>, T> factory)
            : this(size, factory, AccessMode.FIFO)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="PoolContainer{T}"/> class.
        /// </summary>
        /// <param name="size">The size.</param>
        /// <param name="factory">The factory.</param>
        /// <param name="accessMode">The access mode.</param>
        /// <exception cref="System.ArgumentOutOfRangeException">Argument 'size' must be greater than zero.</exception>
        /// <exception cref="System.ArgumentNullException">Factory delegete to create instance of IComplexContruct cannot be null.</exception>
        public PoolContainer(int size, Func<PoolContainer<T>, T> factory, AccessMode accessMode)
        {
            if (size <= 0)
                throw new ArgumentOutOfRangeException("size", size,
                    "Argument 'size' must be greater than zero.");
            if (factory == null)
                throw new ArgumentNullException("Factory delegete to create instance of IComplexContruct cannot be null.");

            this.factory = factory;
            semaphore = new SemaphoreSlim(size, size);
            this.itemStore = ItemStoreFactory<T>.CreateItemStore(accessMode, size);
            //Pre-load items into the itemstore
            PreloadItems(size);
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Gets a value indicating whether this instance is disposed.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is disposed; otherwise, <c>false</c>.
        /// </value>
        public bool IsDisposed
        {
            get { return isDisposed; }
        }

        /// <summary>
        /// Acquires an instance of IComplexContruct.
        /// </summary>
        /// <returns></returns>
        public T Acquire()
        {
            semaphore.Wait();
            lock (itemStore)
            {
                return itemStore.Fetch();
            }
        }

        /// <summary>
        /// Releases the specified instance of IComplexContruct.
        /// </summary>
        /// <param name="item">The item.</param>
        public void Release(T item)
        {
            lock (itemStore)
            {
                itemStore.Store(item);
            }
            semaphore.Release();
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            if (!isDisposed)
            {
                isDisposed = true;
                lock (itemStore)
                {
                    while (itemStore.Count > 0)
                    {
                        IDisposable disposable = itemStore.Fetch();
                        disposable.Dispose();
                    }
                }
                semaphore.Dispose();
            }
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// Preloads the items into item-store.
        /// </summary>
        /// <param name="size">The size.</param>
        private void PreloadItems(int size)
        {
            for (int itemIndex = 0; itemIndex < size; itemIndex++)
            {
                T item = factory(this);
                itemStore.Store(item);
            }
        }
        #endregion
    }
}