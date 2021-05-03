using System;
using Prophunt.Utils;
using Sandbox;

namespace Prophunt.Players
{
	// Taken from DM98
	internal partial class ProphuntPlayer
	{
		[Net]
		public NetList<int> Ammo { get; set; } = new();

		public void ClearAmmo()
		{
			Ammo.Clear();
		}

		public int AmmoCount( AmmoType type )
		{
			if ( type == AmmoType.Pistol ) return 999;

			if ( Ammo == null ) return 0;

			return Ammo.Get( type );
		}

		public bool GiveAmmo( AmmoType type, int amount )
		{
			if ( !Host.IsServer ) return false;
			if ( type == AmmoType.Pistol ) return false;
			if ( Ammo == null ) return false;

			var currentAmmo = AmmoCount( type );
			return Ammo.Set( type, currentAmmo + amount );
		}

		public int TakeAmmo( AmmoType type, int amount )
		{
			//if ( Ammo == null ) return 0;

			var available = Ammo.Get( type );
			if ( type == AmmoType.Pistol ) available = 999;
			amount = Math.Min( Ammo.Get( type ), amount );

			Ammo.Set( type, available - amount );
			NetworkDirty( "Ammo", NetVarGroup.Net );

			return amount;
		}
	}
}
