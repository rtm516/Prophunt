using System.Threading.Tasks;
using Prophunt.Players;
using Prophunt.Rounds;
using Prophunt.UI;
using Sandbox;
using Sandbox.UI;

namespace Prophunt
{
	[Library( "prophunt", Title = "Prophunt" )]
	internal partial class Game : Sandbox.Game
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
				_ = new MainHud();

			_ = StartTickTimer();

			ChangeRound( new PreGameRound() );
		}

		public override Player CreatePlayer() => new ProphuntPlayer();

		public void ChangeRound( BaseRound round )
		{
			Assert.NotNull( round );

			Round?.Finish();
			Round = round;
			Round?.Start();
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

		public static void SystemMessage( string message )
		{
			Host.AssertServer();
			ChatBox.AddChatEntry( Player.All, "Server", message, "/materials/prophunt/ui/system.png" );
		}
	}
}
