namespace HelloWorld;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();
		Routing.RegisterRoute(nameof(WelcomePage), typeof(WelcomePage));
		Routing.RegisterRoute(nameof(ContextSelectionPage), typeof(ContextSelectionPage));
	}
}
