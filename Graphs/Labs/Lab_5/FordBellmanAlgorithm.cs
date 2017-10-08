using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labs.Lab_5
{
    public class FordBellmanAlgorithm
    {
        private const int Infinity = int.MaxValue;

        public static VertexShortDistancesResult FindShortDistances(IList<Edge> edges, int from, int vertexCount)
        {
            var distances = new int[vertexCount];
            for (int i = 0; i < vertexCount; i++)
            {
                distances[i] = Infinity;
            }

            var fromParentToChildEdges = new Edge[vertexCount];

            distances[from] = 0;

            while (true)
            {
                bool any = false;

                foreach (Edge edge in edges)
                {
                    if (distances[edge.From] < Infinity)
                    {
                        if (distances[edge.To] > distances[edge.From] + edge.Weight)
                        {
                            distances[edge.To] = distances[edge.From] + edge.Weight;
                            fromParentToChildEdges[edge.To] = edge;
                            any = true;
                        }
                    }
                }

                if (!any)
                {
                    break;
                }
            }

            return new VertexShortDistancesResult(from, distances, fromParentToChildEdges);
        }
    }
}
