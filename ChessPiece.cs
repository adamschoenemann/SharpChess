using System;
using Gtk;
using Cairo;

namespace Chess
{
	public abstract class ChessPiece
	{
		public Player Player
		{
			get;
			private set;
		}

		public ChessPiece(Player player)
		{
			this.Player = player;
		}

		public abstract bool CanMove(int x, int y, Tile tile);

		public abstract void Die();
	}
}

