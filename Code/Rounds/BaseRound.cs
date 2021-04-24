using Sandbox;

namespace Prophunt.Rounds
{
	public abstract class BaseRound : NetworkClass
	{
		public TimeSince TimeSinceRoundStart;

		public BaseRound() { }

		public virtual void Start()
		{
			TimeSinceRoundStart = 0;
		}

		public virtual void Finish() { }

		public virtual void Tick() { }

		public virtual void PlayerKilled( Player player ) { }
	}
}
