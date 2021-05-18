using Prophunt.Utils;
using Sandbox;

namespace Prophunt.Players
{
	internal partial class ProphuntPlayer
	{
		[Net]
		private Prop Target { get; set; }
		private Prop _Target;

		public override void Simulate( Client cl )
		{
			base.Simulate( cl );

			// Update active weapon
			if ( Input.ActiveChild != null )
			{
				ActiveChild = Input.ActiveChild;
			}

			SimulateActiveChild( cl, ActiveChild );

			if ( IsLocalPawn && Target != _Target )
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
				SimulateSpectator();
			}
			else
			{
				SimulatePlayer();
			}

			if ( Team == Team.Prop )
			{
				SimulateProp();
			}
			else if ( Team == Team.Seeker )
			{
				SimulateSeeker();
			}
		}

		private void SimulateSpectator()
		{
		}

		private void SimulatePlayer()
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

		private void SimulateProp()
		{
			if ( IsServer )
			{
				var tr = Trace.Ray( EyePos, EyePos + EyeRot.Forward * 85 )
					.UseHitboxes()
					.Ignore( this )
					.Run();

				if ( tr.Hit && tr.Body.IsValid() && tr.Entity is Sandbox.Prop && tr.Body.BodyType == PhysicsBodyType.Dynamic && !Config.BannedProps.Contains( (tr.Entity as Sandbox.Prop).GetModelName() ) )
				{
					Target = tr.Entity as Sandbox.Prop;
				}
				else
				{
					Target = null;
				}

				if ( TimeSinceLastTaunt > Config.TauntInterval )
				{
					Taunt();
				}
			}

			if ( Input.Pressed( InputButton.Attack2 ) && Animator is PropAnimator )
			{
				Locked = !Locked;
			}
		}

		private void SimulateSeeker()
		{
		}
	}
}
