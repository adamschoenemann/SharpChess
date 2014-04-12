using System;

namespace SharpChess
{
	public static class ExtensionMethods
	{
		public static int RoundToInt(this double val)
		{
			return (int)Math.Round(val, MidpointRounding.AwayFromZero);
		}
	}
}

