using Microsoft.VisualStudio.TestTools.UnitTesting;
using PrimAlgorithm;

namespace Tests.Prim
{
	[TestClass]
	public class DenseGraphsAlgoritmTests
	{
		[TestMethod]
		public void GetMinWeight_WithDataUnit1_ReturnsCorrectResult()
		{
			string inputData = "4 4\r\n1 2 1\r\n2 3 2\r\n3 4 5\r\n4 1 4";
			var graphData = new GraphData(inputData);

			int minWeight = new DenseGrpahsAlgorithm().GetMinWeight(graphData.VertexesCount, graphData.EdgeMatrix);

			Assert.AreEqual(7, minWeight);
		}

		[TestMethod]
		public void GetMinWeight_WithDataUnit2_ReturnsCorrectResult()
		{
			string inputData = "3 3\r\n1 2 1\r\n2 3 2\r\n3 1 3";
			var graphData = new GraphData(inputData);

			int minWeight = new DenseGrpahsAlgorithm().GetMinWeight(graphData.VertexesCount, graphData.EdgeMatrix);

			Assert.AreEqual(3, minWeight);
		}
	}
}
