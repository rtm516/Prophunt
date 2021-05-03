using Sandbox;

[Library( "weapon_pistol", Title = "Pistol" )]
partial class Pistol : ProphuntWeapon
{
	public override string ViewModelPath => "weapons/rust_pistol/v_rust_pistol.vmdl";

	public override float PrimaryRate => 15.0f;
	public override float SecondaryRate => 1.0f;
	public override int ClipSize => 12;
	public override float ReloadTime => 3.0f;
	public override int BucketWeight => 1;

	public override void Spawn()
	{
		base.Spawn();

		SetModel( "weapons/rust_pistol/rust_pistol.vmdl" );
		AmmoClip = 12;
	}

	/// <summary>
	/// Lets make primary attack semi automatic
	/// </summary>
	public override bool CanPrimaryAttack()
	{
		return base.CanPrimaryAttack() && Owner.Input.Pressed( InputButton.Attack1 );
	}

	public override void AttackPrimary()
	{
		TimeSincePrimaryAttack = 0;

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


		//
		// Tell the clients to play the shoot effects
		//
		ShootEffects();
		PlaySound( "rust_pistol.shoot" );

		//
		// Shoot the bullets
		//
		ShootBullet( 0.05f, 1.5f, 9.0f, 3.0f );
	}
}
