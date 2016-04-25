using System;

namespace AstroApp.Helpers
{
	public class Layout : IEquatable<Layout>
	{
		public int RowCount { get; }
		public int ColumnCount { get;  }
		public double Ratio { get; }

		public Layout(int rowCount, int columnCount)
		{
			RowCount = rowCount;
			ColumnCount = columnCount;
			Ratio = (float)columnCount / rowCount;
		}

		#region Equals part
		public override bool Equals(object obj)
		{
			if (ReferenceEquals(null, obj)) return false;
			if (ReferenceEquals(this, obj)) return true;
			var other = obj as Layout;
			return other != null && Equals(other);
		}

		public bool Equals(Layout other)
		{
			if (ReferenceEquals(null, other)) return false;
			if (ReferenceEquals(this, other)) return true;
			return RowCount == other.RowCount && ColumnCount == other.ColumnCount;
		}

		public override int GetHashCode()
		{
			unchecked
			{
				return (RowCount*397) ^ ColumnCount;
			}
		}
		#endregion

		public override string ToString()
		{
			return $"{ColumnCount}x{RowCount} ({Ratio})";
		}
	}
}
