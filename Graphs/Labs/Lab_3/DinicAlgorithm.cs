using System;
using System.Collections.Generic;

namespace Labs.Lab_3
{
    public class DinicAlgorithm
    {
        private const int Infinity = int.MaxValue;

        private readonly int vertexCount;
        private readonly int[,] capacityMatrix;
        private readonly int[,] flowMatrix;

        private int[] distances;
        private int[] parents;

        public DinicAlgorithm(int vertexCount, int[,] capacityMatrix)
        {
            this.vertexCount = vertexCount;
            this.capacityMatrix = capacityMatrix;
            this.flowMatrix = new int[this.vertexCount, this.vertexCount];

            this.distances = new int[this.vertexCount];
            this.parents = new int[this.vertexCount];
        }

        public int CalculateMaxFlow(int s, int t)
        {
            int flow = 0;
            for (;;)
            {
                if (!this.Bfs(s, t))
                {
                    break;
                }

                this.parents = new int[this.vertexCount];
                int pushed = this.Dfs(s, Infinity, t);
                while (pushed != 0)
                {
                    flow += pushed;

                }
            }

            return flow;
        }

        private bool Bfs(int s, int t)
        {
            this.distances = new int[this.vertexCount];
            for (int i = 0; i < this.distances.Length; i++)
            {
                distances[i] = -1;
            }

            Queue<int> queue = new Queue<int>();
            queue.Enqueue(s);
            this.distances[s] = 0;

            while (queue.Count > 0)
            {
                int v = queue.Dequeue();
                for (int to = 0; to < this.vertexCount; to++)
                    if (distances[to] == -1 && this.flowMatrix[v, to] < this.capacityMatrix[v, to])
                    {
                        queue.Enqueue(to);
                        this.distances[to] = this.distances[v] + 1;
                    }
            }

            return this.distances[t] != -1;
        }

        private int Dfs(int v, int flow, int t)
        {
            if (flow == 0)
            {
                return 0;
            }
            if (v == t)
            {
                return flow;
            }

            for (int to = this.parents[v]; to < this.vertexCount; to++, this.parents[v] = to)
            {
                if (this.distances[to] != this.distances[v] + 1)
                {
                    continue;
                }

                int pushed = Dfs(to, Math.Min(flow, this.capacityMatrix[v, to] - this.flowMatrix[v, to]), t);
                if (pushed != 0)
                {
                    this.flowMatrix[v, to] += pushed;
                    this.flowMatrix[to, v] -= pushed;

                    return pushed;
                }
            }

            return 0;
        }
    }
}
