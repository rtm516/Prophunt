﻿using System.Collections.Generic;

namespace Prophunt
{
	internal static class Config
	{
		public static float SeekerPct = 0.1f;

		public static int MinPlayers = 2;

		public static int WarmupRoundLength = 30;
		public static int GameRoundLength = 60 * 5;
		public static int PostGameRoundLength = 10;

		public static int TauntInterval = 5;

		public static List<string> BannedProps = new List<string> {
			"models/citizen_props/hotdog01.vmdl",
			"models/citizen_props/newspaper01.vmdl"
		};

		public static List<string> Taunts = new List<string> {
			"sounds/footsteps/footstep-concrete.sound",
			"sounds/footsteps/footstep-metal.sound",
			"sounds/footsteps/footstep-wood.sound"
		};
	}
}
