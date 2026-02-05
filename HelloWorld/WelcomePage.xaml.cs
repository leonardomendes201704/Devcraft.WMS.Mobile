namespace HelloWorld;

[QueryProperty(nameof(Email), "email")]
public partial class WelcomePage : ContentPage
{
	string _email = string.Empty;

	public string Email
	{
		get => _email;
		set
		{
			_email = value ?? string.Empty;
			WelcomeLabel.Text = $"Bem-vindo, {_email}!";
		}
	}

	public WelcomePage()
	{
		InitializeComponent();
		LoadFromStorage();
	}

	async void LoadFromStorage()
	{
		var storedEmail = await AuthStorage.GetEmailAsync();
		if (!string.IsNullOrWhiteSpace(storedEmail))
		{
			WelcomeLabel.Text = $"Bem-vindo, {storedEmail}!";
		}
	}

	void OnCardTapped(object? sender, EventArgs e)
	{
		// Placeholder para navegação futura
	}
}
