using System.Collections.Generic;
using System.Linq;

namespace Labs.Lab_5
{
    public class ShortGamiltonCycleResult
    {
        public ShortGamiltonCycleResult(int distance, IEnumerable<Edge> edges)
        {
            Distance = distance;
            Edges = edges;
        }

        public int Distance { get; }

        public IEnumerable<Edge> Edges { get; }

        public string GetPath()
        {
            return string.Join(", ", this.Edges.Select(edge => $"({edge.From}, {edge.To})"));
        }
    }
}
