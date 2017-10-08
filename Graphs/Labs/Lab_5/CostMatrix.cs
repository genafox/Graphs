using System;
using System.Collections.Generic;

namespace Labs.Lab_5
{
    public class CostMatrix
    {
        private const int Infinity = int.MaxValue;
        
        private readonly int[,] matrix;

        private readonly Lazy<IList<Edge>> edgesContainer;

        public CostMatrix(int vertexCount)
        {
            this.matrix = new int[vertexCount, vertexCount];

            for (int i = 0; i < vertexCount; i++)
            {
                for (int j = 0; j < vertexCount; j++)
                {
                    matrix[i, j] = Infinity;
                }
            }

            this.edgesContainer = new Lazy<IList<Edge>>(this.GetEdges);
        }

        public CostMatrix(int[,] matrix)
        {
            this.matrix = matrix;
            this.edgesContainer = new Lazy<IList<Edge>>(this.GetEdges);
        }

        public int[,] Matrix => this.matrix;

        public IList<Edge> Edges => this.edgesContainer.Value;

        public void AddEdge(int startV, int endV, int weight)
        {
            matrix[startV, endV] = weight;
        }

        private IList<Edge> GetEdges()
        {
            var edges = new List<Edge>();
            for (int i = 0; i < this.matrix.GetLength(0); i++)
            {
                for (int j = 0; j < this.matrix.GetLength(1); j++)
                {
                    if (matrix[i, j] != Infinity)
                    {
                        edges.Add(new Edge(i, j, matrix[i, j]));
                    }
                }
            }

            return edges;
        }
    }
}
