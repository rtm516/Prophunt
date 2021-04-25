using Prophunt.Util;
using Sandbox;
using Sandbox.UI;
using System;
using System.Collections.Generic;
using System.Linq;

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
				Game.Instance.SystemMessage( $"Round over! {(SeekersWin ? "Seekers" : "Props")} win!" );
			}
		}

		public override void Tick()
		{
			base.Tick();

			if ( TimeSinceRoundStart > 10 && Host.IsServer )
			{
				Game.Instance.ChangeRound( new PreGameRound() );
			}
		}

	}
}
