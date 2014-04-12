using System;
using Gtk;

namespace SharpChess
{
	class MainClass
	{
		public static void Main(string[] args)
		{
			Application.Init();
			MainWindow win = MainWindow.Instance;
			win.Show();
			Application.Run();
		}
	}
}
