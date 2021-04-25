using Prophunt.Util;
using Sandbox;
using System;
using System.Collections.Generic;

namespace Prophunt.Rounds
{
	public partial class GameRound : BaseRound
	{
		public override string Name { get => "Seeking"; }

		public GameRound() : base()
		{
			RoundLength = 60 * 5;
		}

		public override void Start()
		{
			base.Start();
			CheckPlayers();
		}

		public override void Tick()
		{
			base.Tick();

			if ( Host.IsServer && TimeSinceRoundStart > RoundLength )
			{
				// Time ran out so props win
				Game.Instance.ChangeRound( new PostGameRound( false ) );
			}
		}

		public override void PlayerKilled( Player player )
		{
			base.PlayerKilled( player );
			CheckPlayers();
		}

		public override void PlayerDisconnected( Player player, NetworkDisconnectionReason reason )
		{
			base.PlayerDisconnected( player, reason );
			CheckPlayers();
		}

		private void CheckPlayers()
		{
			if ( Host.IsClient ) return;

			Dictionary<Team, int> playerTeams = new Dictionary<Team, int>();
			int aliveCount = 0;
			foreach ( Player loopPlayer in Player.All )
			{
				ProphuntPlayer prophuntPlayer = loopPlayer as ProphuntPlayer;
				if ( prophuntPlayer.Team != Team.Spectator )
				{
					aliveCount++;
					playerTeams[prophuntPlayer.Team] = playerTeams.GetValueOrDefault( prophuntPlayer.Team, 0 ) + 1;
				}
			}

			if ( aliveCount == 0 )
			{
				// If everyone is dead make props win
				Game.Instance.ChangeRound( new PostGameRound( false ) );
				return;
			}

			int count;
			if ( !playerTeams.TryGetValue( Team.Prop, out count ) )
			{
				// Seekers win
				Game.Instance.ChangeRound( new PostGameRound( true ) );
			}
			else if ( !playerTeams.TryGetValue( Team.Seeker, out count ) )
			{
				// Props win
				Game.Instance.ChangeRound( new PostGameRound( false ) );
			}
		}
	}
}
