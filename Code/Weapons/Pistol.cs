using Prophunt;
using Sandbox;
using System.Collections.Generic;

[Library( "weapon_pistol" )]
partial class Pistol : BaseWeapon
{
	public override string ViewModelPath => "weapons/rust_pistol/v_rust_pistol.vmdl";
	public override float PrimaryRate => 10;

	public override void Spawn()
	{
		base.Spawn();

		SetModel( "weapons/rust_pistol/rust_pistol.vmdl" );
	}

	/// <summary>
	/// Lets make primary attack semi automatic
	/// </summary>
	public override bool CanPrimaryAttack()
	{
		if ( !Owner.Input.Pressed( InputButton.Attack1 ) )
			return false;

		return base.CanPrimaryAttack();
	}

	public override void AttackPrimary()
	{
		TimeSincePrimaryAttack = 0;

		//
		// Tell the clients to play the shoot effects
		//
		ShootEffects();


		bool InWater = Physics.TestPointContents( Owner.EyePos, CollisionLayer.Water );
		var forward = Owner.EyeRot.Forward * (InWater ? 500 : 4000);

		//
		// ShootBullet is coded in a way where we can have bullets pass through shit
		// or bounce off shit, in which case it'll return multiple results
		//
		foreach ( var tr in TraceBullet( Owner.EyePos, Owner.EyePos + Owner.EyeRot.Forward * 4000 ) )
		{
			tr.Surface.DoBulletImpact( tr );

			if ( !IsServer ) continue;
			if ( !tr.Entity.IsValid() ) continue;

			if ( tr.Entity is ProphuntPlayer player && player.Team == (Owner as ProphuntPlayer).Team ) continue;

			//
			// We turn predictiuon off for this, so aany exploding effects
			//
			using ( Prediction.Off() )
			{

				var damage = DamageInfo.FromBullet( tr.EndPos, forward.Normal * 100, 15 )
					.UsingTraceResult( tr )
					.WithAttacker( Owner )
					.WithWeapon( this );

				tr.Entity.TakeDamage( damage );

				if ( tr.Entity is Prop && tr.Body.BodyType == PhysicsBodyType.Dynamic )
				{
					var damageSelf = DamageInfo.Generic( 5 )
							.WithAttacker( Owner )
							.WithWeapon( this );

					Owner.TakeDamage( damageSelf );
				}
			}
		}

	}

	public override IEnumerable<TraceResult> TraceBullet( Vector3 start, Vector3 end, float radius = 2 )
	{
		bool InWater = Physics.TestPointContents( start, CollisionLayer.Water );

		var tr = Trace.Ray( start, end )
				//.UseHitboxes()
				.HitLayer( CollisionLayer.Water, !InWater )
				.Ignore( Owner )
				.Ignore( this )
				.Size( radius )
				.Run();

		yield return tr;
	}

	public void OnBulletHitEntity( Entity ent, Transform position )
	{

	}

	[ClientRpc]
	public virtual void ShootEffects()
	{
		Host.AssertClient();

		var muzzle = EffectEntity.GetAttachment( "muzzle" );
		bool InWater = Physics.TestPointContents( muzzle.Pos, CollisionLayer.Water );

		Sound.FromEntity( "rust_pistol.shoot", this );
		Particles.Create( "particles/pistol_muzzleflash.vpcf", EffectEntity, "muzzle" );

		ViewModelEntity?.SetAnimParam( "fire", true );
		CrosshairPanel?.OnEvent( "onattack" );

		if ( Owner == Player.Local )
		{
			new Sandbox.ScreenShake.Perlin( 0.5f, 2.0f, 0.5f );
		}
	}
}
