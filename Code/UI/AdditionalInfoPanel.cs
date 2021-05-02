using Sandbox.UI;

namespace Prophunt.UI
{
	public class AdditionalInfoPanel : Panel
	{
		public AdditionalInfoPanel()
		{
			StyleSheet.Load( "/ui/AdditionalInfoPanel.scss" );

			Panel AdditionalInfoPanelDisplay = Add.Panel( "AdditionalInfoPanelDisplay" );
			AdditionalInfoPanelDisplay.AddChild<AdditionalInfoRightPanel>();
			AdditionalInfoPanelDisplay.AddChild<AdditionalInfoLeftPanel>();
		}
	}
}
