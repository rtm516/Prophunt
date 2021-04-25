using Prophunt;
using Prophunt.Rounds;
using Sandbox;

internal class ProphuntWeapon : BaseWeapon
{
	public override bool CanPrimaryAttack()
	{
		// Prevent shooting for seekers in warmup
		if ( Prophunt.Game.Instance.Round is WarnupRound && Owner is ProphuntPlayer player && player.Team == Prophunt.Util.Team.Seeker)
		{
			return false;
		}

		return base.CanPrimaryAttack();
	}
}
