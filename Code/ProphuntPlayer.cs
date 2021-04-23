using Prophunt;
using Prophunt.Util;
using Sandbox;
using System;

namespace Prophunt
{
	partial class ProphuntPlayer : BasePlayer
	{
		[Net]
		public Team Team { get; set; }
		[Net]
		public bool Locked { get; set; }

		public ProphuntPlayer()
		{
			Log.Info( "Prophunt Player" );
			Inventory = new BaseInventory( this );
		}

		public override void Spawn()
		{
			base.Spawn();

			Team = Rand.Int( 1 ) == 0 ? Team.Seeker : Team.Prop;
		}

		public override void Respawn()
		{
			SetModel( "models/citizen/citizen.vmdl" );

			Controller = Team == Team.Spectator ? new NoclipController() : new WalkController();
			Animator = new StandardPlayerAnimator();
			Camera = new FirstPersonCamera();

			EnableAllCollisions = Team != Team.Spectator;
			EnableDrawing = Team != Team.Spectator;
			EnableHideInFirstPerson = true;
			EnableShadowInFirstPerson = true;

			Health = 100;
			LifeState = LifeState.Alive;

			Inventory.DeleteContents();
			if (Team == Team.Seeker)
			{
				Inventory.Add( new Pistol(), true );
			}

			base.Respawn();
		}

		public override void OnKilled()
		{
			Inventory.DeleteContents();
			
			Controller = null;
			Camera = new SpectateRagdollCamera();

			EnableAllCollisions = false;
			EnableDrawing = false;

			Team = Team.Spectator;
			
			base.OnKilled();
		}

		protected override void Tick()
		{
			base.Tick();

			if ( Team == Team.Spectator )
			{
				TickSpectator();
			}
			else
			{
				TickPlayer();
			}

			if ( Team == Team.Prop )
			{
				TickProp();
			}
			else if ( Team == Team.Seeker )
			{
				TickSeeker();
			}
		}

		private void TickSpectator()
		{

		}

		private void TickPlayer()
		{
			if ( Input.Pressed( InputButton.View ) )
			{
				if ( !(Camera is FirstPersonCamera) )
				{
					Camera = new FirstPersonCamera();
				}
				else
				{
					Camera = new ThirdPersonCamera();
				}
			}

			if ( LifeState == LifeState.Alive )
			{
				TickPlayerUse();
			}
		}

		private void TickProp()
		{
			if ( Input.Pressed( InputButton.Use ) )
			{
				var tr = Trace.Ray( EyePos, EyePos + EyeRot.Forward * 100f )
					.UseHitboxes()
					.Ignore( this )
					.Run();

				if ( tr.Hit && tr.Body.IsValid() && tr.Entity is Prop && tr.Body.BodyType == PhysicsBodyType.Dynamic )
				{
					if ( !(Animator is PropAnimator) )
					{
						Animator = new PropAnimator();
					}

					if ( !(Controller is PropController) )
					{
						Controller = new PropController();
					}

					Log.Info( CollisionBounds.Mins.ToString() + "\t" + CollisionBounds.Maxs.ToString() );

					Model model = (tr.Entity as Prop).GetModel();
					SetModel( model );

					(Controller as WalkController).SetBBox( CollisionBounds.Mins, CollisionBounds.Maxs );

					//Log.Info( CollisionBounds.Mins.ToString() + "\t" + CollisionBounds.Maxs.ToString() );

					//return;
					// Update the collision bounds
					//Vector3 size = (tr.Entity as Prop).CollisionBounds.Size;
					//WalkController walkController = Controller as WalkController;
					//walkController.BodyGirth = Math.Max( size.x, size.y );
					//walkController.BodyHeight = size.z;
					//walkController.UpdateBBox();

					// Force the physics to update
					//this.PhysicsBody?.Wake();
					//UpdatePhysicsHull();
					this.SetupPhysicsFromModel( PhysicsMotionType.Static );
				}
			}

			if ( Input.Pressed( InputButton.Attack2 ) && Animator is PropAnimator )
			{
				Locked = !Locked;
			}
		}

		private void TickSeeker()
		{

		}
	}
}
