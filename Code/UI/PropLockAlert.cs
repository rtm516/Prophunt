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
			Add.Label( "Rotation Locked!", "Title" );
			Add.Label( "Press RMB to unlock", "Instruction" );
		}

		public override void Tick()
		{
			if ( Player.Local is not ProphuntPlayer player ) return;

			if ( player.Team != Team.Prop )
			{
				Style.Display = DisplayMode.None;
			}
			else
			{
				Style.Display = DisplayMode.Flex;
			}

			SetClass( "PropLockOn", player.Locked );

			Style.Dirty();
		}
	}
}
