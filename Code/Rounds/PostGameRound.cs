using Prophunt.Utils;
using Sandbox;

namespace Prophunt.Rounds
{
	public partial class PostGameRound : BaseRound
	{
		public override string Name { get => "Finished"; }

		[Net]
		public bool SeekersWin { get; set; }

		public PostGameRound() : base()
		{
			SeekersWin = false;
			RoundLength = Config.PostGameRoundLength;
		}

		public PostGameRound( bool seekersWin ) : base()
		{
			SeekersWin = seekersWin;
			RoundLength = Config.PostGameRoundLength;
		}

		public override void Start()
		{
			base.Start();
			if ( Host.IsServer )
			{
				Game.SystemMessage( $"Round over! {(SeekersWin ? "Seekers" : "Props")} win!" );
			}
		}

		public override void Tick()
		{
			base.Tick();

			if ( TimeSinceRoundStart > RoundLength && Host.IsServer )
			{
				Game.Instance.ChangeRound( new PreGameRound() );
			}
		}
	}
}
