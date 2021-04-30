using Prophunt.Players;
using Prophunt.Utils;
using Sandbox;
using Sandbox.UI;
using Sandbox.UI.Construct;

namespace Prophunt.UI.External
{
	public class Health : Panel
	{
		public Label Label;

		public Health()
		{
			Add.Label( "🩸", "icon" );
			Label = Add.Label( "100", "value" );
		}

		public override void Tick()
		{
			if ( Player.Local is not ProphuntPlayer player ) return;

			Label.Text = $"{player.Health:n0}";
		}
	}
}
