using System;
using SharpChess;

namespace SharpChess
{
	public class Rook : ChessPiece
	{
		public Rook(Player player) : base(player)
		{
		}

		#region implemented abstract members of ChessPiece

		public override bool CanMove(int x, int y, Tile tile)
		{
			if (x == 0 && y != 0)
			{
				return true;
			}
			if (y == 0 && x != 0)
			{
				return true;
			}
			return false;
		}

		public override void Die()
		{
			throw new NotImplementedException();
		}

		#endregion

	}
}

