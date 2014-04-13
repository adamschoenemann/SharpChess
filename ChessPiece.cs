using System;
using Gtk;
using Cairo;

namespace SharpChess
{
	public class PieceMoveEventArgs : EventArgs
	{
		public readonly BoardIndex Prev, New;

		public PieceMoveEventArgs(BoardIndex p, BoardIndex n)
		{
			Prev = p;
			New = n;
		}

		public PieceMoveEventArgs(int fromRow, int fromCol, int toRow, int toCol)
			: this(new BoardIndex(fromRow, fromCol), new BoardIndex(toRow, toCol))
		{

		}
	}

	public abstract class ChessPiece
	{
		public Player Player
		{
			get;
			protected set;
		}

		public ChessPiece(Player player)
		{
			this.Player = player;
		}

		public abstract bool CanMove(int rows, int cols, Tile tile);

		public virtual void OnTaken()
		{
			Player.OnPieceTaken(this);
		}

		public event EventHandler<PieceMoveEventArgs> MoveEvent;

		public void SendMoveEvent(object sender, PieceMoveEventArgs args)
		{
			if (MoveEvent != null)
			{
				MoveEvent(sender, args);
			}
		}

		public virtual void Draw(Context ctx, Gdk.Rectangle area)
		{
			ImageSurface icon;
			try
			{
				icon = new ImageSurface(FileName);
				float w_scale = (float)area.Width / (float)icon.Width;
				float h_scale = (float)area.Height / (float)icon.Height;

				ctx.Scale(w_scale, h_scale);
				ctx.SetSource(new SurfacePattern(icon));

				ctx.Paint();
			}
			catch (Exception e)
			{
				Console.WriteLine(e.Message);
			}
		}

		public virtual string FileName
		{
			get
			{
				return String.Format("Assets/{0}-{1}-32.png", Name, Player.IsBlack ? "black" : "white");
			}
		}

		public virtual string Name
		{
			get
			{
				return this.GetType().Name.ToLowerInvariant();
			}
		}
	}
}

