using System;
using Gtk;
using Cairo;
using System.Security.Principal;

namespace SharpChess
{
	public class King : ChessPiece
	{
		public King(Player player) : base(player)
		{

		}

		#region implemented abstract members of ChessPiece

		public override bool CanMove(int rows, int cols, Tile tile)
		{
			if (Math.Abs(rows) > 1 || Math.Abs(cols) > 1)
			{
				return false;
			}
			return true;
		}

		#endregion

	}
}
