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
			if ( Local.Pawn is not ProphuntPlayer player ) return;

			Title.SetText( "Points" );
			Value.SetText( player.Points.ToString() );
		}
	}
}
