using System;
using Gtk;
using Cairo;
using Glade;

namespace SharpChess
{
	public class BoardIndex
	{
		public int Row, Col;

		public BoardIndex(int r, int c)
		{
			Row = r;
			Col = c;
		}
	}

	public class ChessBoard : Fixed
	{
		private Tile[,] tiles;
		private Player[] players;
		private BoardIndex selected;

		public Player ActivePlayer
		{
			get;
			private set;
		}

		public ChessBoard()
		{
			tiles = new Tile[8, 8];
			selected = null;
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
					double color = (y + x) % 2 == 0 ? 0.8 : 0.2;
					int w = (this.WidthRequest / 8.0).RoundToInt();
					int h = (this.HeightRequest / 8.0).RoundToInt();
					tiles[y, x] = new Tile(new Color(color, color, color), w, h);
					tiles[y, x].SetSizeRequest(w, h);
					tiles[y, x].ButtonPressEvent += (object o, ButtonPressEventArgs args) =>
					{
						Tile tile = o as Tile;
						Console.WriteLine("Tile at (" + x + "," + y + ") pressed");
						if (selected == null)
						{
							tile.Selected = true;
							selected = new BoardIndex(y, x);
						}
						else
						{
							Move(selected, new BoardIndex(y, x));
							tiles[selected.Row, selected.Col].Selected = false;
							selected = null;
						}
						MainWindow.Instance.QueueDraw();


					};
					this.Put(tiles[y, x], w * x, h * y);
				}
			}

			ActivePlayer = players[0];
			SetupGame();
		}

		private void SetupGame()
		{
			// pawns
			for (int i = 0; i < 8; i++)
			{
				tiles[1, i].Piece = new Pawn(players[1]);
				tiles[6, i].Piece = new Pawn(players[0]);
			}

			// the rest
			tiles[0, 0].Piece = new Rook(players[1]);
			tiles[0, 1].Piece = new Knight(players[1]);
			tiles[0, 2].Piece = new Bishop(players[1]);
			tiles[0, 3].Piece = new King(players[1]);
			tiles[0, 4].Piece = new Queen(players[1]);
			tiles[0, 5].Piece = new Bishop(players[1]);
			tiles[0, 6].Piece = new Knight(players[1]);
			tiles[0, 7].Piece = new Rook(players[1]);

			tiles[7, 0].Piece = new Rook(players[0]);
			tiles[7, 1].Piece = new Knight(players[0]);
			tiles[7, 2].Piece = new Bishop(players[0]);
			tiles[7, 3].Piece = new King(players[0]);
			tiles[7, 4].Piece = new Queen(players[0]);
			tiles[7, 5].Piece = new Bishop(players[0]);
			tiles[7, 6].Piece = new Knight(players[0]);
			tiles[7, 7].Piece = new Rook(players[0]);



		}

		public bool Move(BoardIndex from, BoardIndex to)
		{
			return Move(from.Row, from.Col, to.Row, to.Col);
		}

		public bool Move(int fromRow, int fromCol, int toRow, int toCol)
		{
			if (fromRow == toRow && fromCol == toCol)
			{
				Console.WriteLine("You can't make a no-move");
				return false;
			}
			// If trying to move from empty tile
			if (tiles[fromRow, fromCol].IsEmpty)
			{
				Console.WriteLine("Cannot move from empty tile");
				return false;
			}
			ChessPiece piece = tiles[fromRow, fromCol].Piece;
			// Check if we're trying to move a piece that is not "ours"
			if (piece.Player != ActivePlayer)
			{
				Console.WriteLine("It is not your turn");
				return false;
			}
			// Test if the piece can do the move
			if (piece.CanMove(toRow - fromRow, toCol - fromCol, tiles[toRow, toCol]) == false)
			{
				Console.WriteLine("Illegal move for this piece");
				return false;
			}
			// If there is a piece where we will move, it should die
			if (tiles[toRow, toCol].IsEmpty == false)
			{
				tiles[toRow, toCol].Piece.Die();
			}
			Console.WriteLine("Moving pieces");
			// Move the piece
			tiles[toRow, toCol].Piece = piece;
			// Empty previous tile
			tiles[fromRow, fromCol].Piece = null;

			piece.SendMoveEvent(this, new PieceMoveEventArgs(fromRow, fromCol, toRow, toCol));

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

