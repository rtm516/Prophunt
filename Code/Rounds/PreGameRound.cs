using System;
using System.Collections.Generic;
using System.Linq;
using Prophunt.Players;
using Prophunt.Utils;
using Sandbox;

namespace Prophunt.Rounds
{
	internal class PreGameRound : BaseRound
	{
		public override string Name { get => "Waiting"; }

		public override void Start()
		{
			base.Start();

			CheckReady();
		}

		public override void Finish()
		{
			base.Finish();

			if ( Host.IsClient ) return;

			// Remove existing props
			foreach ( Entity entity in Entity.All )
			{
				if ( entity is Prop prop && entity.ClassInfo != null && (entity.ClassInfo.Name == "prop_physics" || entity.ClassInfo.Name == "ph_prop_physics") )
				{
					entity.Delete();
				}
			}

			// Spawn map props
			List<Prop> props = new List<Prop>();
			foreach ( MapProp mapProp in Game.Instance.MapProps )
			{
				Prop prop = Library.Create<Entity>( mapProp.ClassName ) as Prop;
				prop.SetModel( mapProp.Model );
				prop.WorldPos = mapProp.Position;
				prop.WorldRot = mapProp.Rotation;
				prop.WorldScale = mapProp.Scale;
				prop.RenderColor = mapProp.Color;

				// Freeze the prop until we are done placing
				prop.MoveType = MoveType.None;
				prop.PhysicsEnabled = false;

				props.Add( prop );
			}

			// Fixes physics issues on initial spawn
			foreach ( Prop prop in props )
			{
				prop.Spawn();
			}
		}

		public override void ClientJoined( Client client )
		{
			base.ClientJoined( client );
			CheckReady();
		}

		public override void ClientDisconnected( Client client, NetworkDisconnectionReason reason )
		{
			base.ClientDisconnected( client, reason );
			CheckReady( client );
		}

		private void CheckReady( Client ignored = null )
		{
			if ( Host.IsClient ) return;

			int playerCount = Client.All.Count;
			if ( playerCount >= Config.MinPlayers )
			{
				List<Player> allPlayers = Client.All.Where( client => client != ignored ).Select( client => client.Pawn as Player ).ToList();

				List<Player> seekers = allPlayers.OrderBy( x => Rand.Float() ).Take( (int)Math.Ceiling( playerCount * Config.SeekerPct ) ).ToList();

				foreach ( Player player in allPlayers )
				{
					(player as ProphuntPlayer).Team = seekers.Remove( player ) ? Team.Seeker : Team.Prop;
					player.Respawn();
				}

				Game.Instance.ChangeRound( new WarnupRound() );
			}
			else
			{
				Game.SystemMessage( $"Need {Config.MinPlayers - playerCount} more players to start! ({playerCount}/{Config.MinPlayers})" );
			}
		}
	}
}
