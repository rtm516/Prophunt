using Sandbox;

namespace Prophunt.Players
{
	internal class PropAnimator : StandardPlayerAnimator
	{
		private Rotation LastRotation;

		public override void DoRotation( Rotation idealRotation )
		{
			if ( !(Pawn as ProphuntPlayer).Locked )
			{
				LastRotation = idealRotation;
			}

			Rotation = LastRotation;
		}
	}
}
