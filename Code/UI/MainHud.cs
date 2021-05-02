using Sandbox;
using Sandbox.UI;

namespace Prophunt.UI
{
	[Library]
	public partial class MainHud : Hud
	{
		public MainHud()
		{
			if ( !IsClient ) return;

			// Default HUD elements
			RootPanel.StyleSheet.Load( "/ui/external/SandboxHud.scss" );
			RootPanel.AddChild<ChatBox>();
			RootPanel.AddChild<VoiceList>();
			RootPanel.AddChild<KillFeed>();
			RootPanel.AddChild<Scoreboard<ScoreboardEntry>>();
			RootPanel.AddChild<CrosshairCanvas>();
			//RootPanel.AddChild<InventoryBar>();

			// Our HUD elements
			RootPanel.AddChild<SeekerBlackout>();
			RootPanel.AddChild<PlayerCount>();
			RootPanel.AddChild<PropLockOutline>();
			RootPanel.AddChild<Timer>();
			RootPanel.AddChild<InfoPanel>();

			// Set the default crosshair
			CrosshairCanvas.SetCrosshair( new StandardCrosshair() );
		}
	}
}
