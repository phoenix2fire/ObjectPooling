using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pooling
{
    public interface IComplexContruct : IDisposable
    {
        /// <summary>
        /// Tests this instance.
        /// </summary>
        void Test();
    }
}
