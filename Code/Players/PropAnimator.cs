using Sandbox;

namespace Prophunt.Players
{
	internal class PropAnimator : StandardPlayerAnimator
	{
		private Rotation LastRotation;

		public override void DoRotation( Rotation idealRotation )
		{
			if ( !(Player as ProphuntPlayer).Locked )
			{
				LastRotation = idealRotation;
			}

			Rot = LastRotation;
		}
	}
}
