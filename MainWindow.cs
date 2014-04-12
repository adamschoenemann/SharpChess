using System;
using Gtk;
using Chess;

public partial class MainWindow: Gtk.Window
{
	public MainWindow() : base(Gtk.WindowType.Toplevel)
	{
		ChessBoard board = new ChessBoard();
		board.SetSizeRequest(300, 300);
		board.Initialize();
		this.Add(board);

		Build();
		this.SetSizeRequest(300, 300);
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
