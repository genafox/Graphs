using System;
using System.Collections.Generic;
using System.Linq;

namespace Labs.Lab_5
{
    public class LittleAlgotithm
    {
        private const int Infinity = int.MaxValue;

        private int bottomLimit = 0;

        private readonly CostMatrix originalMatrix;
        private readonly List<Cell> gamiltonCycle = new List<Cell>();
        private readonly VertexShortDistancesResult[] vertexDistances;

        public LittleAlgotithm(CostMatrix originalMatrix)
        {
            this.originalMatrix = originalMatrix;
            this.vertexDistances = new VertexShortDistancesResult[originalMatrix.Matrix.GetLength(0)];
        }

        public ShortGamiltonCycleResult FindGamiltonCycle()
        {
            Cell[,] normalizedMatrix = Normalize(this.originalMatrix);

            var iteratedMatrix = Iterate(normalizedMatrix);
            while (iteratedMatrix != null)
            {
                iteratedMatrix = Iterate(iteratedMatrix);
            }

            var resultEdges = new List<Edge>();
            foreach (Cell cell in gamiltonCycle)
            {
                if (cell.IsEdge)
                {
                    resultEdges.Add(new Edge(cell.OriginalRowIndex, cell.OriginalColumnIndex, cell.Value));
                }
                else
                {
                    resultEdges.AddRange(this.vertexDistances[cell.OriginalRowIndex].GetPath(cell.OriginalColumnIndex));
                }
            }

            return new ShortGamiltonCycleResult(this.bottomLimit, resultEdges);
        }

        private Cell[,] Iterate(Cell[,] normalizedMatrix)
        {
            Print(normalizedMatrix);
            List<Cell> zeros = new List<Cell>();

            for (int i = 0; i < normalizedMatrix.GetLength(0); i++)
            {
                int minInRow = GetMinInRow(normalizedMatrix, i);
                this.bottomLimit += minInRow;
                for (int j = 0; j < normalizedMatrix.GetLength(1); j++)
                {
                    if (normalizedMatrix[i, j].Value != Infinity)
                    {
                        normalizedMatrix[i, j].Value -= minInRow;
                        if (normalizedMatrix[i, j].Value == 0)
                        {
                            zeros.Add(normalizedMatrix[i, j]);
                        }
                    }
                }
            }

            for (int j = 0; j < normalizedMatrix.GetLength(0); j++)
            {
                int minInColumn = GetMinInColumn(normalizedMatrix, j);
                this.bottomLimit += minInColumn;
                if (minInColumn > 0)
                {
                    for (int i = 0; i < normalizedMatrix.GetLength(1); i++)
                    {
                        if (normalizedMatrix[i, j].Value != Infinity)
                        {
                            normalizedMatrix[i, j].Value -= minInColumn;
                            if (normalizedMatrix[i, j].Value == 0)
                            {
                                zeros.Add(normalizedMatrix[i, j]);
                            }
                        }
                    }
                }
            }

            if (normalizedMatrix.GetLength(0) > 2)
            {
                Cell maxZero = default(Cell);
                int maxZeroValue = 0;
                foreach (Cell zero in zeros)
                {
                    int minInRow = GetMinInRow(normalizedMatrix, zero.RowIndex, zero);
                    int minInColumn = GetMinInColumn(normalizedMatrix, zero.ColumnIndex, zero);

                    if (minInRow + minInColumn > maxZeroValue)
                    {
                        maxZero = zero;
                        maxZeroValue = minInRow + minInColumn;
                    }
                }

                gamiltonCycle.Add(maxZero);
                Cell[,] reducedMatrix = Reduce(normalizedMatrix, maxZero.RowIndex, maxZero.ColumnIndex);
                reducedMatrix[GetRowWithoutInfinity(reducedMatrix), GetColumnWithoutInfinity(reducedMatrix)].Value = Infinity;

                return reducedMatrix;
            }
            else
            {
                gamiltonCycle.AddRange(zeros);
                foreach (Cell zero in zeros)
                {
                    bottomLimit += zero.Value;
                }
            }

            return null;
        }

