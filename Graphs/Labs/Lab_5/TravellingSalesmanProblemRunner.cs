using System;

namespace Labs.Lab_5
{
    public class TravellingSalesmanProblemRunner
    {
        public void Run()
        {
            // G - 1
            //var matrix = G1();

            //G - 2
            var matrix = G2();

            var algorithm = new LittleAlgotithm(matrix);

            ShortGamiltonCycleResult result = algorithm.FindGamiltonCycle();

            Console.WriteLine("Path: {0}", result.GetPath());
        }

        private CostMatrix G1()
        {
            var matrix = new CostMatrix(7);
            matrix.AddEdge(0, 1, 12);
            matrix.AddEdge(1, 0, 12);
            matrix.AddEdge(1, 2, 10);
            matrix.AddEdge(2, 1, 10);
            matrix.AddEdge(3, 2, 17);
            matrix.AddEdge(0, 3, 28);
            matrix.AddEdge(3, 0, 28);
            matrix.AddEdge(1, 3, 43);
            matrix.AddEdge(3, 1, 43);
            matrix.AddEdge(4, 1, 31);
            matrix.AddEdge(2, 4, 10);
            matrix.AddEdge(4, 2, 10);
            matrix.AddEdge(4, 5, 8);
            matrix.AddEdge(5, 4, 14);
            matrix.AddEdge(5, 6, 6);
            matrix.AddEdge(6, 5, 6);

            return matrix;
        }

        private CostMatrix G2()
        {
            var matrix = new CostMatrix(4);
            matrix.AddEdge(0, 1, 42);
            matrix.AddEdge(0, 2, 30);
            matrix.AddEdge(0, 3, 12);
            matrix.AddEdge(1, 0, 42);
            matrix.AddEdge(1, 2, 20);
            matrix.AddEdge(1, 3, 35);
            matrix.AddEdge(2, 0, 30);
            matrix.AddEdge(2, 1, 20);
            matrix.AddEdge(2, 3, 34);
            matrix.AddEdge(3, 0, 12);
            matrix.AddEdge(3, 1, 35);
            matrix.AddEdge(3, 2, 34);

            return matrix;
        }
    }
}
