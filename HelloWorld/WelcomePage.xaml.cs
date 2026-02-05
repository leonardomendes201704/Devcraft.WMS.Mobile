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

		var customer = await AuthStorage.GetCustomerNameAsync();
		var warehouse = await AuthStorage.GetWarehouseNameAsync();
		if (!string.IsNullOrWhiteSpace(customer) && !string.IsNullOrWhiteSpace(warehouse))
		{
			ContextLabel.Text = $"Contexto: {customer} • {warehouse}";
		}
		else
		{
			ContextLabel.Text = "Contexto: não selecionado";
		}
	}

	async void OnCardTapped(object? sender, EventArgs e)
	{
		if (_isAnimating || sender is not VisualElement card)
			return;

		try
		{
			_isAnimating = true;
			await card.ScaleTo(1.02, 60, Easing.CubicOut);
			await card.RotateTo(0.6, 50, Easing.CubicOut);
			await card.RotateTo(-0.6, 50, Easing.CubicOut);
			await card.RotateTo(0, 50, Easing.CubicOut);
			await card.ScaleTo(1.0, 80, Easing.CubicIn);
		}
		finally
		{
			_isAnimating = false;
		}
	}
}
