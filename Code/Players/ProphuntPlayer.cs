using Prophunt.Utils;
using Sandbox;

namespace Prophunt.Players
{
	internal partial class ProphuntPlayer : BasePlayer
	{
		[Net]
		public Team Team { get; set; }
		[Net]
		public bool Locked { get; set; }
		[Net]
		public int Points { get; set; }

		public ProphuntPlayer()
		{
			Log.Info( "Prophunt Player" );
			Inventory = new BaseInventory( this );
		}

		public override void Spawn()
		{
			base.Spawn();

			Team = Team.Spectator;
		}

		public override void Respawn()
		{
			SetModel( "models/citizen/citizen.vmdl" );

			Controller = Team == Team.Spectator ? new SpectatorController() : Team == Team.Seeker ? new SeekerController() : new WalkController();
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

			Dress();
			ClearAmmo();
			
			GiveAmmo( AmmoType.Buckshot, 20 );
			GiveAmmo( AmmoType.SMG, 200 );

			base.Respawn();
		}
	}
}
