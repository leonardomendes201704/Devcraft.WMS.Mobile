using System.Collections.ObjectModel;

namespace HelloWorld;

public partial class PickingQueuePage : ContentPage
{
	const string CustomerHeader = "X-Customer-Id";
	readonly ObservableCollection<PickingTaskItem> _items = new();
	readonly List<PickingTaskItem> _allItems = new();

	public PickingQueuePage()
	{
		InitializeComponent();
		TasksList.ItemsSource = _items;
		StatusPicker.ItemsSource = new List<string> { "Pending", "InProgress", "Completed", "Reassigned", "Canceled", "All" };
		StatusPicker.SelectedItem = "Pending";
		ActiveSwitch.IsToggled = true;
		Loaded += async (_, _) => await LoadAsync();
	}

	async Task LoadAsync()
	{
		StatusLabel.Text = string.Empty;
		try
		{
			var customerId = await AuthStorage.GetCustomerIdAsync();
			if (customerId is null)
			{
				StatusLabel.Text = "Contexto de cliente não definido.";
				return;
			}

			var status = StatusPicker.SelectedItem?.ToString();
			var isActive = ActiveSwitch.IsToggled;
			var statusParam = status == "All" ? "" : $"&status={status}";
			var query = $"api/picking-tasks?pageNumber=1&pageSize=50&isActive={isActive}&orderBy=Sequence&orderDir=asc{statusParam}";

			var headers = new Dictionary<string, string?> { [CustomerHeader] = customerId.Value.ToString() };
			var result = await ApiClient.GetAsync<PagedResult<PickingTaskItem>>(query, headers);

			_allItems.Clear();
			_allItems.AddRange(result?.Items ?? Array.Empty<PickingTaskItem>());
			ApplySearch();
		}
		catch (Exception ex)
		{
			StatusLabel.Text = $"Erro ao carregar: {ex.Message}";
		}
	}

	void ApplySearch()
	{
		var term = SearchEntry.Text?.Trim() ?? string.Empty;
		var filtered = string.IsNullOrWhiteSpace(term)
			? _allItems
			: _allItems.Where(i => i.OrderNumber.Contains(term, StringComparison.OrdinalIgnoreCase)).ToList();

		_items.Clear();
		foreach (var item in filtered)
		{
			_items.Add(item);
		}
	}

	async void OnRefreshClicked(object? sender, EventArgs e)	=> await LoadAsync();
	void OnSearchChanged(object? sender, TextChangedEventArgs e) => ApplySearch();
	async void OnFilterChanged(object? sender, EventArgs e) => await LoadAsync();

	async void OnStartClicked(object? sender, EventArgs e)
	{
		if (sender is not Button btn || btn.CommandParameter is not PickingTaskItem item)
			return;

		if (!string.Equals(item.Status, "Pending", StringComparison.OrdinalIgnoreCase))
		{
			await DisplayAlertAsync("Bloqueado", "Só é possível iniciar tarefas em status Pending.", "OK");
			return;
		}

		var customerId = await AuthStorage.GetCustomerIdAsync();
		if (customerId is null)
		{
			await DisplayAlertAsync("Erro", "Contexto de cliente não definido.", "OK");
			return;
		}

		try
		{
			var headers = new Dictionary<string, string?> { [CustomerHeader] = customerId.Value.ToString() };
			var result = await ApiClient.PostAsync<PickingTaskDetail>($"api/picking-tasks/{item.Id}/start", null, headers);
			if (result is not null)
			{
				await DisplayAlertAsync("Iniciado", "Tarefa iniciada com sucesso.", "OK");
				await LoadAsync();
			}
		}
		catch (Exception ex)
		{
			await DisplayAlertAsync("Erro", ex.Message, "OK");
		}
	}

	public sealed record PagedResult<T>(IReadOnlyList<T> Items, int TotalCount, int PageNumber, int PageSize, string OrderBy, string OrderDir);
	public sealed record PickingTaskItem(Guid Id, Guid OutboundOrderId, Guid WarehouseId, string OrderNumber, string WarehouseName, string Status, int Sequence, Guid? AssignedUserId, bool IsActive, DateTime CreatedAtUtc);
	public sealed record PickingTaskDetail(Guid Id, Guid OutboundOrderId, Guid WarehouseId, string OrderNumber, string WarehouseName, string Status, int Sequence, Guid? AssignedUserId, string? Notes, DateTime? StartedAtUtc, DateTime? CompletedAtUtc, bool IsActive, DateTime CreatedAtUtc);
}
