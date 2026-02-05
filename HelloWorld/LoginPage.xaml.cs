using System.Net.Http.Json;
using System.Text.Json;

namespace HelloWorld;

public partial class LoginPage : ContentPage
{
	static readonly HttpClient Http = new();

	public LoginPage()
	{
		InitializeComponent();
		EmailEntry.Text = "admin@admin.com.br";
		PasswordEntry.Text = "Naotemsenha0!";
	}

	private async void OnLoginClicked(object? sender, EventArgs e)
	{
		StatusLabel.Text = string.Empty;
		LoginBtn.IsEnabled = false;

		try
		{
			var payload = new LoginRequest(EmailEntry.Text?.Trim() ?? string.Empty,
				PasswordEntry.Text ?? string.Empty);
			var url = $"{GetApiBaseUrl()}/api/auth/login";
			using var response = await Http.PostAsJsonAsync(url, payload);

			if (!response.IsSuccessStatusCode)
			{
				var errorText = await response.Content.ReadAsStringAsync();
				StatusLabel.Text = $"Login falhou: {(int)response.StatusCode}. {errorText}";
				return;
			}

			var json = await response.Content.ReadAsStringAsync();
			var result = JsonSerializer.Deserialize<LoginResponse>(json, new JsonSerializerOptions
			{
				PropertyNameCaseInsensitive = true
			});

			if (result?.Token is { Length: > 0 })
			{
				await AuthStorage.SaveTokenAsync(result.Token, result.Email ?? payload.Email);
			}

			await Shell.Current.GoToAsync(nameof(WelcomePage), new Dictionary<string, object>
			{
				["email"] = result?.Email ?? payload.Email
			});
		}
		catch (Exception ex)
		{
			StatusLabel.Text = $"Erro de conexão: {ex.Message}";
		}
		finally
		{
			LoginBtn.IsEnabled = true;
		}
	}

	private static string GetApiBaseUrl()
	{
#if ANDROID
		return "http://10.0.2.2:5137";
#else
		return "http://localhost:5137";
#endif
	}

	private sealed record LoginRequest(string Email, string Password);
	private sealed record LoginResponse(string UserId, string Email, string Token);
}
