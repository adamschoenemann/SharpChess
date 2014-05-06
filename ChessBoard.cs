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
		private BoardIndex selected;
		public readonly ChessGame Game;

		public ChessBoard(ChessGame game)
		{
			tiles = new Tile[8, 8];
			selected = null;
			Game = game;
		}

		internal void PutPiece(int row, int col, ChessPiece piece)
		{
			tiles[row, col].Piece = piece;
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
					tiles[y, x] = new Tile(new Color(color, color, color));
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

			Game.SetupGame();
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
			if (piece.Player != Game.ActivePlayer)
			{
				Console.WriteLine("It is not your turn");
				return false;
			}

			ChessPiece target = tiles[toRow, toCol].Piece;
			// You cant move on top of your own pieces
			if (target != null && target.Player == piece.Player)
			{
				Console.WriteLine("You cant move on top of your own piece");
				return false;
			}

			// Test if the path is clear
			int rows = toRow - fromRow, cols = toCol - fromCol;
			if (IsPathClear(fromRow, fromCol, toRow, toCol) == false)
			{
				Console.WriteLine("Path is not clear for that move");
				return false;
			}

			// Test if the piece can do the move
			if (piece.CanMove(rows, cols, tiles[toRow, toCol]) == false)
			{
				Console.WriteLine("Illegal move for this piece");
				return false;
			}
			// If there is a piece where we will move, it should die
			if (tiles[toRow, toCol].IsEmpty == false)
			{
				tiles[toRow, toCol].Piece.OnTaken();
			}
			Console.WriteLine("Moving pieces");
			// Move the piece
			tiles[toRow, toCol].Piece = piece;
			// Empty previous tile
			tiles[fromRow, fromCol].Piece = null;	

			piece.SendMoveEvent(this, new PieceMoveEventArgs(fromRow, fromCol, toRow, toCol));
			Game.OnMove();

			return true; 

		}

		private bool IsPathClear(int fromRow, int fromCol, int toRow, int toCol)
		{
			int rows = toRow - fromRow, cols = toCol - fromCol;
			int pieces = 0;
			if (Math.Abs(rows) == Math.Abs(cols)) // diagonal move
			{
				for (int r = 0, c = 0;
					Math.Abs(r) < Math.Abs(rows) && Math.Abs(c) < Math.Abs(cols);
					 r += Math.Sign(rows), c += Math.Sign(cols))
				{
					if (tiles[fromRow + r, fromCol + c].IsEmpty == false)
						pieces++;
				}
				return (pieces <= 1);
			}
			if ((rows == 0 && Math.Abs(cols) > 0)) // straight move right/left
			{
				for (int c = 0; c < Math.Abs(cols); c++)
				{
					if (tiles[fromRow, fromCol + c * Math.Abs(cols)].IsEmpty == false)
						pieces++;
				}
				return (pieces <= 1);
			}
			if (cols == 0 && Math.Abs(rows) > 0) // straight move up/down
			{
				for (int r = 0; r < Math.Abs(rows); r++)
				{
					if (tiles[fromRow + r * Math.Sign(rows), fromCol].IsEmpty == false)
						pieces++;
				}
				return (pieces <= 1);
			}
			// Probably moving a knight
			return true;
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

