using System;
using Gtk;
using Cairo;

namespace SharpChess
{
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

		public abstract bool CanMove(int x, int y, Tile tile);

		public abstract void Die();

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

