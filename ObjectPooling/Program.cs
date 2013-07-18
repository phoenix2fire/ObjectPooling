using System;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Pooling
{
    public class Program
    {
        /// <summary>
        /// The pool size
        /// </summary>
        public static int PoolSize = 5;

        public static void Main(string[] args)
        {
            using (PoolContainer<IComplexContruct> poolContainer = new PoolContainer<IComplexContruct>(PoolSize, VideoFactory))
            {
                int concurrentThreads = 3;
                int remaining = concurrentThreads;
                List<Task> taskList = new List<Task>();
                for (int index = 0; index < concurrentThreads; index++)
                {
                    int threadIndex = index;
                    taskList.Add(Task.Factory.StartNew(() =>
                    {
                        Thread.CurrentThread.Name = string.Format("Thread No: {0}", threadIndex);
                        for (int j = 0; j < 2; j++)
                        {
                            using (IComplexContruct complexConstruct = poolContainer.Acquire())
                            {
                                using (IComplexContruct complexConstructNew = poolContainer.Acquire())
                                {
                                    complexConstruct.Test();
                                    complexConstructNew.Test();
                                }
                            }
                        }
                    }));
                }
                Task.WaitAll(taskList.ToArray());
                Console.WriteLine("Multi-Threaded pooling test finished!\n");
            }
            Console.ReadLine();
        }

        /// <summary>
        /// Video factory.
        /// </summary>
        /// <param name="poolContainer">The pool container.</param>
        /// <returns></returns>
        public static IComplexContruct VideoFactory(PoolContainer<IComplexContruct> poolContainer)
        {
            return new PooledConstruct<VideoFrame>(poolContainer, new VideoFrame());
        }
    }
}