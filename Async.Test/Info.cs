using System;
using System.Diagnostics;
using System.Threading;

namespace Async.Test
{
    static class Info
    {
        public static int GetUsedThreadPoolThreads()
        {
            int maxWorkers, maxIo, availableWorkers, availableIo;
            ThreadPool.GetMaxThreads(out maxWorkers, out maxIo);
            ThreadPool.GetAvailableThreads(out availableWorkers, out availableIo);
            return maxWorkers - availableWorkers;
        }

        public static int ThreadStats()
        {
            int threadCount = Process.GetCurrentProcess().Threads.Count;

            int maxWorkers, maxIo, availableWorkers, availableIo;
            ThreadPool.GetMaxThreads(out maxWorkers, out maxIo);
            ThreadPool.GetAvailableThreads(out availableWorkers, out availableIo);

            Console.WriteLine("Total {0} threads. Thread Pool max threads {1} , {2} available",
                threadCount, maxWorkers, availableWorkers);

            return threadCount;
        }
    }
}
