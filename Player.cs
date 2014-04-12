using System;
using Gtk;
using Cairo;

namespace Chess
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

		public readonly PlayerType Type;

		public Player(PlayerType type, Color color)
		{
			this.Color = color;
			this.Type = type;
		}

		public bool IsWhite()
		{
			return (Type == PlayerType.White);
		}

		public bool IsBlack()
		{
			return (Type == PlayerType.Black);
		}
	}
}

