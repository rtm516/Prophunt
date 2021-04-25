using Sandbox;

namespace Prophunt.Rounds
{
	public abstract class BaseRound : NetworkClass
	{
		public TimeSince TimeSinceRoundStart;
		public int RoundLength;
		public abstract string Name { get; }

		public BaseRound()
		{
			RoundLength = -1;
		}

		public virtual void Start()
		{
			TimeSinceRoundStart = 0;
		}

		public virtual void Finish() { }

		public virtual void Tick() { }

		public virtual void PlayerKilled( Player player ) { }

		public virtual void PlayerJoined( Player player ) { }

		public virtual void PlayerDisconnected( Player player, NetworkDisconnectionReason reason ) { }
	}
}
