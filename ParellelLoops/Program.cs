using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParellelLoops
{
    class Program
    {
        static void Main(string[] args)
        {
            //// Testing ParallelFor.
            //MyParallel.ParallelFor(0, 100, index => {
            //    Console.WriteLine("Task ID {0} processing index: {1}",
            //    Task.CurrentId, index);
            //});

            //// Testing ParallelForEach.
            //MyParallel.ParallelForEach(
            //    new List<string> { "apple", "blue", "Hello", "cotton", "dark", "fox" },
            //    item => 
            //    {
            //        Console.WriteLine("Item {0} has {1} characters",
            //        item, item.Length);
            //    });

            //Testing ParallelForEachWithOptions.
            MyParallel.ParallelForEachWithOptions(new List<int> { 1, 2, 3, 4, 5, 6, 7 },
                new ParallelOptions { MaxDegreeOfParallelism = 3 },
                value =>
                {
                    Console.WriteLine("Task ID: {0},\tValue {1} ", Task.CurrentId, value);
                });

            //Parallel.ForEach(new List<int> { 1, 2, 3, 4, 5, 6, 7 },
            //    new ParallelOptions { MaxDegreeOfParallelism = 3},
            //    index =>
            //    {
            //        Console.WriteLine("Task ID: {0}, Value {1} ", Task.CurrentId, index);
            //    });
        }
    }
}
