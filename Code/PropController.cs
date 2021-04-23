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

			//Rot = Rotation.LookAt( Input.Rot.Forward.WithZ( 0 ), Vector3.Up );
			Rot = Rotation.FromYaw( 90f );
			EyeHeight = Player.CollisionBounds.Maxs.z * 0.8f;

			//Player.WorldRot = Rot;
			//Log.Info( Rot.ToString() );
			//Player.WorldRot = Rotation.LookAt( Input.Rot.Forward.WithZ( 0 ), Vector3.Up );

			Log.Info( Player.WorldRot.ToString() );
			base.Tick();
			Log.Info( Player.WorldRot.ToString() );
		}
	}
}
