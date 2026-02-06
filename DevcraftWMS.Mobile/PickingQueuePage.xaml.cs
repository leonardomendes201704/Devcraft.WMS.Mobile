using System.Collections.ObjectModel;

namespace DevcraftWMS.Mobile;

public partial class PickingQueuePage : ContentPage
{
	const string CustomerHeader = "X-Customer-Id";
	readonly ObservableCollection<PickingTaskItem> _items = new();
	readonly List<PickingTaskItem> _allItems = new();
	readonly List<StatusOption> _statusOptions = new()
	{
		new StatusOption("Pending", 0),
		new StatusOption("InProgress", 1),
		new StatusOption("Completed", 2),
		new StatusOption("Reassigned", 3),
		new StatusOption("Canceled", 4),
		new StatusOption("All", null),
	};

	public PickingQueuePage()
	{
		InitializeComponent();
		TasksList.ItemsSource = _items;
		StatusPicker.ItemsSource = _statusOptions;
		StatusPicker.ItemDisplayBinding = new Binding(nameof(StatusOption.Label));
		StatusPicker.SelectedItem = _statusOptions[0];
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

			var status = StatusPicker.SelectedItem as StatusOption;
			var isActive = ActiveSwitch.IsToggled;
			var statusParam = status?.Value is null ? "" : $"&status={status.Value}";
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

		if (item.Status != 0)
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

	public sealed class PickingTaskItem
	{
		public Guid Id { get; init; }
		public Guid OutboundOrderId { get; init; }
		public Guid WarehouseId { get; init; }
		public string OrderNumber { get; init; } = string.Empty;
		public string WarehouseName { get; init; } = string.Empty;
		public int Status { get; init; }
		public int Sequence { get; init; }
		public Guid? AssignedUserId { get; init; }
		public bool IsActive { get; init; }
		public DateTime CreatedAtUtc { get; init; }
		public string StatusText => GetStatusText(Status);
	}

	public sealed class PickingTaskDetail
	{
		public Guid Id { get; init; }
		public Guid OutboundOrderId { get; init; }
		public Guid WarehouseId { get; init; }
		public string OrderNumber { get; init; } = string.Empty;
		public string WarehouseName { get; init; } = string.Empty;
		public int Status { get; init; }
		public int Sequence { get; init; }
		public Guid? AssignedUserId { get; init; }
		public string? Notes { get; init; }
		public DateTime? StartedAtUtc { get; init; }
		public DateTime? CompletedAtUtc { get; init; }
		public bool IsActive { get; init; }
		public DateTime CreatedAtUtc { get; init; }
		public string StatusText => GetStatusText(Status);
	}

	static string GetStatusText(int status) => status switch
	{
		0 => "Pending",
		1 => "InProgress",
		2 => "Completed",
		3 => "Reassigned",
		4 => "Canceled",
		_ => $"Unknown ({status})"
	};

	public sealed record StatusOption(string Label, int? Value);
}

