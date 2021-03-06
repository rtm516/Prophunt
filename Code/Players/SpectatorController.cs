using System;
using System.Collections.Generic;
using System.Linq;
using Prophunt.Utils;
using Sandbox;

namespace Prophunt.Players
{
	public class SpectatorController : NoclipController
	{
		private int PlayerIndex;

		internal ProphuntPlayer TargetPlayer;
		internal bool TargetLocked;

		public SpectatorController()
		{
			PlayerIndex = 0;
			TargetLocked = true;
		}

		public override void Simulate()
		{
			base.Simulate();

			bool posUpdated = false;
			if ( Input.Pressed( InputButton.Attack1 ) )
			{
				PlayerIndex++;
				posUpdated = true;
			}
			else if( Input.Pressed( InputButton.Attack2 ) )
			{
				PlayerIndex--;
				posUpdated = true;
			}

			if ( posUpdated )
			{
				List<Player> alivePlayers = Client.All.Select( client => client.Pawn as Player ).Where( player => (player as ProphuntPlayer).Team != Team.Spectator ).ToList();

				if ( alivePlayers.Count == 0 ) return;

				// Wrap the player count
				PlayerIndex = Math.Abs(PlayerIndex) % alivePlayers.Count;

				// Don't allow them to select themselves
				if ( alivePlayers[PlayerIndex] == Pawn )
				{
					PlayerIndex++;
					PlayerIndex = Math.Abs( PlayerIndex ) % alivePlayers.Count;
				}

				TargetPlayer = alivePlayers[PlayerIndex] as ProphuntPlayer;

				// Move to the new player position
				Position = alivePlayers[PlayerIndex].Position;
			}
		}
	}
}
