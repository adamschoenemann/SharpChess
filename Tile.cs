using System;
using Gtk;
using Cairo;

namespace SharpChess
{
	public class Tile : DrawingArea
	{
		public Color Color
		{
			get;
			set;
		}

		public ChessPiece Piece
		{
			internal set;
			get;
		}

		public bool IsEmpty
		{
			get
			{
				return Piece == null;
			}
		}

		public bool Selected
		{
			get;
			set;
		}

		public Tile(Color color)
		{
			this.Color = color;
			this.AddEvents((int)Gdk.EventMask.ButtonPressMask);
		}

		protected override bool OnExposeEvent(Gdk.EventExpose evnt)
		{

			//Console.WriteLine("Tile exposed with area " + this.Allocation);
			//Console.WriteLine("WidthRequest: " + WidthRequest + " HeightRequest: " + HeightRequest);
			Gdk.Rectangle a = evnt.Area;

			using (Context ctx = Gdk.CairoHelper.Create((evnt.Window)))
			{
				if (Selected)
				{
					ctx.SetSourceRGBA(0.2, 0.2, 0.8, 1.0);
				}
				else
				{
					ctx.SetSourceRGBA(Color.R, Color.B, Color.G, Color.A);
				}
				ctx.Rectangle(new Rectangle(0, 0, WidthRequest, HeightRequest));
				ctx.Rectangle(new Rectangle(0, 0, a.Width, a.Height));

				ctx.Fill();
				if (Piece != null)
				{
					ctx.Save();
					Piece.Draw(ctx, a);
					ctx.Restore();
				}
			}

			return base.OnExposeEvent(evnt);
		}
	}
}

