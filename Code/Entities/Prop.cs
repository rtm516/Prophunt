using Sandbox;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prophunt.Entities
{
	[Library( "prop_physics" )]
	public partial class Prop : Sandbox.Prop, IUse
	{
		public bool IsUsable( Entity user )
		{
			if ( user is ProphuntPlayer player )
			{
				return player.Team == Util.Team.Prop && this.GetModelName() != "models/citizen_props/hotdog01.vmdl" && this.GetModelName() != "models/citizen_props/newspaper01.vmdl";
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
	}
}
