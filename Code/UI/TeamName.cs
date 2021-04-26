using Prophunt.Players;
using Prophunt.Utils;
using Sandbox;
using Sandbox.UI;
using Sandbox.UI.Construct;
using System;

namespace Prophunt.UI
{
	public class TeamName : Panel
	{
		private Label Team;

		public TeamName()
		{
			Team = Add.Label("", "team");
		}
		public override void Tick()
		{
			if ( Player.Local is not ProphuntPlayer player ) return;

			Team.SetText( Enum.GetName( typeof( Team ), player.Team ) );
		}
	}
}
