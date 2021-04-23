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
			RootPanel.StyleSheet.Load( "/ui/SandboxHud.scss" );
			RootPanel.AddChild<ChatBox>();
			RootPanel.AddChild<Health>();
			RootPanel.AddChild<VoiceList>();
			RootPanel.AddChild<KillFeed>();
			RootPanel.AddChild<Scoreboard<ScoreboardEntry>>();
			RootPanel.AddChild<CrosshairCanvas>();
			RootPanel.AddChild<InventoryBar>();

			// Our HUD elements
			RootPanel.StyleSheet.Load( "/ui/MainHud.scss" );
			RootPanel.AddChild<TeamName>();
			RootPanel.AddChild<LockedDisplay>();

			// Set the default crosshair
			CrosshairCanvas.SetCrosshair( new StandardCrosshair() );
		}
	}
}
