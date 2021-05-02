using Prophunt.Players;
using Prophunt.Utils;
using Sandbox;
using Sandbox.UI;
using Sandbox.UI.Construct;
using System;

namespace Prophunt.UI
{
	public class PlayerCount : Panel
	{
		private Label Seekers;
		private Label Props;

		public PlayerCount()
		{
			Panel panel = Add.Panel( "PlayerCountDisplay" );

			Seekers = panel.Add.Label( "", "Seekers" );
			Props = panel.Add.Label( "", "Props" );
		}

		public override void Tick()
		{
			if ( Player.Local is not ProphuntPlayer player ) return;

			int seekerCount = 0;
			int propsCount = 0;
			foreach ( Player loopPlayer in Player.All )
			{
				if ( loopPlayer is ProphuntPlayer prophuntPlayer )
				{
					if ( prophuntPlayer.Team == Team.Seeker )
					{
						seekerCount++;
					}
					else if ( prophuntPlayer.Team == Team.Prop )
					{
						propsCount++;
					}
				}
			}

			Seekers.SetText( seekerCount.ToString() );
			Props.SetText( propsCount.ToString() );
		}
	}
}
