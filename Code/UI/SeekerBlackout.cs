using Prophunt.Rounds;
using Prophunt.Util;
using Sandbox;
using Sandbox.UI;
using Sandbox.UI.Construct;
using System;

namespace Prophunt.UI
{
	public class SeekerBlackout : Panel
	{
		public SeekerBlackout()
		{
		}
		public override void Tick()
		{
			var player = Player.Local as ProphuntPlayer;
			if ( player == null ) return;

			if ( player.Team == Team.Seeker && Game.Instance.Round is WarnupRound )
			{
				Style.Display = DisplayMode.Flex;
			}
			else
			{
				Style.Display = DisplayMode.None;
			}
			Style.Dirty();
		}
	}
}
