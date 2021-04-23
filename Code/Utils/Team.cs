using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prophunt.Util
{
	public enum Team
	{
		Prop,
		Seeker,
		Spectator
	}

	public static class Extensions
	{
		internal static string GetName( this Team team )
		{
			switch ( team )
			{
				case Team.Prop:
					return "Prop";

				case Team.Seeker:
					return "Seeker";

				case Team.Spectator:
					return "Spectator";

				default:
					return "";
			}
		}
	}
}
