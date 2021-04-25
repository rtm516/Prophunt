using Sandbox;
using System.Linq;

namespace Prophunt
{
	partial class ProphuntPlayer
	{
		private TimeSince TimeSinceLastTaunt;

		protected override void UseFail()
		{
			Taunt();
		}

		private void Taunt( string path = null )
		{
			if ( path == null )
			{
				path = Rand.FromArray<string>( Config.Taunts.ToArray() );
			}

			TimeSinceLastTaunt = 0;
			PlaySound( path );
		}
	}
}
