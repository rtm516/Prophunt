using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sandbox.UI;

namespace Prophunt.UI
{
	public class AdditionalInfoPanel : Panel
	{
		public AdditionalInfoPanel()
		{
			Panel AdditionalInfoPanelDisplay = Add.Panel( "AdditionalInfoPanelDisplay" );
			AdditionalInfoPanelDisplay.AddChild<AdditionalInfoRightPanel>();
			AdditionalInfoPanelDisplay.AddChild<AdditionalInfoLeftPanel>();
		}
	}
}
