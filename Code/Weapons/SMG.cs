using Sandbox;
using Prophunt.Utils;
using prophunt.Weapons;

[Library( "weapon_smg", Title = "SMG" )]
partial class SMG : ProphuntWeapon
{ 
	public override string ViewModelPath => "weapons/rust_smg/v_rust_smg.vmdl";

	public override float PrimaryRate => 15.0f;
	public override float SecondaryRate => 1.0f;
	public override AmmoType AmmoType => AmmoType.SMG;
	public override int ClipSize => 30;
	public override float ReloadTime => 4.0f;
	public override int BucketWeight => 2;

	public override void Spawn()
	{
		base.Spawn();

		SetModel( "weapons/rust_smg/rust_smg.vmdl" );
		AmmoClip = 20;
	}

	public override void AttackPrimary()
	{
		TimeSincePrimaryAttack = 0;
		TimeSinceSecondaryAttack = 0;

		if ( !TakeAmmo( 1 ) )
		{
			if ( AmmoClip >= ClipSize )
			{
				DryFire();
			}
			else
			{
				Reload();
			}

			return;
		}

		(Owner as AnimEntity).SetAnimParam( "b_attack", true );

		//
		// Tell the clients to play the shoot effects
		//
		ShootEffects();
		PlaySound( "rust_smg.shoot" );

		//
		// Shoot the bullets
		//
		ShootBullet( 0.1f, 1.5f, 5.0f, 3.0f );

	}

	public override void AttackSecondary()
	{
		TimeSinceSecondaryAttack = 0;

		if ( Host.IsClient ) return;

		new Grenade
		{
			WorldPos = Owner.EyePos + Owner.EyeRot.Forward * 50,
			WorldRot = Owner.EyeRot,
			Velocity = Owner.EyeRot.Forward * 1000,
			Owner = Owner
		};
	}

	[ClientRpc]
	protected override void ShootEffects()
	{
		Host.AssertClient();

		Particles.Create( "particles/pistol_muzzleflash.vpcf", EffectEntity, "muzzle" );
		Particles.Create( "particles/pistol_ejectbrass.vpcf", EffectEntity, "ejection_point" );

		if ( IsLocalPawn )
		{
			new Sandbox.ScreenShake.Perlin(0.5f, 4.0f, 1.0f, 0.5f);
		}

		ViewModelEntity?.SetAnimParam( "fire", true );
		CrosshairPanel?.OnEvent( "fire" );
	}

	public override void SimulateAnimator( PawnAnimator anim )
	{
		anim.SetParam( "holdtype", 2 ); // TODO this is shit
		anim.SetParam( "aimat_weight", 1.0f );
	}
}
