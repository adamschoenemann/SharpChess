using System;
using SharpChess;

namespace SharpChess
{
	public class Bishop : ChessPiece
	{
		public Bishop(Player player) : base(player)
		{
		}

		#region implemented abstract members of ChessPiece

		public override bool CanMove(int x, int y, Tile tile)
		{
			if (Math.Abs(x) == Math.Abs(y))
			{
				return true;
			}
			return false;
		}

		#endregion

	}
}

