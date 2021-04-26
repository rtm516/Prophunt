using System;
using Prophunt.Utils;
using Sandbox;
using Prop = Prophunt.Entities.Prop;

namespace Prophunt.Players
{
	internal partial class ProphuntPlayer
	{
		internal void OnPropUse( Prop prop )
		{
			if ( !(Animator is PropAnimator) )
			{
				Animator = new PropAnimator();
			}

			if ( !(Controller is PropController) )
			{
				Controller = new PropController();
			}

			Model model = prop.GetModel();
			SetModel( model );

			// TODO: https://discord.com/channels/833983068468936704/834020807633535047/835298925556662312
			// a good way to do colliders for prophunt may be making it so that when you become a prop, your collider represents the smallest possible bounds the prop could have?

			float xyMax = (float)Math.Round( Math.Max( CollisionBounds.Maxs.x, CollisionBounds.Maxs.y ) );
			float xyMaxInverted = xyMax * -1;
			float height = (float)Math.Round( CollisionBounds.Maxs.z - CollisionBounds.Mins.z );

			(Controller as WalkController).SetBBox( new Vector3( xyMaxInverted, xyMaxInverted, 0 ), new Vector3( xyMax, xyMax, height ) );
		}

		public override void OnKilled()
		{
			Inventory.DeleteContents();
			UnDress();

			Controller = null;
			Camera = new SpectateRagdollCamera();

			EnableAllCollisions = false;
			EnableDrawing = false;

			Team = Team.Spectator;

			base.OnKilled();
		}
	}
}
