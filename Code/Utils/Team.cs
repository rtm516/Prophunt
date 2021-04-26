namespace Prophunt.Utils
{
	public enum Team
	{
		Prop,
		Seeker,
		Spectator
	}

	public static class Extensions
	{
		internal static string GetName( this Team team )
		{
			return team switch
			{
				Team.Prop => "Prop",
				Team.Seeker => "Seeker",
				Team.Spectator => "Spectator",
				_ => ""
			};
		}
	}
}
