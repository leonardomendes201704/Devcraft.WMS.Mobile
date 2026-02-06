namespace DevcraftWMS.Mobile;

public static class AuthStorage
{
	const string TokenKey = "auth_token";
	const string EmailKey = "auth_email";
	const string CustomerIdKey = "auth_customer_id";
	const string CustomerNameKey = "auth_customer_name";
	const string WarehouseIdKey = "auth_warehouse_id";
	const string WarehouseNameKey = "auth_warehouse_name";

	public static async Task SaveTokenAsync(string token, string email)
	{
		await SecureStorage.SetAsync(TokenKey, token);
		await SecureStorage.SetAsync(EmailKey, email);
	}

	public static async Task SaveContextAsync(Guid customerId, string customerName, Guid warehouseId, string warehouseName)
	{
		await SecureStorage.SetAsync(CustomerIdKey, customerId.ToString());
		await SecureStorage.SetAsync(CustomerNameKey, customerName);
		await SecureStorage.SetAsync(WarehouseIdKey, warehouseId.ToString());
		await SecureStorage.SetAsync(WarehouseNameKey, warehouseName);
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

	public static async Task<Guid?> GetCustomerIdAsync()
	{
		try
		{
			var value = await SecureStorage.GetAsync(CustomerIdKey);
			return Guid.TryParse(value, out var id) ? id : null;
		}
		catch
		{
			return null;
		}
	}

	public static async Task<string?> GetCustomerNameAsync()
	{
		try
		{
			return await SecureStorage.GetAsync(CustomerNameKey);
		}
		catch
		{
			return null;
		}
	}

	public static async Task<Guid?> GetWarehouseIdAsync()
	{
		try
		{
			var value = await SecureStorage.GetAsync(WarehouseIdKey);
			return Guid.TryParse(value, out var id) ? id : null;
		}
		catch
		{
			return null;
		}
	}

	public static async Task<string?> GetWarehouseNameAsync()
	{
		try
		{
			return await SecureStorage.GetAsync(WarehouseNameKey);
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
			SecureStorage.Remove(CustomerIdKey);
			SecureStorage.Remove(CustomerNameKey);
			SecureStorage.Remove(WarehouseIdKey);
			SecureStorage.Remove(WarehouseNameKey);
			await Task.CompletedTask;
		}
		catch
		{
			await Task.CompletedTask;
		}
	}
}

