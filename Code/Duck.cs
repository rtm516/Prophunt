using Sandbox;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prophunt
{
	[Library]
	public class Duck : Sandbox.Duck
	{
		public Duck( BasePlayerController controller ) : base( controller )
		{
		}

		public override void PreTick()
		{
			
		}
	}
}
