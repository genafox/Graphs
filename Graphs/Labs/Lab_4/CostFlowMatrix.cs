namespace Labs.Lab_4
{
    public class CostFlowMatrix
    {
        private readonly int[,] capacityMatrix;
        private readonly int[,] costMatrix;

        public CostFlowMatrix(int vertexCount)
        {
            this.costMatrix = new int[vertexCount, vertexCount];
            this.capacityMatrix = new int[vertexCount, vertexCount];

            for (int i = 0; i < vertexCount; i++)
            {
                for (int j = 0; j < vertexCount; j++)
                {
                    this.costMatrix[i, j] = 0;
                    this.capacityMatrix[i, j] = 0;
                }
            }
        }

        public int[,] CostMatrix => this.costMatrix;

        public int[,] CapacityMatrix => this.capacityMatrix;

        public void AddEdge(int startV, int endV, int capacity, int cost)
        {
            this.capacityMatrix[startV, endV] = capacity;
            this.costMatrix[startV, endV] = cost;
            this.costMatrix[endV, startV] = -cost;
        }
    }
}
