using Prophunt.Players;
using Prophunt.Utils;
using Sandbox;
using Sandbox.UI;
using Sandbox.UI.Construct;

namespace Prophunt.UI
{
	public class AdditionalInfoRightPanel : Panel
	{
		public Label Title;
		public Label Value;

		public AdditionalInfoRightPanel()
		{
			Title = Add.Label( "", "Title" );
			Value = Add.Label( "", "Value" );
		}

		public override void Tick()
		{
			if ( Player.Local is not ProphuntPlayer player ) return;

			if ( player.Team == Team.Prop )
			{
				Title.SetText( "Force Taunt" );
				Value.SetText( (Config.TauntInterval - player.TimeSinceLastTaunt).ToTimeString() );
			}
			else if ( player.Team == Team.Seeker )
			{
				if ( player.ActiveChild is not ProphuntWeapon weapon ) return;
				Title.SetText( weapon.AmmoClip.ToString() );
				Value.SetText( weapon.AvailableAmmoString() );
			}
		}
	}
}
