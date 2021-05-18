using Prophunt.Players;
using Prophunt.Utils;
using Sandbox;
using Sandbox.UI;
using Sandbox.UI.Construct;
using System;
using System.Linq;

namespace Prophunt.UI
{
	public class PlayerCount : Panel
	{
		private Label Seekers;
		private Label Props;

		public PlayerCount()
		{
			StyleSheet.Load( "/ui/PlayerCount.scss" );

			Panel panel = Add.Panel( "PlayerCountDisplay" );

			Seekers = panel.Add.Label( "", "Seekers" );
			Props = panel.Add.Label( "", "Props" );
		}

		public override void Tick()
		{
			if ( Local.Pawn is not ProphuntPlayer player ) return;

			int seekerCount = 0;
			int propsCount = 0;
			var players = Client.All.Select( client => client.Pawn as Player );
			foreach ( Player loopPlayer in players )
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
