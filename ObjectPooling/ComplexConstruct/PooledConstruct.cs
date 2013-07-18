using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pooling
{
    public class PooledConstruct<T> : IComplexContruct where T: IComplexContruct, new()
    {
        #region Private Members

        private T complexConstruct;
        private PoolContainer<IComplexContruct> poolContainer;

        #endregion

        #region CTOR
        /// <summary>
        /// Initializes a new instance of the <see cref="PooledConstruct{T}" /> class.
        /// </summary>
        /// <param name="poolContainer">The pool container.</param>
        /// <param name="complexConstruct">The complex construct.</param>
        /// <exception cref="System.ArgumentNullException">Pool container cannot be null.</exception>
        public PooledConstruct(PoolContainer<IComplexContruct> poolContainer, T complexConstruct)
        {
            if (poolContainer == null)
                throw new ArgumentNullException("Pool container cannot be null.");

            this.poolContainer = poolContainer;
            this.complexConstruct = complexConstruct;
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            if (poolContainer.IsDisposed)
            {
                complexConstruct.Dispose();
            }
            else
            {
                poolContainer.Release(this);
            }
        }

        /// <summary>
        /// Tests this instance.
        /// </summary>
        public void Test()
        {
            complexConstruct.Test();
        }
        #endregion
    }
}
