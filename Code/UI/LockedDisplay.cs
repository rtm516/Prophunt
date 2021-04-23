using Prophunt.Util;
using Sandbox;
using Sandbox.UI;
using Sandbox.UI.Construct;
using System;

namespace Prophunt.UI
{
	public class LockedDisplay : Panel
	{
		private Label Label;

		public LockedDisplay()
		{
			Label = Add.Label("", "value" );
		}
		public override void Tick()
		{
			var player = Player.Local as ProphuntPlayer;
			if ( player == null ) return;

			Label.SetText( player.Locked ? "🔒" : "🔓" );
		}
	}
}