        private static int GetRowWithoutInfinity(Cell[,] matrix)
        {
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                bool hasInfinity = false;
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    if (matrix[i, j].Value == Infinity)
                    {
                        hasInfinity = true;
                        break;
                    }
                }

                if (!hasInfinity)
                {
                    return i;
                }
            }

            return -1;
        }

        private static int GetColumnWithoutInfinity(Cell[,] matrix)
        {
            for (int j = 0; j < matrix.GetLength(1); j++)
            {
                bool hasInfinity = false;
                for (int i = 0; i < matrix.GetLength(0); i++)
                {
                    if (matrix[i, j].Value == Infinity)
                    {
                        hasInfinity = true;
                        break;
                    }
                }

                if (!hasInfinity)
                {
                    return j;
                }
            }

            return -1;
        }

        private static int GetMinInRow(Cell[,] matrix, int rowNumber, Cell? except = null)
        {
            int minValue = Infinity;
            for (int j = 0; j < matrix.GetLength(1); j++)
            {
                if (matrix[rowNumber, j].Value < minValue && (except == null || j != except.Value.ColumnIndex))
                {
                    minValue = matrix[rowNumber, j].Value;
                }
            }

            return minValue;
        }

        private static int GetMinInColumn(Cell[,] matrix, int columnNumber, Cell? except = null)
        {
            int minValue = Infinity;
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                if (matrix[i, columnNumber].Value < minValue && (except == null || i != except.Value.RowIndex))
                {
                    minValue = matrix[i, columnNumber].Value;
                }
            }

            return minValue;
        }

        private Cell[,] Reduce(Cell[,] matrix, int deleteRow, int deleteColumn)
        {
            Cell[,] newMatrix = new Cell[matrix.GetLength(0) - 1, matrix.GetLength(0) - 1];
            for (int i = 0, row = 0; i < matrix.GetLength(0); i++)
            {
                if (i != deleteRow)
                {
                    for (int j = 0, col = 0; j < matrix.GetLength(1); j++)
                    {
                        if (j != deleteColumn)
                        {
                            newMatrix[row, col] = new Cell(
                                matrix[i, j].OriginalRowIndex,
                                matrix[i, j].OriginalColumnIndex,
                                row,
                                col,
                                matrix[i, j].Value,
                                matrix[i, j].IsEdge);

                            col++;
                        }
                    }

                    row++;
                }
            }

            return newMatrix;
        }

        private Cell[,] Normalize(CostMatrix matrix)
        {
            int vertexCount = matrix.Matrix.GetLength(0);
            Cell[,] normalized = new Cell[vertexCount, vertexCount];

            for (int i = 0; i < vertexCount; i++)
            {
                for (int j = 0; j < vertexCount; j++)
                {
                    int minDistance = GetMinVertexDistance(matrix.Edges, i, j, vertexCount);

                    if (i != j && matrix.Matrix[i, j] == Infinity)
                    {
                        normalized[i, j] = new Cell(i, j, i, j, minDistance, false);
                    }
                    else
                    {
                        int cellValue = i != j ? minDistance : matrix.Matrix[i, j];
                        normalized[i, j] = new Cell(i, j, i, j, cellValue, true);
                    }
                }
            }

            return normalized;
        }

        private int GetMinVertexDistance(IList<Edge> edges, int @from, int to, int vertexCount)
        {
            if (this.vertexDistances[@from] == default(VertexShortDistancesResult))
            {
                this.vertexDistances[@from] = FordBellmanAlgorithm.FindShortDistances(edges, @from, vertexCount);
            }

            return this.vertexDistances[@from].Get(to);
        }

        private void Print(Cell[,] matrix)
        {
            Console.WriteLine("---------------------------------");
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                Console.WriteLine();
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    Console.Write("{0}\t", matrix[i, j].Value);
                }
            }

            Console.WriteLine("---------------------------------");
        }
    }
}
