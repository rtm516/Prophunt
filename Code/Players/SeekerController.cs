using Prophunt.Rounds;
using Sandbox;

namespace Prophunt.Players
{
	internal class SeekerController : WalkController
	{
		public override void Simulate()
		{
			if ( Game.Instance.Round is not WarnupRound )
			{
				base.Simulate();
			}
		}
	}
}
