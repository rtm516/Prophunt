using Prophunt.Players;
using Prophunt.Utils;
using Sandbox;

namespace Prophunt.Entities
{
	[Library( "prop_physics" )]
	public partial class Prop : Sandbox.Prop, IUse
	{
		public bool IsUsable( Entity user )
		{
			if ( user is ProphuntPlayer player )
			{
				return player.Team == Team.Prop && !Config.BannedProps.Contains( GetModelName() );
			}

			return false;
		}

		public bool OnUse( Entity user )
		{
			if ( user is ProphuntPlayer player )
			{
				player.OnPropUse( this );
			}

			return false;
		}

		public override void TakeDamage( DamageInfo info )
		{
			if ( PhysicsBody != null && PhysicsBody.BodyType == PhysicsBodyType.Dynamic && info.Attacker is ProphuntPlayer )
			{
				var damageSelf = DamageInfo.Generic( info.Damage * 0.75f )
					.WithAttacker( info.Attacker )
					.WithWeapon( info.Weapon );

				info.Attacker.TakeDamage( damageSelf );
			}

			base.TakeDamage( info );
		}
	}
}
