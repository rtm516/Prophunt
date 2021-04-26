using Prophunt.Utils;
using Sandbox;

namespace Prophunt.Players
{
	internal partial class ProphuntPlayer
	{
		[Net]
		public TimeSince TimeSinceLastTaunt { get; private set; }

		protected override void UseFail()
		{
			Taunt();
		}

		private void Taunt( string path = null )
		{
			if ( path == null )
			{
				path = Rand.FromArray( Config.Taunts.ToArray() );
			}

			TimeSinceLastTaunt = 0;
			PlaySound( path );
		}
	}
}
