namespace DevcraftWMS.Mobile;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();
		Routing.RegisterRoute(nameof(WelcomePage), typeof(WelcomePage));
		Routing.RegisterRoute(nameof(ContextSelectionPage), typeof(ContextSelectionPage));
		Routing.RegisterRoute(nameof(PickingQueuePage), typeof(PickingQueuePage));
	}
}

