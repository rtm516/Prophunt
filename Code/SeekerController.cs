using Prophunt.Rounds;
using Sandbox;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prophunt
{
	class SeekerController : WalkController
	{
		public override void Tick()
		{
			if ( Game.Instance.Round is not WarnupRound )
			{
				base.Tick();
			}
		}
	}
}
