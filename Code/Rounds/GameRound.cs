using Prophunt.Util;
using Sandbox;
using System.Collections.Generic;

namespace Prophunt.Rounds
{
	public partial class GameRound : BaseRound
	{
		public override void PlayerKilled( Player player )
		{
			base.PlayerKilled( player );

			Dictionary<Team, int> playerTeams = new Dictionary<Team, int>();
			int aliveCount = 0;
			foreach ( Player loopPlayer in Player.All )
			{
				ProphuntPlayer prophuntPlayer = loopPlayer as ProphuntPlayer;
				if ( prophuntPlayer.Team != Team.Spectator )
				{
					aliveCount++;
					playerTeams[prophuntPlayer.Team] = playerTeams.GetValueOrDefault( prophuntPlayer.Team, 0 );
				}
			}

			if ( aliveCount == 0 )
			{
				// If everyone is dead make props win
				Game.Instance.ChangeRound( new PostGameRound( false ) );
			}

			int count;
			if ( playerTeams.TryGetValue( Team.Prop, out count ) && count == 0 )
			{
				// Seekers win
				Game.Instance.ChangeRound( new PostGameRound(true) );
			}
			else if ( playerTeams.TryGetValue( Team.Seeker, out count ) && count == 0 )
			{
				// Props win
				Game.Instance.ChangeRound( new PostGameRound(false) );
			}
		}
	}
}
