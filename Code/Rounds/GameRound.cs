using Prophunt.Players;
using Prophunt.Utils;
using Sandbox;
using System.Collections.Generic;

namespace Prophunt.Rounds
{
	public partial class GameRound : BaseRound
	{
		public override string Name { get => "Seeking"; }

		public GameRound() : base()
		{
			RoundLength = Config.GameRoundLength;
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
			CheckPlayers( player );
		}

		private void CheckPlayers( Player ignored = null )
		{
			if ( Host.IsClient ) return;

			Dictionary<Team, int> playerTeams = new();
			int aliveCount = 0;
			foreach ( Player player in Player.All )
			{
				if ( player == ignored ) continue;
				ProphuntPlayer prophuntPlayer = player as ProphuntPlayer;
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

			if ( !playerTeams.TryGetValue( Team.Prop, out _ ) )
			{
				// Seekers win
				Game.Instance.ChangeRound( new PostGameRound( true ) );
			}
			else if ( !playerTeams.TryGetValue( Team.Seeker, out _ ) )
			{
				// Props win
				Game.Instance.ChangeRound( new PostGameRound( false ) );
			}
		}
	}
}
