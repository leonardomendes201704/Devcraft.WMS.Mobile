using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;

namespace HelloWorld;

public static class ApiClient
{
	static readonly HttpClient Http = new();

	public static async Task<T?> GetAsync<T>(string path, IDictionary<string, string?>? headers = null)
	{
		using var request = new HttpRequestMessage(HttpMethod.Get, BuildUrl(path));
		await AddAuthHeaderAsync(request);
		AddCustomHeaders(request, headers);
		using var response = await Http.SendAsync(request);
		response.EnsureSuccessStatusCode();
		var json = await response.Content.ReadAsStringAsync();
		return JsonSerializer.Deserialize<T>(json, JsonOptions);
	}

	public static async Task<T?> PostAsync<T>(string path, object payload, IDictionary<string, string?>? headers = null)
	{
		using var request = new HttpRequestMessage(HttpMethod.Post, BuildUrl(path));
		await AddAuthHeaderAsync(request);
		AddCustomHeaders(request, headers);
		request.Content = JsonContent.Create(payload);
		using var response = await Http.SendAsync(request);
		response.EnsureSuccessStatusCode();
		var json = await response.Content.ReadAsStringAsync();
		return JsonSerializer.Deserialize<T>(json, JsonOptions);
	}

	static async Task AddAuthHeaderAsync(HttpRequestMessage request)
	{
		var token = await AuthStorage.GetTokenAsync();
		if (!string.IsNullOrWhiteSpace(token))
		{
			request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
		}
	}

	static void AddCustomHeaders(HttpRequestMessage request, IDictionary<string, string?>? headers)
	{
		if (headers is null)
			return;

		foreach (var pair in headers)
		{
			if (!string.IsNullOrWhiteSpace(pair.Value))
			{
				request.Headers.TryAddWithoutValidation(pair.Key, pair.Value);
			}
		}
	}

	static string BuildUrl(string path)
	{
		var baseUrl = GetApiBaseUrl();
		return path.StartsWith("/") ? $"{baseUrl}{path}" : $"{baseUrl}/{path}";
	}

	static string GetApiBaseUrl()
	{
#if ANDROID
		if (AppSettings.UseDeviceUrl)
			return AppSettings.DeviceApiUrl;
		return "http://10.0.2.2:5137";
#else
		return "http://localhost:5137";
#endif
	}

	static readonly JsonSerializerOptions JsonOptions = new()
	{
		PropertyNameCaseInsensitive = true
	};
}
