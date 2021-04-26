﻿using Prophunt.Players;
using Prophunt.Utils;
using Sandbox;
using Sandbox.UI;
using Sandbox.UI.Construct;
using System;

namespace Prophunt.UI
{
	public class TauntTimer : Panel
	{
		private Label Label;

		public TauntTimer()
		{
			Label = Add.Label("", "value" );
		}
		public override void Tick()
		{
			var player = Player.Local as ProphuntPlayer;
			if ( player == null ) return;

			if ( player.Team != Team.Prop )
			{
				Style.Display = DisplayMode.None;
			}
			else
			{
				Style.Display = DisplayMode.Flex;
			}
			Style.Dirty();

			Label.SetText( (Config.TauntInterval - player.TimeSinceLastTaunt).ToTimeString() );
		}
	}
}