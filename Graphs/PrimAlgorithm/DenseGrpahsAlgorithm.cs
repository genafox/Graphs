using System;
using System.Linq;

namespace PrimAlgorithm
{
	public class DenseGrpahsAlgorithm
	{
		private const int Infinity = int.MaxValue;
		private const int Nonexistent = -1;

		public int GetMinWeight(int vertexCount, int[,] edgeMatrix)
		{
			int result = 0;

			bool[] usedVertexes = new bool[vertexCount];
			int[] minVertexEdges = Enumerable.Repeat(Infinity, vertexCount).ToArray();
			int[] minVertexEdgeEnds = Enumerable.Repeat(Nonexistent, vertexCount).ToArray();

			minVertexEdges[0] = 0;
			for (int i = 0; i < vertexCount; ++i)
			{
				int vertexWithMinEdge = -1;
				for (int j = 0; j < vertexCount; ++j)
					if (!usedVertexes[j] && 
						(vertexWithMinEdge == Nonexistent || minVertexEdges[j] < minVertexEdges[vertexWithMinEdge]))
						vertexWithMinEdge = j;
				if (minVertexEdges[vertexWithMinEdge] == Infinity)
				{
					throw new ArgumentException("Graph is not linked");
				}

				usedVertexes[vertexWithMinEdge] = true;
				if (minVertexEdgeEnds[vertexWithMinEdge] != Nonexistent)
					result += edgeMatrix[vertexWithMinEdge, minVertexEdgeEnds[vertexWithMinEdge]];

				for (int k = 0; k < vertexCount; k++)
					if (edgeMatrix[vertexWithMinEdge, k] < minVertexEdges[k])
					{
						minVertexEdges[k] = edgeMatrix[vertexWithMinEdge, k];
						minVertexEdgeEnds[k] = vertexWithMinEdge;
					}
			}

			return result;
		}
	}
}
