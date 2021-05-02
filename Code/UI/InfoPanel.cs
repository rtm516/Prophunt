using Prophunt.Players;
using Prophunt.Utils;
using Sandbox;
using Sandbox.UI;

namespace Prophunt.UI
{
	public class InfoPanel : Panel
	{
		public InfoPanel()
		{
			StyleSheet.Load( "/ui/InfoPanel.scss" );

			Panel panel = Add.Panel( "InfoPanelDisplay" );
			panel.AddChild<PropLockAlert>();
			panel.AddChild<HealthBar>();
			panel.AddChild<AdditionalInfoPanel>();
		}

		public override void Tick()
		{
			base.Tick();

			if ( Player.Local is not ProphuntPlayer player ) return;

			if ( player.Team == Team.Spectator )
			{
				Style.Display = DisplayMode.None;
			}
			else
			{
				Style.Display = DisplayMode.Flex;
			}
			Style.Dirty();
		}
	}
}
