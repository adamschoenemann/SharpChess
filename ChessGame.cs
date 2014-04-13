using System;

namespace SharpChess
{
	public class ChessGame
	{
		private Player[] players;

		public Player ActivePlayer
		{
			get;
			private set;
		}

		public Player White
		{
			get { return players[0]; }
		}

		public Player Black
		{
			get { return players[1]; }
		}

		int Rounds
		{
			get;
			set;
		}

		public readonly ChessBoard Board;

		public ChessGame(Player white, Player black)
		{
			players = new Player[2]{ white, black };
			white.game = black.game = this;
			ActivePlayer = white;
			Rounds = 0;
			Board = new ChessBoard(this);
		}

		public void OnMove()
		{
			SwitchPlayer();
			Rounds++;
		}

		public void SetupGame()
		{
			// pawns
			for (int i = 0; i < 8; i++)
			{
				Board.PutPiece(1, i, new Pawn(players[1]));
				Board.PutPiece(6, i, new Pawn(players[0]));
			}

			// the rest
			Board.PutPiece(0, 0, new Rook(players[1]));
			Board.PutPiece(0, 1, new Knight(players[1]));
			Board.PutPiece(0, 2, new Bishop(players[1]));
			Board.PutPiece(0, 3, new King(players[1]));
			Board.PutPiece(0, 4, new Queen(players[1]));
			Board.PutPiece(0, 5, new Bishop(players[1]));
			Board.PutPiece(0, 6, new Knight(players[1]));
			Board.PutPiece(0, 7, new Rook(players[1]));

			Board.PutPiece(7, 0, new Rook(players[0]));
			Board.PutPiece(7, 1, new Knight(players[0]));
			Board.PutPiece(7, 2, new Bishop(players[0]));
			Board.PutPiece(7, 3, new King(players[0]));
			Board.PutPiece(7, 4, new Queen(players[0]));
			Board.PutPiece(7, 5, new Bishop(players[0]));
			Board.PutPiece(7, 6, new Knight(players[0]));
			Board.PutPiece(7, 7, new Rook(players[0]));
		}

		internal void Win(Player player)
		{
			Console.WriteLine("Player " + player.Type + " wins the game!");
		}

		public void Lose(Player player)
		{
			Console.WriteLine("Player " + player.Type + " loses the game!");
		}

		private void SwitchPlayer()
		{
			ActivePlayer = (ActivePlayer == players[0]) ? players[1] : players[0];
		}
	}
}

