using System;
using SharpChess;

namespace SharpChess
{
	public class Queen : ChessPiece
	{
		public Queen(Player player) : base(player)
		{
		}

		#region implemented abstract members of ChessPiece

		public override bool CanMove(int rows, int cols, Tile tile)
		{
			if (rows == 0 && Math.Abs(cols) > 0)
				return true;
			if (cols == 0 && Math.Abs(rows) > 0)
				return true;
			if (Math.Abs(cols) == Math.Abs(rows))
				return true;
			return false;
		}

		#endregion

	}
}

