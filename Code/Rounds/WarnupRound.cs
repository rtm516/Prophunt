using Prophunt.Utils;
using Sandbox;

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

			if ( TimeSinceRoundStart > RoundLength && Host.IsServer )
			{
				Game.Instance.ChangeRound( new GameRound() );
			}
		}
	}
}
