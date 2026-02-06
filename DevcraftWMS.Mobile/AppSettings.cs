namespace DevcraftWMS.Mobile;

public static class AppSettings
{
	const string UseDeviceUrlKey = "use_device_url";
	const string DeviceUrlKey = "device_api_url";

	public static bool UseDeviceUrl
	{
		get => Preferences.Get(UseDeviceUrlKey, false);
		set => Preferences.Set(UseDeviceUrlKey, value);
	}

	public static string DeviceApiUrl
	{
		get => Preferences.Get(DeviceUrlKey, "http://192.168.0.196:5137");
		set => Preferences.Set(DeviceUrlKey, value);
	}
}

