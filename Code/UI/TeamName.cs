using Prophunt.Util;
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
			var player = Player.Local as ProphuntPlayer;
			if ( player == null ) return;

			Team.SetText( Enum.GetName( typeof( Team ), player.Team ) );
		}
	}
}
