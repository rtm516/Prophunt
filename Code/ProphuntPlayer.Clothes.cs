using Sandbox;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prophunt
{
	partial class ProphuntPlayer
	{
		private ModelEntity hat;
		private ModelEntity jacket;
		private ModelEntity pants;
		private ModelEntity shoes;

		private void Dress()
		{
			UnDress();

			if ( Team != Util.Team.Seeker ) return;

			hat = new ModelEntity();
			hat.SetModel( "models/citizen_clothes/hat/hat_uniform.police.vmdl" );
			hat.SetParent( this, true );
			hat.EnableShadowInFirstPerson = true;
			hat.EnableHideInFirstPerson = true;

			jacket = new ModelEntity();
			jacket.SetModel( "models/citizen_clothes/jacket/jacket.tuxedo.vmdl" );
			jacket.SetParent( this, true );
			jacket.EnableShadowInFirstPerson = true;
			jacket.EnableHideInFirstPerson = true;

			pants = new ModelEntity();
			pants.SetModel( "models/citizen_clothes/trousers/trousers.smart.vmdl" );
			pants.SetParent( this, true );
			pants.EnableShadowInFirstPerson = true;
			pants.EnableHideInFirstPerson = true;

			shoes = new ModelEntity();
			shoes.SetModel( "models/citizen_clothes/shoes/shoes.smartbrown.vmdl" );
			shoes.SetParent( this, true );
			shoes.EnableShadowInFirstPerson = true;
			shoes.EnableHideInFirstPerson = true;
		}

		private void UnDress()
		{
			hat?.Delete();
			jacket?.Delete();
			pants?.Delete();
			shoes?.Delete();
		}
	}
}
