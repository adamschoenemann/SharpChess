using System;
using Gtk;
using SharpChess;
using Cairo;

public partial class MainWindow: Gtk.Window
{
	private static MainWindow instance;

	public static MainWindow Instance
	{
		get
		{
			if (instance == null)
			{
				instance = new MainWindow();
			}
			return instance;
		}
	}

	private MainWindow() : base(Gtk.WindowType.Toplevel)
	{
		ChessGame game = new ChessGame(
			                 new Player(PlayerType.White, new Color(1, 1, 1)),
			                 new Player(PlayerType.Black, new Color(0, 0, 0))
		                 );

		ChessBoard board = game.Board;
		this.SetSizeRequest(300, 300);
		board.SetSizeRequest(300, 300);
		board.Initialize();
		this.Add(board);

		Build();

		this.Resizable = false;
	}

	protected void OnDeleteEvent(object sender, DeleteEventArgs a)
	{
		Application.Quit();
		a.RetVal = true;
	}

	protected override bool OnExposeEvent(Gdk.EventExpose evnt)
	{
		using (Cairo.Context ctx = Gdk.CairoHelper.Create(evnt.Window))
		{
			ctx.SetSourceRGB(0, 0, 0);
			ctx.MoveTo(0, 0);
			ctx.LineTo(50, 50);
		}
		return base.OnExposeEvent(evnt);
	}

	protected override bool OnKeyPressEvent(Gdk.EventKey evnt)
	{
		if (evnt.Key == Gdk.Key.Escape)
		{
			Application.Quit();
			return false;
		}
		return true;
	}
}
