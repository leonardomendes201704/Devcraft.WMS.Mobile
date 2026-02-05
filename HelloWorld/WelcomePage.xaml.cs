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
	}
}
