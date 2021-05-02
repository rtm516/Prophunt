using System.Collections.Generic;
using Prophunt.Utils;
using Sandbox;
using Prop = Prophunt.Entities.Prop;

namespace Prophunt
{
	partial class Game
	{
		public List<MapProp> MapProps = new List<MapProp>();

		public override void PlayerKilled( Player player )
		{
			base.PlayerKilled( player );
			Round?.PlayerKilled( player );
		}

		public override void PlayerJoined( Player player )
		{
			base.PlayerJoined( player );
			Round?.PlayerJoined( player );
		}

		public override void PlayerDisconnected( Player player, NetworkDisconnectionReason reason )
		{
			base.PlayerDisconnected( player, reason );
			Round?.PlayerDisconnected( player, reason );
		}

		public override void PostLevelLoaded()
		{
			base.PostLevelLoaded();

			if ( Host.IsClient ) return;

			foreach (Entity entity in Entity.All)
			{
				if ( entity is Prop prop && entity.ClassInfo != null && (entity.ClassInfo.Name == "prop_physics" || entity.ClassInfo.Name == "ph_prop_physics" ))
				{
					MapProp mapProp = new MapProp();

					mapProp.ClassName = prop.ClassInfo.Name;
					mapProp.Model = prop.GetModel();
					mapProp.Position = prop.WorldPos;
					mapProp.Rotation = prop.WorldRot;
					mapProp.Scale = prop.WorldScale;
					mapProp.Color = prop.RenderColor;

					MapProps.Add( mapProp );
				}
			}
		}

		private void Tick()
		{
			Round?.Tick();

			// From: https://github.com/Facepunch/sbox-hidden/blob/71e32f7d1f28d51a5c3078e99f069d0b73ef490d/code/Game.cs#L186
			if ( IsClient )
			{
				// We have to hack around this for now until we can detect changes in net variables.
				if ( _lastRound != Round )
				{
					_lastRound?.Finish();
					_lastRound = Round;
					_lastRound.Start();
				}
			}
		}
	}
}
