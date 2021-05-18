using Prophunt.Players;
using Prophunt.Utils;
using Sandbox;
using Sandbox.UI;
using Sandbox.UI.Construct;

namespace Prophunt.UI
{
	public class PropLockAlert : Panel
	{
		public PropLockAlert()
		{
			StyleSheet.Load( "/ui/PropLockAlert.scss" );

			Add.Label( "Rotation Locked!", "Title" );
			Add.Label( "Press RMB to unlock", "Instruction" );
		}

		public override void Tick()
		{
			if ( Local.Pawn is not ProphuntPlayer player ) return;

			SetClass( "PropLockOn", player.Locked && player.Team == Team.Prop );

			Style.Dirty();
		}
	}
}
