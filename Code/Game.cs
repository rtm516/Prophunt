﻿using Prophunt.Rounds;
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
		[Net]
		public BaseRound Round { get; set; }
		private BaseRound _lastRound;

		public static Game Instance
		{
			get => Current as Game;
		}

		public Game()
		{
			Log.Info( "Game Started" );
			if ( IsServer )
				new MainHud();

			Log.Info( IsClient.ToString() + "\t1" );
			_ = StartTickTimer();

			ChangeRound( new GameRound() );
		}

		public override Player CreatePlayer() => new ProphuntPlayer();

		public void ChangeRound( BaseRound round )
		{
			Assert.NotNull( round );

			Round?.Finish();
			Round = round;
			Round?.Start();
		}

		public override void PlayerKilled( Player player )
		{
			base.PlayerKilled( player );
			Round?.PlayerKilled( player );
		}

		// Work around until ticks are implemented for Games
		public async Task StartTickTimer()
		{
			Log.Info( IsClient.ToString() +  "\t2" );
			while ( true )
			{
				await Task.NextPhysicsFrame();
				Tick();
			}
		}

		private void Tick()
		{
			Round?.Tick();

			// From: https://github.com/Facepunch/sbox-hidden/blob/71e32f7d1f28d51a5c3078e99f069d0b73ef490d/code/Game.cs#L186
			if ( IsClient )
			{
				// We have to hack around this for now until we can detect changes in net variables.
				if ( _lastRound != Round )
				{
					_lastRound?.Finish();
					_lastRound = Round;
					_lastRound.Start();
				}
			}
		}
	}
}
