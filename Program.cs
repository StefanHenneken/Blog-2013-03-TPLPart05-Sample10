using System;
using System.Threading;
using System.Threading.Tasks;

namespace Sample10
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Start");
            TaskScheduler.UnobservedTaskException += TaskScheduler_UnobservedTaskException;
            Task.Run(() =>
            {
                throw new InvalidOperationException();
            });
            Thread.Sleep(100);
            GC.Collect();
            GC.WaitForPendingFinalizers();
            Console.ReadLine();
        }
        private static void TaskScheduler_UnobservedTaskException(object sender, UnobservedTaskExceptionEventArgs e)
        {
            Console.WriteLine("Unobserved Task Exception: " + e.Exception.Message);
            e.SetObserved();
            e.Exception.Flatten();
            foreach (Exception ex in e.Exception.InnerExceptions)
                Console.WriteLine("  Exception: " + ex.Message);
        }
    }
}
