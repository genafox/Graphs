using System;
using Labs.Lab_4;
using Labs.Lab_5;

namespace Labs
{
    class Program
    {
        static void Main(string[] args)
        {
            var travellingSalesmen = new TravellingSalesmanProblemRunner();
            travellingSalesmen.Run();

            var maxFlow = new MaxFlowMinCostRunner();
            maxFlow.Run();

            Console.ReadKey();
        }
    }
}
