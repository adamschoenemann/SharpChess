using System;
using SharpChess;

namespace SharpChess
{
	public class Knight : ChessPiece
	{
		public Knight(Player player) : base(player)
		{
		}

		#region implemented abstract members of ChessPiece

		public override bool CanMove(int rows, int cols, Tile tile)
		{
			if (Math.Abs(rows) == 2 && Math.Abs(cols) == 1)
				return true;
			if (Math.Abs(cols) == 2 && Math.Abs(rows) == 1)
				return true;
			return false;
		}

		#endregion

	}
}

