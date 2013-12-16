using System;

namespace Async.Test
{
    class Program
    {
        static void Main(string[] args)
        {
            // run 1 million actions using TaskFactory.StartNew()  
            TaskFactoryStartNewExample.Run(1000 * 1000);

            // run 1 million actions using ThreadPool.QueueWorkItem() 
            ThreadPoolQueueWorkItemExample.Run(1000 * 1000);

            // run 100 actions using Thread.Start() 
            // unable to run 1 million :)
            ThreadStartExample.Run(100);

            Console.WriteLine("Done, press any key to exit");
            Console.ReadKey();
        }
    }
}
