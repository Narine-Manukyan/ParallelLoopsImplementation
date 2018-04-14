using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ParellelLoops
{
    /// <summary>
    /// Provides support for parallel loops and regions.
    /// </summary>
    public static class MyParallel
    {
        /// <summary>
        /// Executes a for loop in which iterations may run in parallel.
        /// </summary>
        /// <param name="fromInclusive">The start index(inclusive)</param>
        /// <param name="toExclusive">The end index (exclusive)</param>
        /// <param name="body">The delegate that is invoked once per iteration.<param>
        public static void ParallelFor(int fromInclusive, int toExclusive, Action<int> body)
        {
            if (body == null)
                throw new ArgumentNullException("Body is Null.");
            // If 'to' is less than 'from'.
            if (toExclusive <= fromInclusive)
                return;
            // List for tasks.
            List<Task> tasks = new List<Task>();
            // Current position.
            int j = fromInclusive;
            for (int i = fromInclusive; i < toExclusive; i++)
            {
                // Starting new Task.
                Task t = Task.Run(() => body(j++));
                tasks.Add(t);
            }
            // Wait for each task parallel.
            Task.WaitAll(tasks.ToArray());
        }

        /// <summary>
        /// Executes a foreach operation on an IEnumerable in which iterations may run in parallel, 
        /// loop options can be configured, and the state of the loop can be monitored and manipulated.
        /// </summary>
        /// <typeparam name="TSource">The type of the data in the source.</typeparam>
        /// <param name="source">An enumerable data source.</param>
        /// <param name="body">The delegate that is invoked once per iteration.</param>
        public static void ParallelForEach<TSource>(IEnumerable<TSource> source, Action<TSource> body)
        {
            if(body == null)
                throw new ArgumentNullException("Body is Null.");
            if(source == null)
                throw new ArgumentNullException("Source is Null.");
            // List for tasks.
            List<Task> tasks = new List<Task>();
            foreach (TSource item in source)
            {
                // Starting new Task.
                Task t = Task.Run(() => body(item));
                tasks.Add(t);
            }
            // Wait for each task parallel.
            Task.WaitAll(tasks.ToArray());
        }

        /// <summary>
        /// Executes a foreach operation on an IEnumerable in which iterations may run in parallel and loop options can be configured.
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="source">An enumerable data source.</param>
        /// <param name="parallelOptions">An object that configures the behavior of this operation.</param>
        /// <param name="body">The delegate that is invoked once per iteration.</param>
        public static void ParallelForEachWithOptions<TSource>(IEnumerable<TSource> source,
            ParallelOptions parallelOptions, Action<TSource> body)
        {
            if (body == null)
                throw new ArgumentNullException("Body is Null.");
            if (source == null)
                throw new ArgumentNullException("Source is Null.");
            if (parallelOptions == null)
                throw new ArgumentNullException("ParallelOptions is Null.");
            if (parallelOptions.MaxDegreeOfParallelism <= -1)
                throw new Exception("MaxDegreeOfParallelism is less than -1");
            // List for tasks.
            // MaxDegreeOfParallelism Gets or Sets the maximum number of concurrent tasks 
            // enabled by this ParallelOptions instance.
            List<Task> tasks = new List<Task>();
            int j = 0;
            foreach (TSource item in source)
            {
                // Starting new Task.
                Task t = Task.Run(() => body(item));
                tasks.Add(t);
                j = tasks.IndexOf(t);
                if (j >= parallelOptions.MaxDegreeOfParallelism)
                {
                    Task.WaitAny(tasks.ToArray());
                }
                j++;
            }
            // Wait for each task parallel.
            Task.WaitAll(tasks.ToArray());
        }
    }
}
