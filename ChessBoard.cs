using System;
using Gtk;
using Cairo;
using Glade;

namespace Chess
{
	public class ChessBoard : Fixed
	{
		private Tile[,] tiles;
		private Player[] players;

		public Player ActivePlayer
		{
			get;
			private set;
		}

		public ChessBoard()
		{
			tiles = new Tile[8, 8];
			players = new Player[2]
			{ 
				new Player(PlayerType.White, new Color(1, 1, 1)),
				new Player(PlayerType.Black, new Color(0, 0, 0))
			};


		}

		public void Initialize()
		{
			for (int row = 0; row < tiles.GetLength(0); row++)
			{
				for (int col = 0; col < tiles.GetLength(1); col++)
				{
					int y = row, x = col;
					double color = (y + x) % 2 == 0 ? 0.8 : 0.0;
					int w = (this.WidthRequest / 8.0).RoundToInt();
					int h = (this.HeightRequest / 8.0).RoundToInt();
					tiles[y, x] = new Tile(new Color(color, color, color), w, h);
					tiles[y, x].SetSizeRequest(w, h);
					tiles[y, x].ButtonPressEvent += (object o, ButtonPressEventArgs args) =>
					{
						Console.WriteLine("X : {0}, Y: {1}", x, y);
					};
					this.Put(tiles[y, x], w * x, h * y);
				}
			}

			ActivePlayer = players[0];
		}

		public bool Move(int fromRow, int fromCol, int toRow, int toCol)
		{
			// If trying to move from empty tile
			if (tiles[fromRow, fromCol].IsEmpty)
			{
				return false;
			}
			ChessPiece piece = tiles[fromRow, fromCol].Piece;
			// Check if we're trying to move a piece that is not "ours"
			if (piece.Player != ActivePlayer)
			{
				return false;
			}
			// Test if the piece can do the move
			if (piece.CanMove(toRow - fromRow, toCol - fromCol, tiles[toRow, toCol]) == false)
			{
				return false;
			}
			// If there is a piece where we will move, it should die
			if (tiles[toRow, toCol].IsEmpty == false)
			{
				tiles[toRow, toCol].Piece.Die();
			}
			// Move the piece
			tiles[toRow, toCol].Piece = tiles[fromRow, fromCol].Piece;
			// Empty previous tile
			tiles[fromRow, fromCol].Piece = null;

			SwitchPlayer();
			return true; 

		}

		private void SwitchPlayer()
		{
			ActivePlayer = (ActivePlayer == players[0]) ? players[1] : players[0];
		}

		protected override bool OnExposeEvent(Gdk.EventExpose evnt)
		{
			Console.WriteLine("ChessBoard Exposed with area " + evnt.Area);
			using (Context ctx = Gdk.CairoHelper.Create(evnt.Window))
			{
				ctx.SetSourceRGB(0, 0, 0);
				ctx.MoveTo(0, 0);
				ctx.LineTo(50, 50);
				//ctx.Stroke();
			}
			for (int y = 0; y < tiles.GetLength(0); y++)
			{
				for (int x = 0; x < tiles.GetLength(1); x++)
				{

				}
			}
			return base.OnExposeEvent(evnt);
		}
	}
}
