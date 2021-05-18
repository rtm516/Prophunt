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

		public virtual void ClientKilled( Client client ) { }

		public virtual void ClientJoined( Client client ) { }

		public virtual void ClientDisconnected( Client client, NetworkDisconnectionReason reason ) { }
	}
}
