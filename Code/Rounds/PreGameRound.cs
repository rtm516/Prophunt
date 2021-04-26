﻿using System;
using System.Collections.Generic;
using System.Linq;
using Prophunt.Players;
using Prophunt.Utils;
using Sandbox;

namespace Prophunt.Rounds
{
	internal class PreGameRound : BaseRound
	{
		public override string Name { get => "Waiting"; }

		public override void Start()
		{
			base.Start();

			CheckReady();
		}

		public override void PlayerJoined( Player player )
		{
			base.PlayerJoined( player );
			CheckReady();
		}

		public override void PlayerDisconnected( Player player, NetworkDisconnectionReason reason )
		{
			base.PlayerDisconnected( player, reason );
			CheckReady( player );
		}

		private void CheckReady( Player ignored = null )
		{
			if ( Host.IsClient ) return;

			int playerCount = Player.All.Count;
			if ( playerCount >= Config.MinPlayers )
			{
				List<Player> allPlayers = Player.All.Where( player => player != ignored ).ToList();

				List<Player> seekers = allPlayers.OrderBy( x => Rand.Float() ).Take( (int)Math.Ceiling( playerCount * Config.SeekerPct ) ).ToList();

				foreach ( Player player in allPlayers )
				{
					(player as ProphuntPlayer).Team = seekers.Remove( player ) ? Team.Seeker : Team.Prop;
					player.Respawn();
				}

				Game.Instance.ChangeRound( new WarnupRound() );
			}
			else
			{
				Game.SystemMessage( $"Need {Config.MinPlayers - playerCount} more players to start! ({playerCount}/{Config.MinPlayers})" );
			}
		}
	}
}
