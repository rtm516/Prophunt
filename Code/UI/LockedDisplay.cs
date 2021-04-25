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

			if ( player.Team != Team.Prop )
			{
				Style.Display = DisplayMode.None;
			}
			else
			{
				Style.Display = DisplayMode.Flex;
			}
			Style.Dirty();

			Label.SetText( player.Locked ? "\ud83d\udd12" : "\ud83d\udd13" );
		}
	}
}
