using Sandbox;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prophunt.Rounds
{
	public abstract class BaseRound : NetworkClass
	{
		public TimeSince TimeSinceRoundStart;

		public virtual void Start()
		{
			TimeSinceRoundStart = 0;
		}

		public virtual void Finish() { }

		public virtual void Tick() { }

		public virtual void PlayerKilled( Player player ) { }
	}
}
