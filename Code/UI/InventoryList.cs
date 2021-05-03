using System.Collections.Generic;
using Prophunt.Players;
using Sandbox;
using Sandbox.UI;
using Sandbox.UI.Construct;

namespace Prophunt.UI
{
	public class InventoryList : Panel, IClientInput
	{
		private List<Label> Items = new();

		public InventoryList()
		{
			StyleSheet.Load( "/ui/InventoryList.scss" );

			//Items.Add( Add.Label( "Pistol", "weapon" ) );
			//Items.Add( Add.Label( "SMG", "weapon" ) );
			//Items.Add( Add.Label( "Shotgun", "weapon" ) );
		}

		public override void Tick()
		{
			base.Tick();

			if ( Player.Local is not ProphuntPlayer player ) return;

			int i;
			for ( i = 0; i < player.Inventory.Count(); i++ )
			{
				ProphuntWeapon weapon = player.Inventory.GetSlot( i ) as ProphuntWeapon;
				if ( i > Items.Count - 1 || Items[i] is not Label )
				{
					Items.Add( Add.Label( weapon.ToString(), "weapon" ) );
				}
				else
				{
					Items[i].SetText( weapon.ToString() );
				}

				Items[i].SetClass( "selected", weapon.IsActiveChild() );
				Items[i].AddClass( "weapon" );
			}

			for ( int j = i; j < Items.Count; j++ )
			{
				Items[j].Delete();
			}
		}

		public void ProcessClientInput( ClientInput input )
		{
			var player = Player.Local;
			if ( player == null )
				return;

			var inventory = Player.Local.Inventory;
			if ( inventory == null )
				return;

			if ( input.Pressed( InputButton.Slot1 ) ) SetActiveSlot( input, inventory, 0 );
			if ( input.Pressed( InputButton.Slot2 ) ) SetActiveSlot( input, inventory, 1 );
			if ( input.Pressed( InputButton.Slot3 ) ) SetActiveSlot( input, inventory, 2 );
			if ( input.Pressed( InputButton.Slot4 ) ) SetActiveSlot( input, inventory, 3 );
			if ( input.Pressed( InputButton.Slot5 ) ) SetActiveSlot( input, inventory, 4 );
			if ( input.Pressed( InputButton.Slot6 ) ) SetActiveSlot( input, inventory, 5 );

			if ( input.MouseWheel != 0 ) SwitchActiveSlot( input, inventory, input.MouseWheel );
		}

		private static void SetActiveSlot( ClientInput input, IBaseInventory inventory, int i )
		{
			var player = Player.Local;
			if ( player == null )
				return;

			var ent = inventory.GetSlot( i );
			if ( player.ActiveChild == ent )
				return;

			if ( ent == null )
				return;

			input.ActiveChild = ent;
		}

		private static void SwitchActiveSlot( ClientInput input, IBaseInventory inventory, int idelta )
		{
			var count = inventory.Count();
			if ( count == 0 ) return;

			var slot = inventory.GetActiveSlot();
			var nextSlot = slot + idelta;

			while ( nextSlot < 0 ) nextSlot += count;
			while ( nextSlot >= count ) nextSlot -= count;

			SetActiveSlot( input, inventory, nextSlot );
		}
	}
}
