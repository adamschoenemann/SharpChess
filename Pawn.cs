using System;
using SharpChess;

namespace SharpChess
{
	public class Pawn : ChessPiece
	{
		public Pawn(Player player) : base(player)
		{
		}

		#region implemented abstract members of ChessPiece

		public override bool CanMove(int x, int y, Tile tile)
		{
			throw new NotImplementedException();
		}

		public override void Die()
		{
			throw new NotImplementedException();
		}

		#endregion

	}
}

