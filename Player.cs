using System;
using Gtk;
using Cairo;

namespace SharpChess
{
	public enum PlayerType
	{
		White,
		Black
	}

	public class Player
	{
		public Color Color
		{
			get;
			private set;
		}

		public int Pieces
		{
			get;
			private set;
		}

		public readonly PlayerType Type;
		internal ChessGame game;

		public Player(PlayerType type, Color color)
		{
			this.Color = color;
			this.Type = type;
			this.Pieces = 16;
		}

		public bool IsWhite
		{
			get
			{
				return (Type == PlayerType.White);
			}
		}

		public bool IsBlack
		{
			get
			{
				return (Type == PlayerType.Black);
			}
		}

		public void OnPieceTaken(ChessPiece chessPiece)
		{
			Pieces--;
			Console.WriteLine("Piece taken from " + Type.ToString() + " player");
			if (chessPiece is Pawn)
			{
				Console.WriteLine("Ohh, it was just a pawn");
			}
			else if (chessPiece is King)
			{
				Console.WriteLine("Damn, we lost");
				game.Lose(this);
			}
		}
	}
}

