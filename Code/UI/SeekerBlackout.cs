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
			StyleSheet.Load( "/ui/SeekerBlackout.scss" );
		}

		public override void Tick()
		{
			if ( Local.Pawn is not ProphuntPlayer player ) return;

			SetClass( "active", player.Team == Team.Seeker && Game.Instance.Round is WarnupRound );
		}
	}
}
