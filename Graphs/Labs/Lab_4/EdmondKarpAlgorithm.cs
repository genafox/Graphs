using System;
using System.Collections.Generic;

namespace Labs.Lab_4
{
    public class EdmondKarpAlgorithm
    {
        private const int Infinity = int.MaxValue;

        private readonly int vertexCount;

        //Common
        private readonly int[,] capacityMatrix;
        private readonly int[,] costMatrix;
        private readonly int[,] flowMatrix;

        // Raund Data
        private readonly int[] parents;
        private readonly int[] vertexResidualCapacity;
        private readonly int[] distances;

        public EdmondKarpAlgorithm(int vertexCount, int[,] capacityMatrix, int[,] costMatrix)
        {
            this.vertexCount = vertexCount;
            this.capacityMatrix = capacityMatrix;
            this.costMatrix = costMatrix;
            this.flowMatrix = new int[this.vertexCount, this.vertexCount];

            this.parents = new int[this.vertexCount];
            this.distances = new int[this.vertexCount];
            this.vertexResidualCapacity = new int[this.vertexCount];

            this.ResetRaundData();
        }

        public long MaxFlow { get; private set; }

        public long MinCost { get; private set; }

        public void Run(int s, int t)
        {
            this.CalculateMaxFlow(s, t);

            int negativeCycleVertex = CheckNegativeCycle(t);
            while (negativeCycleVertex != Infinity)
            {
                int v = negativeCycleVertex;
                int u = this.parents[v];
                int add = Infinity;
                do
                {
                    add = Math.Min(add, this.capacityMatrix[u, v] - this.flowMatrix[u, v]);
                    v = u;
                    u = this.parents[v];
                }
                while (v != negativeCycleVertex);

                v = negativeCycleVertex;
                u = this.parents[v];
                do
                {
                    this.flowMatrix[u, v] += add;
                    this.flowMatrix[v, u] -= add;
                    v = u;
                    u = this.parents[v];
                }
                while (v != negativeCycleVertex);

                negativeCycleVertex = CheckNegativeCycle(t);
            }

            for (int u = 0; u < this.vertexCount; u++)
            {
                for (int v = 0; v < this.vertexCount; v++)
                {
                    if (this.flowMatrix[u, v] > 0)
                    {
                        this.MinCost += this.flowMatrix[u, v] * this.costMatrix[u, v];
                    }
                }
            }
        }

        private void ResetRaundData()
        {
            for (int i = 1; i < this.vertexCount; i++)
            {
                this.parents[i] = -1;
                this.vertexResidualCapacity[i] = -1;
                this.distances[i] = Infinity;
            }
        }

        private bool Bfs(int s, int t)
        {
            this.ResetRaundData();
            Queue<int> queue = new Queue<int>();
            bool[] marks = new bool[this.vertexCount];
            marks[s] = true;
            this.parents[s] = s;
            this.vertexResidualCapacity[s] = Infinity;

            queue.Enqueue(s);
            while (!marks[t] && queue.Count > 0)
            {
                int u = queue.Dequeue();
                for (int v = 0; v < this.vertexCount; v++)
                {
                    int residualCapacity = this.capacityMatrix[u, v] - this.flowMatrix[u, v];
                    if (!marks[v] && residualCapacity > 0)
                    {
                        this.vertexResidualCapacity[v] = Math.Min(this.vertexResidualCapacity[u], residualCapacity);
                        marks[v] = true;
                        parents[v] = u;
                        queue.Enqueue(v);
                    }
                }

                    
            }

            return marks[t];
        }

        private int CheckNegativeCycle(int s)
        {
            this.ResetRaundData();
            Queue<int> queue = new Queue<int>();
            this.parents[s] = 0;
            this.distances[s] = 0;

            queue.Enqueue(s);
            queue.Enqueue(Infinity);

            int series = 0;
            while (queue.Count > 0)
            {
                if (queue.Peek() == Infinity)
                {
                    queue.Dequeue();
                    if (++series > this.vertexCount)
                    {
                        return check_cycles();
                    }
                    if (queue.Count == 0)
                    {
                        return Infinity;
                    }

                    queue.Enqueue(Infinity);
                }

                int u = queue.Dequeue();
                for (int v = 0; v < this.vertexCount; v++)
                    if (this.distances[v] > this.distances[u] + edge_cost(u, v))
                    {
                        this.distances[v] = this.distances[u] + edge_cost(u, v);
                        this.parents[v] = u;
                        queue.Enqueue(v);
                    }
            }

            return Infinity;
        }

        private int check_cycles()
        {
            for (int u = 1; u < this.vertexCount; u++)
                for (int v = 1; v < this.vertexCount; v++)
                    if (distances[v] > distances[u] + edge_cost(u, v))
                        return u;

            return Infinity;
        }

        private int edge_cost(int u, int v)
        {
            if (this.capacityMatrix[u, v] - this.flowMatrix[u, v] > 0)
            {
                return this.costMatrix[u, v];
            }

            return Infinity;
        }

        private void CalculateMaxFlow(int s, int t)
        {
            var flow = 0;
            while (Bfs(s, t))
            {
                int add = this.vertexResidualCapacity[t];

                int v = t;
                int u = this.parents[v];
                while (v != s)
                {
                    this.flowMatrix[u, v] += add;
                    this.flowMatrix[v, u] -= add;
                    v = u;
                    u = this.parents[v];
                }

                flow += add;
            }

            this.MaxFlow = flow;
        }
    }
}
