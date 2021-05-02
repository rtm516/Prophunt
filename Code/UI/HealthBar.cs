using Prophunt.Players;
using Sandbox;
using Sandbox.UI;

namespace Prophunt.UI
{
	public class HealthBar : Panel
	{
		public Panel HealthBarCount;

		public HealthBar()
		{
			StyleSheet.Load( "/ui/HealthBar.scss" );
			HealthBarCount = Add.Panel( "HealthBarCount" );
		}

		public override void Tick()
		{
			if ( Player.Local is not ProphuntPlayer player ) return;

			SetClass( "PropLockOn", player.Locked );
			Style.Dirty();

			HealthBarCount.Style.Width = Length.Fraction( player.Health / 100f );
			HealthBarCount.Style.Dirty();
		}
	}
}
