using Prophunt.Players;
using Prophunt.Rounds;
using Prophunt.Utils;
using Sandbox;
using Sandbox.UI;

namespace Prophunt.UI
{
	public class SeekerBlackout : Panel
	{
		public SeekerBlackout()
		{
		}

		public override void Tick()
		{
			if ( Player.Local is not ProphuntPlayer player ) return;

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
