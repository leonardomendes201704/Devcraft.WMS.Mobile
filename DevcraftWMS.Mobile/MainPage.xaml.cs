namespace DevcraftWMS.Mobile;

public partial class MainPage : ContentPage
{
	int count = 0;
	static readonly HttpClient Http = new();

	public MainPage()
	{
		InitializeComponent();
	}

	private void OnCounterClicked(object? sender, EventArgs e)
	{
		count++;

		if (count == 1)
			CounterBtn.Text = $"Clicked {count} time";
		else
			CounterBtn.Text = $"Clicked {count} times";

		SemanticScreenReader.Announce(CounterBtn.Text);
	}

	private async void OnApiClicked(object? sender, EventArgs e)
	{
		ApiBtn.IsEnabled = false;
		ApiStatus.Text = "Status da API: consultando...";

		try
		{
			var baseUrl = GetApiBaseUrl();
			var url = $"{baseUrl}/swagger/index.html";
			using var response = await Http.GetAsync(url);
			ApiStatus.Text = $"Status da API: {(int)response.StatusCode} {response.ReasonPhrase}";
		}
		catch (Exception ex)
		{
			ApiStatus.Text = $"Status da API: erro - {ex.Message}";
		}
		finally
		{
			ApiBtn.IsEnabled = true;
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
}

