﻿using Prophunt.Players;
using Prophunt.Utils;
using Sandbox;
using Sandbox.UI;

namespace Prophunt.UI
{
	public class PropLockOutline : Panel
	{
		public PropLockOutline()
		{
			StyleSheet.Load( "/ui/PropLockOutline.scss" );
		}

		public override void Tick()
		{
			if ( Player.Local is not ProphuntPlayer player ) return;
			
			if ( player.Team != Team.Prop )
			{
				Style.Display = DisplayMode.None;
			}
			else
			{
				Style.Display = DisplayMode.Flex;
			}

			SetClass( "PropLockOn", player.Locked );

			Style.Dirty();
		}
	}
}