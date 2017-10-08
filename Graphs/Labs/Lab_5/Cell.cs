namespace Labs.Lab_5
{
    public struct Cell
    {
        public Cell(int originalRowIndex, int originalColumnIndex, int rowIndex, int columnIndex, int value, bool isEdge)
        {
            this.OriginalRowIndex = originalRowIndex;
            this.OriginalColumnIndex = originalColumnIndex;
            this.RowIndex = rowIndex;
            this.ColumnIndex = columnIndex;
            this.Value = value;
            this.IsEdge = isEdge;
        }

        public int OriginalRowIndex { get; }

        public int OriginalColumnIndex { get; }

        public int RowIndex { get; }

        public int ColumnIndex { get; }

        public int Value { get; set; }

        public bool IsEdge { get; }
    }
}
