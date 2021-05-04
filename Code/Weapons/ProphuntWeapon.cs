using System.Collections.Generic;
using Prophunt.Players;
using Prophunt.Rounds;
using Prophunt.Utils;
using Sandbox;
using Game = Prophunt.Game;

// Based on DM98
internal partial class ProphuntWeapon : BaseWeapon
{
	public virtual AmmoType AmmoType => AmmoType.Pistol;
	public virtual int ClipSize => 16;
	public virtual float ReloadTime => 3.0f;
	public virtual int BucketWeight => 100;

	[NetPredicted]
	public int AmmoClip { get; set; }

	[NetPredicted]
	public TimeSince TimeSinceReload { get; set; }

	[NetPredicted]
	public bool IsReloading { get; set; }

	[NetPredicted]
	public TimeSince TimeSinceDeployed { get; set; }


	public int AvailableAmmo()
	{
		var owner = Owner as ProphuntPlayer;
		if ( owner == null ) return 0;
		return owner.AmmoCount( AmmoType );
	}

	public string AvailableAmmoString()
	{
		if ( AmmoType == AmmoType.Pistol )
		{
			return "\x221E";
		}

		return AvailableAmmo().ToString();
	}

	public override void ActiveStart( Entity ent )
	{
		base.ActiveStart( ent );

		TimeSinceDeployed = 0;
	}

	public override string ViewModelPath => "weapons/rust_pistol/v_rust_pistol.vmdl";

	public override void Spawn()
	{
		base.Spawn();

		SetModel( "weapons/rust_pistol/rust_pistol.vmdl" );
	}

	public override void Reload()
	{
		if ( IsReloading )
			return;

		if ( AmmoClip >= ClipSize )
			return;

		TimeSinceReload = 0;

		if ( Owner is ProphuntPlayer player )
		{
			if ( player.AmmoCount( AmmoType ) <= 0 )
				return;

			StartReloadEffects();
		}

		IsReloading = true;
		Owner.SetAnimParam( "b_reload", true );
		StartReloadEffects();
	}

	public override void OnPlayerControlTick( Player owner )
	{
		if ( TimeSinceDeployed < 0.6f )
			return;

		if ( !IsReloading )
		{
			base.OnPlayerControlTick( owner );
		}

		if ( IsReloading && TimeSinceReload > ReloadTime )
		{
			OnReloadFinish();
		}
	}

	public virtual void OnReloadFinish()
	{
		IsReloading = false;

		if ( Owner is ProphuntPlayer player )
		{
			var ammo = player.TakeAmmo( AmmoType, ClipSize - AmmoClip );
			if ( ammo == 0 )
				return;

			AmmoClip += ammo;
		}
	}

	[ClientRpc]
	public virtual void StartReloadEffects()
	{
		ViewModelEntity?.SetAnimParam( "reload", true );

		// TODO - player third person model reload
	}

	public override void AttackPrimary()
	{
		TimeSincePrimaryAttack = 0;
		TimeSinceSecondaryAttack = 0;

		DryFire();
	}

	[ClientRpc]
	protected virtual void ShootEffects()
	{
		Host.AssertClient();

		Particles.Create( "particles/pistol_muzzleflash.vpcf", EffectEntity, "muzzle" );

		if ( Owner == Player.Local )
		{
			new Sandbox.ScreenShake.Perlin();
		}

		ViewModelEntity?.SetAnimParam( "fire", true );
		CrosshairPanel?.OnEvent( "fire" );
	}

	/// <summary>
	/// Shoot a single bullet
	/// </summary>
	public virtual void ShootBullet( float spread, float force, float damage, float bulletSize )
	{
		var forward = Owner.EyeRot.Forward;
		forward += (Vector3.Random + Vector3.Random + Vector3.Random + Vector3.Random) * spread * 0.25f;
		forward = forward.Normal;

		//
		// ShootBullet is coded in a way where we can have bullets pass through shit
		// or bounce off shit, in which case it'll return multiple results
		//
		foreach ( var tr in TraceBullet( Owner.EyePos, Owner.EyePos + forward * 5000, bulletSize ) )
		{
			tr.Surface.DoBulletImpact( tr );

			if ( !IsServer ) continue;
			if ( !tr.Entity.IsValid() ) continue;

			//
			// We turn predictiuon off for this, so any exploding effects don't get culled etc
			//
			using ( Prediction.Off() )
			{
				var damageInfo = DamageInfo.FromBullet( tr.EndPos, forward * 100 * force, damage )
					.UsingTraceResult( tr )
					.WithAttacker( Owner )
					.WithWeapon( this );

				tr.Entity.TakeDamage( damageInfo );
			}
		}
	}

	public bool TakeAmmo( int amount )
	{
		if ( AmmoClip < amount )
			return false;

		AmmoClip -= amount;
		return true;
	}

	[ClientRpc]
	public virtual void DryFire()
	{
		ViewModelEntity?.SetAnimParam( "dryfire", true );
	}

	public bool IsUsable()
	{
		if ( AmmoClip > 0 ) return true;
		return AvailableAmmo() > 0;
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

	public override bool CanPrimaryAttack()
	{
		return base.CanPrimaryAttack() && CanShoot();
	}

	public override bool CanSecondaryAttack()
	{
		return base.CanSecondaryAttack() && CanShoot();
	}

	private bool CanShoot()
	{
		return !(Game.Instance.Round is WarnupRound && Owner is ProphuntPlayer player && player.Team == Team.Seeker);
	}
}
