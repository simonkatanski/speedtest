using BenchmarkDotNet.Running;
using System;
using Test.Benchmarks;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Tester started");      
            var summary = BenchmarkRunner.Run<AdapterBenchmarks>();            
            Console.WriteLine("Finished");
            Console.ReadKey();
        }
    }
}
