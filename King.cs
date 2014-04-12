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

		public override bool CanMove(int x, int y, Tile tile)
		{
			return true;
		}

		public override void Die()
		{
			//throw new NotImplementedException();
		}

		#endregion

	}
}
