using Prophunt.Players;
using Prophunt.Utils;
using Sandbox;
using Sandbox.UI;
using Sandbox.UI.Construct;
using System;

namespace Prophunt.UI
{
	public class RoundInfo : Panel
	{
		private Label Timer;

		public RoundInfo()
		{
			Timer = Add.Label( "", "timer" );
		}
		public override void Tick()
		{
			if ( Player.Local is not ProphuntPlayer ) return;

			String timeText = "\x221E";
			if ( Game.Instance.Round.RoundLength != -1 )
			{
				timeText = ( Game.Instance.Round.RoundLength - Game.Instance.Round.TimeSinceRoundStart ).ToTimeString();
			}
			Timer.SetText( $"{Game.Instance.Round.Name} - {timeText}" );
		}
	}
}
