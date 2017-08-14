using System.Collections.Generic;
using System.Linq;

namespace Visyn.Mathematics.Matrix
{
    public static class Matrix2DUniform
    {
        public static int RowCount<T>(this T[,] data) => data.GetLength(0);
        public static int ColumnCount<T>(this T[,] data) => data.GetLength(1);

        public static IEnumerable<IEnumerable<T>> Rows<T>(this T[,] data)
        {
            var rowCount = data.RowCount();
            for (var row = 0; row < rowCount; row++)
            {
                yield return RowData(data, row);
            }
        }

        public static IEnumerable<T[]> ToRowArrays<T>(this T[,] data)
        {
            var rowCount = data.RowCount();
            for (var row = 0; row < rowCount; row++)
            {
                yield return RowData(data, row).ToArray();
            }
        }

        public static IEnumerable<T> RowData<T>(this T[,] data, int rowIndex)
        {
            var columnCount = data.ColumnCount();
            for (var c = 0; c < columnCount; c++)
            {
                yield return data[rowIndex, c];
            }
        }
    }
}
