using System.Collections.Generic;

namespace Prophunt.Utils
{
	internal static class Config
	{
		public static float SeekerPct = 0.25f;

		public static int MinPlayers = 2;

		public static int WarmupRoundLength = 30;
		public static int GameRoundLength = 60 * 5;
		public static int PostGameRoundLength = 10;

		public static int TauntInterval = 30;

		public static List<string> BannedProps = new()
		{
			"models/citizen_props/hotdog01.vmdl",
			"models/citizen_props/newspaper01.vmdl"
		};

		public static List<string> Taunts = new()
		{
			"firework",

			// Ported from the original
			"boom_headshot",
			"doh",
			"go_away_or_i_shall",
			"ill_be_back",
			"negative",
			"oh_yea_he_will_pay",
			"ok_i_will_tell_you",
			"please_come_again",
			"threat_neutralized",
			"what_is_wrong_with_you",
			"woohoo",
			"you_dont_know_the_power",
			"you_underestimate_the_power"
		};
	}
}
