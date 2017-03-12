using System;
using System.Linq;

namespace PrimAlgorithm
{
	public class GraphData
	{
		private const int Infinity = int.MaxValue;

		public GraphData(string input)
		{
			string[] rows = input.Split(new [] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);

			this.VertexesCount = Convert.ToInt32(rows[0].Split(' ')[0]);
			this.EdgesCount = Convert.ToInt32(rows[0].Split(' ')[1]);

			//v1, v2, e-weight
			int[][] intValues = rows
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

		public int VertexesCount { get; }

		public int EdgesCount { get; }

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
}
