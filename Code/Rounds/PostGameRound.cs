using Prophunt.Util;
using Sandbox;
using Sandbox.UI;

namespace Prophunt.Rounds
{
	public partial class PostGameRound : BaseRound
	{
		[Net]
		public bool SeekersWin { get; set; }

		public PostGameRound() : base()
		{
			SeekersWin = false;
		}

		public PostGameRound( bool seekersWin ) : base()
		{
			SeekersWin = seekersWin;
		}

		public override void Start()
		{
			base.Start();
			if ( Host.IsServer )
			{
				ChatBox.AddChatEntry( Player.All, "Server", $"Round over! {(SeekersWin ? "Seekers" : "Props")} win!", "/materials/prophunt/ui/system.png" );
			}
		}

		public override void Tick()
		{
			base.Tick();

			if ( TimeSinceRoundStart > 10 && Host.IsServer )
			{
				foreach ( Player loopPlayer in Player.All )
				{
					(loopPlayer as ProphuntPlayer).Team = Rand.Int( 1 ) == 0 ? Team.Seeker : Team.Prop;
					loopPlayer.Respawn();
				}

				Game.Instance.ChangeRound( new GameRound() );
			}
		}

	}
}
