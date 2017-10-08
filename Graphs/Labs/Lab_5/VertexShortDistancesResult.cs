using System.Collections.Generic;

namespace Labs.Lab_5
{
    public class VertexShortDistancesResult
    {
        private readonly int[] distances;

        private readonly Edge[] fromParentToChildEdges;

        public VertexShortDistancesResult(int vertex, int[] distances, Edge[] fromParentToChildEdges)
        {
            this.Vertex = vertex;
            this.distances = distances;
            this.fromParentToChildEdges = fromParentToChildEdges;
        }

        public int Vertex { get; }

        public int Get(int to)
        {
            return distances[to];
        }

        public IEnumerable<Edge> GetPath(int to)
        {
            var result = new List<Edge>();
            int currentTo = to;

            while (currentTo != Vertex)
            {
                result.Insert(0, fromParentToChildEdges[currentTo]);
                currentTo = fromParentToChildEdges[currentTo].From;
            }

            return result;
        }
    }
}
