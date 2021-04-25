using Sandbox;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prophunt.Rounds
{
	class WarnupRound : BaseRound
	{
		public override void Tick()
		{
			base.Tick();

			if (TimeSinceRoundStart > Game.Instance.WarmupTime && Host.IsServer)
			{
				Game.Instance.ChangeRound( new GameRound() );
			}
		}
	}
}
