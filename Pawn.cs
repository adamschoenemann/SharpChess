using System;
using SharpChess;

namespace SharpChess
{
	public class Pawn : ChessPiece
	{
		private bool hasMoved;

		public Pawn(Player player) : base(player)
		{
			MoveEvent += OnMoveEvent; 
		}

		void OnMoveEvent(object sender, PieceMoveEventArgs e)
		{
			hasMoved = true;
			MoveEvent -= OnMoveEvent;
		}

		#region implemented abstract members of ChessPiece

		public override bool CanMove(int rows, int cols, Tile tile)
		{
			int direction = Player.IsBlack ? 1 : -1;
			if (tile.Piece == null)
			{
				if (cols != 0)
				{
					return false;
				}
				if (hasMoved == true && rows == direction)
				{
					return true;
				}
				if (hasMoved == false && (rows == direction || rows == 2 * direction))
				{
					return true;
				}
				return false;
			}
			else // attempting to take a piece
			{
				if (Math.Abs(cols) == 1 && rows == direction)
				{
					return true;
				}
				return false;
			}
		}

		#endregion

	}
}

