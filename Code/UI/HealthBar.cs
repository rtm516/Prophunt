using System;
using Prophunt.Players;
using Prophunt.Utils;
using Sandbox;
using Sandbox.UI;

namespace Prophunt.UI
{
	public class HealthBar : Panel
	{
		private Panel HealthBarCount;
		private float LastHealth;

		public HealthBar()
		{
			StyleSheet.Load( "/ui/HealthBar.scss" );
			HealthBarCount = Add.Panel( "HealthBarCount" );
		}

		public override void Tick()
		{
			if ( Local.Pawn is not ProphuntPlayer player ) return;

			SetClass( "PropLockOn", player.Locked && player.Team == Team.Prop );
			Style.Dirty();

			LastHealth = LastHealth.LerpTo( player.Health, Time.Delta * 4f );
			HealthBarCount.Style.Width = Length.Fraction( LastHealth / 100f );
			HealthBarCount.Style.Dirty();
		}
	}
}
