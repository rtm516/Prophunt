using Prophunt.Util;
using Sandbox;
using Sandbox.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prophunt.Rounds
{
	partial class PreGameRound : BaseRound
	{
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
			CheckReady();
		}

		private void CheckReady()
		{
			if ( Host.IsClient ) return;

			int playerCount = Player.All.Count;
			if ( playerCount >= Game.Instance.MinPlayers )
			{
				List<Player> seekers = Player.All.OrderBy( x => Rand.Float() ).Take( (int)Math.Ceiling( playerCount * Game.Instance.SeekerPct ) ).ToList();

				foreach ( Player loopPlayer in Player.All )
				{
					(loopPlayer as ProphuntPlayer).Team = seekers.Remove( loopPlayer ) ? Team.Seeker : Team.Prop;
					loopPlayer.Respawn();
				}

				Game.Instance.ChangeRound( new WarnupRound() );
			}
			else
			{
				Game.Instance.SystemMessage( $"Need {Game.Instance.MinPlayers - playerCount} more players to start! ({playerCount}/{Game.Instance.MinPlayers})" );
			}
		}
	}
}
