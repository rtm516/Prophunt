using Prophunt.Players;
using Prophunt.Rounds;
using Prophunt.Utils;
using Sandbox;
using Game = Prophunt.Game;

internal class ProphuntWeapon : BaseWeapon
{
	public override bool CanPrimaryAttack()
	{
		// Prevent shooting for seekers in warmup
		if ( Game.Instance.Round is WarnupRound && Owner is ProphuntPlayer player && player.Team == Team.Seeker )
		{
			return false;
		}

		return base.CanPrimaryAttack();
	}
}
