using Sandbox;
using System;

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
			Explode();

			Delete();
		}

		private void Explode()
		{
			// TODO: Rework this effect and damage
			var explosionBehavior = new ModelExplosionBehavior()
			{
				Sound = "",
				Effect = "particles/prophunt/explosion.vpcf",
				Damage = 100f,
				Radius = 100f,
				Force = 20f
			};

			if ( !string.IsNullOrWhiteSpace( explosionBehavior.Sound ) )
			{
				Sound.FromWorld( explosionBehavior.Sound, PhysicsBody.MassCenter );
			}

			if ( !string.IsNullOrWhiteSpace( explosionBehavior.Effect ) )
			{
				Particles.Create( explosionBehavior.Effect, PhysicsBody.MassCenter );
			}

			if ( explosionBehavior.Radius > 0.0f )
			{
				var sourcePos = PhysicsBody.MassCenter;
				var overlaps = Physics.GetEntitiesInSphere( sourcePos, explosionBehavior.Radius );

				foreach ( var overlap in overlaps )
				{
					if ( overlap is not ModelEntity ent || !ent.IsValid() )
						continue;

					if ( ent.LifeState != LifeState.Alive )
						continue;

					if ( !ent.PhysicsBody.IsValid() )
						continue;

					if ( ent.IsWorld )
						continue;

					var targetPos = ent.PhysicsBody.MassCenter;

					var dist = Vector3.DistanceBetween( sourcePos, targetPos );
					if ( dist > explosionBehavior.Radius )
						continue;

					var tr = Trace.Ray( sourcePos, targetPos )
						.Ignore( this )
						.WorldOnly()
						.Run();

					if ( tr.Fraction < 1.0f )
					{
						continue;
					}

					var distanceMul = 1.0f - Math.Clamp( dist / explosionBehavior.Radius, 0.0f, 1.0f );
					var damage = explosionBehavior.Damage * distanceMul;
					var force = (explosionBehavior.Force * distanceMul) * ent.PhysicsBody.Mass;
					var forceDir = (targetPos - sourcePos).Normal;

					ent.TakeDamage( DamageInfo.Explosion( sourcePos, forceDir * force, damage )
						.WithAttacker( Owner ) );
				}
			}
		}
	}
}
