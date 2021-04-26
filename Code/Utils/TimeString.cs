using System;

namespace Prophunt.Utils
{
	public static class TimeString
	{
		internal static string ToTimeString( this int seconds )
		{
			return ((int)Math.Floor( seconds / 60f )).ToString().PadLeft( 2, '0' ) + ":" +
			       (seconds % 60).ToString().PadLeft( 2, '0' );
		}

		internal static string ToTimeString( this float seconds )
		{
			return ((int)Math.Ceiling( seconds )).ToTimeString();
		}
	}
}
