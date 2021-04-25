using Sandbox;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prophunt.Rounds
{
	public partial class WarnupRound : BaseRound
	{
		public override string Name { get => "Warmup"; }

		public WarnupRound() : base()
		{
			RoundLength = Config.WarmupRoundLength;
		}

		public override void Tick()
		{
			base.Tick();

			if (TimeSinceRoundStart > RoundLength && Host.IsServer)
			{
				Game.Instance.ChangeRound( new GameRound() );
			}
		}
	}
}
