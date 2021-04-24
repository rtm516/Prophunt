using Sandbox;

namespace Prophunt
{
	class PropController : WalkController
	{
		public override BBox GetHull()
		{
			return new BBox(mins, maxs);
		}

		public override void UpdateBBox()
		{
			// Dont do anything
		}

		public override void Tick()
		{
			EyeHeight = Player.CollisionBounds.Maxs.z * 0.8f;

			base.Tick();
		}
	}
}
