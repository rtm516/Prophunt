using Prophunt.Players;
using Prophunt.Utils;
using Sandbox;
using Sandbox.UI;
using Sandbox.UI.Construct;

namespace Prophunt.UI
{
	public class AdditionalInfoLeftPanel : Panel
	{
		public Label Title;
		public Label Value;

		public AdditionalInfoLeftPanel()
		{
			Title = Add.Label( "", "Title" );
			Value = Add.Label( "", "Value" );
		}

		public override void Tick()
		{
			if ( Player.Local is not ProphuntPlayer player ) return;

			if ( player.Team == Team.Prop )
			{
				Title.SetText( "Points" );
				Value.SetText( "000" );
			}
			else if ( player.Team == Team.Seeker )
			{
				Title.SetText( "???" );
				Value.SetText( "???" );
			}
		}
	}
}
