using Sandbox;
using System;
using Prophunt.Util;

namespace Prophunt
{
	partial class ProphuntPlayer
	{
		[Net]
		private Prop Target { get; set; }
		private Prop _Target;

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
				if ( _Target.IsValid() )
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

				if ( tr.Hit && tr.Body.IsValid() && tr.Entity is Sandbox.Prop && tr.Body.BodyType == PhysicsBodyType.Dynamic && !Game.Instance.BannedProps.Contains( (tr.Entity as Sandbox.Prop).GetModelName() ) )
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
