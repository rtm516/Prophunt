using Prophunt.Rounds;
using Prophunt.UI;
using Prophunt.Util;
using Sandbox;
using Sandbox.UI;
using Sandbox.UI.Construct;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Prophunt
{
	[Library( "prophunt", Title = "Prophunt" )]
	partial class Game : Sandbox.Game
	{
		private TimeSince timeSinceRoundOver;

		[Net]
		public BaseRound Round { get; set; }

		public static Game Instance
		{
			get => Current as Game;
		}

		public Game()
		{
			Log.Info( "Game Started" );
			if ( IsServer )
				new MainHud();

			_ = StartTickTimer();
		}

		public override Player CreatePlayer() => new ProphuntPlayer();

		public override void PlayerKilled( Player player )
		{
			base.PlayerKilled( player );
			Round?.PlayerKilled( player );

			Dictionary<Team, int> playerTeams = new Dictionary<Team, int>();
			foreach ( Player loopPlayer in Player.All )
			{
				ProphuntPlayer prophuntPlayer = loopPlayer as ProphuntPlayer;
				if ( prophuntPlayer.Team != Team.Spectator )
				{
					playerTeams[prophuntPlayer.Team] = playerTeams.GetValueOrDefault( prophuntPlayer.Team, 0 );
				}
			}

			int count;
			if ( playerTeams.TryGetValue( Team.Prop, out count ) && count == 0 )
			{
				// Seekers win
			}
			else if ( playerTeams.TryGetValue( Team.Seeker, out count ) && count == 0 )
			{
				// Props win
			}

			timeSinceRoundOver = 0;


			// TODO: This is shit and needs making better
			foreach ( Player loopPlayer in Player.All )
			{
				(loopPlayer as ProphuntPlayer).Team = Rand.Int( 1 ) == 0 ? Team.Seeker : Team.Prop;
				loopPlayer.Respawn();
			}
		}

		// Work around until ticks are implemented for Games
		public async Task StartTickTimer()
		{
			while ( true )
			{
				await Task.NextPhysicsFrame();
				Tick();
			}
		}

		private void Tick()
		{
			Round?.Tick();
		}
	}
}
