namespace HelloWorld;

[QueryProperty(nameof(Email), "email")]
public partial class WelcomePage : ContentPage
{
	string _email = string.Empty;
	bool _isAnimating;

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

	async void OnCardTapped(object? sender, TappedEventArgs e)
	{
		if (_isAnimating || sender is not VisualElement card)
			return;

		try
		{
			_isAnimating = true;
			await card.ScaleTo(0.97, 70, Easing.CubicOut);
			await card.FadeTo(0.85, 70, Easing.CubicOut);
			await card.ScaleTo(1.0, 120, Easing.CubicIn);
			await card.FadeTo(1.0, 120, Easing.CubicIn);
		}
		finally
		{
			_isAnimating = false;
		}
	}
}
