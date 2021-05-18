using Sandbox;

namespace Prophunt.Players
{
	internal class PropController : WalkController
	{
		public override BBox GetHull()
		{
			return new(mins, maxs);
		}

		public override void UpdateBBox()
		{
			// Dont do anything
		}

		public override void Simulate()
		{
			EyeHeight = (Pawn as AnimEntity).CollisionBounds.Maxs.z * 0.8f;

			base.Simulate();
		}
	}
}
