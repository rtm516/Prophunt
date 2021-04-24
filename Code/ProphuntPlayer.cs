using Prophunt;
using Prophunt.Entities;
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

		[Net]
		private Sandbox.Prop Target { get; set; }
		private Sandbox.Prop _Target;

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

		internal void OnPropUse( Entities.Prop prop )
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
			if ( Team == Team.Seeker )
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

			if ( IsLocalPlayer && Target != _Target )
			{
				if ( _Target.IsValid() )
				{
					_Target.GlowActive = false;
				}
				_Target = Target;
				if ( _Target .IsValid() )
				{
					_Target.GlowActive = true;
					_Target.GlowColor = Color.Green;
				}
			}

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
			if ( IsServer )
			{
				var tr = Trace.Ray( EyePos, EyePos + EyeRot.Forward * 100f )
					.UseHitboxes()
					.Ignore( this )
					.Run();

				if ( tr.Hit && tr.Body.IsValid() && tr.Entity is Sandbox.Prop && tr.Body.BodyType == PhysicsBodyType.Dynamic )
				{
					Target = tr.Entity as Sandbox.Prop;
				}
				else
				{
					Target = null;
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
