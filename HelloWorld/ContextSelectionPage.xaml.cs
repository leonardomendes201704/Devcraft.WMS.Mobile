using System.Collections.ObjectModel;

namespace HelloWorld;

public partial class ContextSelectionPage : ContentPage
{
	readonly ObservableCollection<CustomerItem> _customers = new();
	readonly ObservableCollection<WarehouseItem> _warehouses = new();

	public ContextSelectionPage()
	{
		InitializeComponent();
		CustomerPicker.ItemsSource = _customers;
		CustomerPicker.ItemDisplayBinding = new Binding(nameof(CustomerItem.Name));
		WarehousePicker.ItemsSource = _warehouses;
		WarehousePicker.ItemDisplayBinding = new Binding(nameof(WarehouseItem.Name));
		Loaded += async (_, _) => await LoadAsync();
	}

	async Task LoadAsync()
	{
		StatusLabel.Text = string.Empty;
		ContinueBtn.IsEnabled = false;

		try
		{
			var customers = await ApiClient.GetAsync<PagedResult<CustomerItem>>("api/customers?pageNumber=1&pageSize=200");
			_customers.Clear();
			foreach (var c in customers?.Items ?? Array.Empty<CustomerItem>())
			{
				_customers.Add(c);
			}

			if (_customers.Count > 0)
				CustomerPicker.SelectedIndex = 0;

			var warehouses = await ApiClient.GetAsync<PagedResult<WarehouseItem>>("api/warehouses?pageNumber=1&pageSize=200");
			_warehouses.Clear();
			foreach (var w in warehouses?.Items ?? Array.Empty<WarehouseItem>())
			{
				_warehouses.Add(w);
			}

			if (_warehouses.Count > 0)
				WarehousePicker.SelectedIndex = 0;

			ContinueBtn.IsEnabled = true;
		}
		catch (Exception ex)
		{
			StatusLabel.Text = $"Falha ao carregar contexto: {ex.Message}";
		}
	}

	async void OnContinueClicked(object? sender, EventArgs e)
	{
		StatusLabel.Text = string.Empty;
		if (CustomerPicker.SelectedItem is not CustomerItem customer || WarehousePicker.SelectedItem is not WarehouseItem warehouse)
		{
			StatusLabel.Text = "Selecione cliente e armazém.";
			return;
		}

		await AuthStorage.SaveContextAsync(customer.Id, customer.Name, warehouse.Id, warehouse.Name);
		await Shell.Current.GoToAsync($"//{nameof(WelcomePage)}");
	}

	public sealed record PagedResult<T>(IReadOnlyList<T> Items, int TotalCount, int PageNumber, int PageSize, string OrderBy, string OrderDir);
	public sealed record CustomerItem(Guid Id, string Name, string Email, DateOnly DateOfBirth, DateTime CreatedAtUtc);
	public sealed record WarehouseItem(Guid Id, string Code, string Name, string WarehouseType, bool IsPrimary, bool IsActive, string? City, string? State, string? Country, DateTime CreatedAtUtc);
}
