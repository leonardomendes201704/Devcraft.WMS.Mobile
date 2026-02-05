namespace HelloWorld;

public static class AuthStorage
{
	const string TokenKey = "auth_token";
	const string EmailKey = "auth_email";

	public static async Task SaveTokenAsync(string token, string email)
	{
		await SecureStorage.SetAsync(TokenKey, token);
		await SecureStorage.SetAsync(EmailKey, email);
	}

	public static async Task<string?> GetTokenAsync()
	{
		try
		{
			return await SecureStorage.GetAsync(TokenKey);
		}
		catch
		{
			return null;
		}
	}

	public static async Task<string?> GetEmailAsync()
	{
		try
		{
			return await SecureStorage.GetAsync(EmailKey);
		}
		catch
		{
			return null;
		}
	}

	public static async Task ClearAsync()
	{
		try
		{
			SecureStorage.Remove(TokenKey);
			SecureStorage.Remove(EmailKey);
			await Task.CompletedTask;
		}
		catch
		{
			await Task.CompletedTask;
		}
	}
}
