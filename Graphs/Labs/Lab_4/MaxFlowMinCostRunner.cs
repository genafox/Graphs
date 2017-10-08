using System;

namespace Labs.Lab_4
{
    public class MaxFlowMinCostRunner
    {
        public void Run()
        {
            var graph = new CostFlowMatrix(4);

            graph.AddEdge(0, 1, 1, 2);
            graph.AddEdge(0, 2, 2, 2);
            graph.AddEdge(2, 1, 1, 1);
            graph.AddEdge(1, 3, 2, 1);
            graph.AddEdge(2, 3, 2, 3);

            var algorithm = new EdmondKarpAlgorithm(4, graph.CapacityMatrix, graph.CostMatrix);

            algorithm.Run(0, 3);

            Console.WriteLine("Max flow:{0}", algorithm.MaxFlow);
            Console.WriteLine("Min cost:{0}", algorithm.MinCost);
        }
    }
}
