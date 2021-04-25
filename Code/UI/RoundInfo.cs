using Prophunt.Util;
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
			var player = Player.Local as ProphuntPlayer;
			if ( player == null ) return;

			String timeText = "\x221E";
			if ( Game.Instance.Round.RoundLength != -1 )
			{
				int timeLeft = (int)Math.Ceiling( Game.Instance.Round.RoundLength - Game.Instance.Round.TimeSinceRoundStart );
				timeText = ((int)Math.Floor( timeLeft / 60f )).ToString().PadLeft( 2, '0' ) + ":" + (timeLeft % 60).ToString().PadLeft( 2, '0' );
			}
			Timer.SetText( $"{Game.Instance.Round.Name} - {timeText}" );
		}
	}
}
