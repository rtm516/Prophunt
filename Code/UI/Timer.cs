using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prophunt;
using Prophunt.Utils;
using Sandbox.UI;
using Sandbox.UI.Construct;

namespace Prophunt.UI
{
	public class Timer : Panel
	{
		private Label Time;
		private Label Status;

		public Timer()
		{
			Time = Add.Label( "", "Time" );
			Status = Add.Label( "", "Status" );
		}

		public override void Tick()
		{
			String timeText = "\x221E";
			if ( Game.Instance.Round.RoundLength != -1 )
			{
				timeText = (Game.Instance.Round.RoundLength - Game.Instance.Round.TimeSinceRoundStart).ToTimeString();
			}
			Time.SetText( timeText );
			Status.SetText( Game.Instance.Round.Name );
		}
	}
}
