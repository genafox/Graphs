using System;
using System.IO;
using System.Linq;

namespace ConsoleRunner
{
	public class Program
	{
		static void Main(string[] args)
		{
			string[] rows = File.ReadAllLines("input.txt");

			GraphData data = new GraphData(rows);

			int result = new DenseGrpahsAlgorithm().GetMinWeight(data.VertexesCount, data.EdgeMatrix);

			using (StreamWriter sw = new StreamWriter("output.txt"))
			{
				sw.WriteLine(result.ToString());
			}
		}

		public class GraphData
		{
			private const int Infinity = int.MaxValue;

			public GraphData(string input) : this(input.Split(new[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries))
			{
			}

			public GraphData(string[] inputRows)
			{
				this.VertexesCount = Convert.ToInt32(inputRows[0].Split(' ')[0]);
				this.EdgesCount = Convert.ToInt32(inputRows[0].Split(' ')[1]);

				//v1, v2, e-weight
				int[][] intValues = inputRows
					.Skip(1)
					.Take(this.EdgesCount)
					.Select(r => r.Split(' ').Select(x => Convert.ToInt32(x)).ToArray())
					.ToArray();

				this.InitEdgeMatrix(this.EdgesCount, this.EdgesCount);
				for (int i = 0; i < this.EdgesCount; i++)
				{
					int[] edge = intValues[i];
					this.EdgeMatrix[edge[0] - 1, edge[1] - 1] = edge[2];
					this.EdgeMatrix[edge[1] - 1, edge[0] - 1] = edge[2];
				}
			}

			public int VertexesCount { get; private set; }

			public int EdgesCount { get; private set; }

			public int[,] EdgeMatrix { get; private set; }

			public void InitEdgeMatrix(int rowsCount, int columnsCount)
			{
				this.EdgeMatrix = new int[rowsCount, columnsCount];
				for (int i = 0; i < this.EdgeMatrix.GetLength(0); i++)
				{
					for (int j = 0; j < this.EdgeMatrix.GetLength(1); j++)
					{
						this.EdgeMatrix[i, j] = Infinity;
					}
				}
			}
		}

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
}
