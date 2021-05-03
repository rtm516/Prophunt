using Sandbox;

namespace prophunt.Weapons
{
	public class Grenade : Prop
	{
		public override void Spawn()
		{
			base.Spawn();

			SetModel( "models/citizen_props/sodacan01.vmdl" );
			SetupPhysicsFromModel( PhysicsMotionType.Dynamic, false );
		}

		public override void Touch( Entity other )
		{
			base.Touch( other );
			
			// TODO: Explode

			Delete();
		}
	}
}
