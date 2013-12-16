using System;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Async.Test
{
    static class TaskFactoryStartNewExample
    {
        /// <summary>
        /// Use Task.Factory.StartNew() to enqueue @total ( 1 million ) number of actions.
        /// The number of native threads used to execute the 1 million actions is managed by the framework ( by default using the thread pool )
        /// </summary>
        public static void Run(int total)
        {
            Console.WriteLine("\n=========================================================");
            Console.WriteLine("==== TaskFactory.StartNew() ====\n");

            // initial number of threads ( to account for threads used by the framework ) 
            var threads = Info.ThreadStats();

            var w = Stopwatch.StartNew();
            int count = 0;

            // use an event to make sure tasks/threads don't finish before all tasks/threads are created
            using (ManualResetEvent evt = new ManualResetEvent(false))
            {
                // start @total number of tasks
                var tasks = Enumerable.Range(0, total)
                    .Select(i => Task.Factory.StartNew(() =>
                    {
                        // will be set after all tasks are created
                        evt.WaitOne();
                        Thread.SpinWait(100); // some computation
                        Interlocked.Increment(ref count); // just for checking we actually run the task
                    }))
                    .ToArray();

                // the actual Threads created at this point is a lot less than @total , on my machine ~5 threads are actually created
                Console.WriteLine("Using {0} threads, {1} from thread pool, for {2} tasks",
                    Process.GetCurrentProcess().Threads.Count - threads,
                    Info.GetUsedThreadPoolThreads(),
                    tasks.Length);

                // start the tasks
                evt.Set();

                Task.WaitAll(tasks);

                Console.WriteLine("Total time {0}", w.Elapsed);

                Debug.Assert(count == total);
            }

            Info.ThreadStats();
        }
    }
}
