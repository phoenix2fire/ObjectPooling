using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Pooling
{
    public class VideoFrame : IComplexContruct
    {
        #region Private Members
        
        private static int count = 0;
        private int frameNumber;

        #endregion

        #region Public Methods
        /// <summary>
        /// Initializes a new instance of the <see cref="VideoFrame"/> class.
        /// </summary>
        public VideoFrame()
        {
            frameNumber = Interlocked.Increment(ref count);
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            Console.WriteLine("Frame #{0} disposed!", frameNumber);
        }

        /// <summary>
        /// Tests this instance.
        /// </summary>
        public void Test()
        {
            Console.WriteLine(Thread.CurrentThread.Name + ": Frame #{0} tested!", frameNumber);
        }
        #endregion
    }
}
