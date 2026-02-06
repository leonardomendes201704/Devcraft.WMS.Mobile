using System.Collections.ObjectModel;

namespace DevcraftWMS.Mobile;

public partial class OutboundCheckQueuePage : ContentPage
{
	const string CustomerHeader = "X-Customer-Id";
	readonly ObservableCollection<OutboundCheckListItem> _items = new();
	readonly List<OutboundCheckListItem> _allItems = new();
	readonly List<StatusOption> _statusOptions = new()
	{
		new StatusOption("Pending", 0),
		new StatusOption("InProgress", 1),
		new StatusOption("Completed", 2),
		new StatusOption("Canceled", 3),
		new StatusOption("All", null),
	};
	readonly List<PriorityOption> _priorityOptions = new()
	{
		new PriorityOption("Low", 0),
		new PriorityOption("Normal", 1),
		new PriorityOption("High", 2),
		new PriorityOption("Urgent", 3),
		new PriorityOption("All", null),
	};

	public OutboundCheckQueuePage()
	{
		InitializeComponent();
		TasksList.ItemsSource = _items;
		StatusPicker.ItemsSource = _statusOptions;
		StatusPicker.ItemDisplayBinding = new Binding(nameof(StatusOption.Label));
		StatusPicker.SelectedItem = _statusOptions[0];
		PriorityPicker.ItemsSource = _priorityOptions;
		PriorityPicker.ItemDisplayBinding = new Binding(nameof(PriorityOption.Label));
		PriorityPicker.SelectedItem = _priorityOptions[^1];
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
				await DisplayAlert("Contexto obrigatório", "Selecione um cliente para carregar a fila de conferência.", "OK");
				return;
			}

			var warehouseId = await AuthStorage.GetWarehouseIdAsync();
			var status = StatusPicker.SelectedItem as StatusOption;
			var priority = PriorityPicker.SelectedItem as PriorityOption;
			var isActive = ActiveSwitch.IsToggled;

			var statusParam = status?.Value is null ? "" : $"&status={status.Value}";
			var priorityParam = priority?.Value is null ? "" : $"&priority={priority.Value}";
			var warehouseParam = warehouseId is null ? "" : $"&warehouseId={warehouseId}";
			var query = $"api/outbound-checks?pageNumber=1&pageSize=50&isActive={isActive}&orderBy=CreatedAtUtc&orderDir=desc{statusParam}{priorityParam}{warehouseParam}";

			var headers = new Dictionary<string, string?> { [CustomerHeader] = customerId.Value.ToString() };
			var result = await ApiClient.GetAsync<PagedResult<OutboundCheckListItem>>(query, headers);

			_allItems.Clear();
			_allItems.AddRange(result?.Items ?? Array.Empty<OutboundCheckListItem>());
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

	async void OnRefreshClicked(object? sender, EventArgs e) => await LoadAsync();
	void OnSearchChanged(object? sender, TextChangedEventArgs e) => ApplySearch();
	async void OnFilterChanged(object? sender, EventArgs e) => await LoadAsync();

	async void OnStartClicked(object? sender, EventArgs e)
	{
		if (sender is not Button btn || btn.CommandParameter is not OutboundCheckListItem item)
			return;

		if (item.Status != 0)
		{
			await DisplayAlert("Bloqueado", "Só é possível iniciar tarefas em status Pending.", "OK");
			return;
		}

		var customerId = await AuthStorage.GetCustomerIdAsync();
		if (customerId is null)
		{
			await DisplayAlert("Erro", "Contexto de cliente não definido.", "OK");
			return;
		}

		try
		{
			var headers = new Dictionary<string, string?> { [CustomerHeader] = customerId.Value.ToString() };
			var result = await ApiClient.PostAsync<OutboundCheckDetail>($"api/outbound-checks/{item.Id}/start", null, headers);
			if (result is not null)
			{
				await Navigation.PushAsync(new OutboundCheckExecutionPage(result));
			}
		}
		catch (Exception ex)
		{
			await DisplayAlert("Erro", ex.Message, "OK");
		}
	}

	public sealed record PagedResult<T>(IReadOnlyList<T> Items, int TotalCount, int PageNumber, int PageSize, string OrderBy, string OrderDir);

	public sealed class OutboundCheckListItem
	{
		public Guid Id { get; init; }
		public Guid OutboundOrderId { get; init; }
		public Guid WarehouseId { get; init; }
		public string OrderNumber { get; init; } = string.Empty;
		public string WarehouseName { get; init; } = string.Empty;
		public int Status { get; init; }
		public int Priority { get; init; }
		public int ItemsCount { get; init; }
		public DateTime CreatedAtUtc { get; init; }
		public bool IsActive { get; init; }
		public string StatusText => GetStatusText(Status);
		public Color StatusColor => GetStatusColor(Status);
		public string PriorityText => GetPriorityText(Priority);
		public Color PriorityColor => GetPriorityColor(Priority);
		public string ItemsLabel => $"Itens: {ItemsCount}";
		public string CreatedAtLabel => $"Criado: {CreatedAtUtc:dd/MM/yyyy HH:mm}";
	}

	public sealed class OutboundCheckDetail
	{
		public Guid Id { get; init; }
		public Guid OutboundOrderId { get; init; }
		public Guid WarehouseId { get; init; }
		public string OrderNumber { get; init; } = string.Empty;
		public string WarehouseName { get; init; } = string.Empty;
		public int Status { get; init; }
		public int Priority { get; init; }
		public Guid? StartedByUserId { get; init; }
		public DateTime? StartedAtUtc { get; init; }
		public Guid? CheckedByUserId { get; init; }
		public DateTime? CheckedAtUtc { get; init; }
		public string? Notes { get; init; }
		public IReadOnlyList<OutboundCheckItem> Items { get; init; } = Array.Empty<OutboundCheckItem>();
	}

	public sealed class OutboundCheckItem
	{
		public Guid Id { get; init; }
		public Guid OutboundOrderItemId { get; init; }
		public Guid ProductId { get; init; }
		public Guid UomId { get; init; }
		public string ProductCode { get; init; } = string.Empty;
		public string ProductName { get; init; } = string.Empty;
		public string UomCode { get; init; } = string.Empty;
		public decimal QuantityExpected { get; init; }
		public decimal QuantityChecked { get; init; }
		public string? DivergenceReason { get; init; }
		public int EvidenceCount { get; init; }
	}

	public static string GetStatusText(int status) => status switch
	{
		0 => "Pending",
		1 => "InProgress",
		2 => "Completed",
		3 => "Canceled",
		_ => $"Unknown ({status})"
	};

	static Color GetStatusColor(int status) => status switch
	{
		0 => Color.FromArgb("#F59E0B"),
		1 => Color.FromArgb("#3B82F6"),
		2 => Color.FromArgb("#10B981"),
		3 => Color.FromArgb("#EF4444"),
		_ => Color.FromArgb("#6B7280")
	};

	public static string GetPriorityText(int priority) => priority switch
	{
		0 => "Low",
		1 => "Normal",
		2 => "High",
		3 => "Urgent",
		_ => $"Unknown ({priority})"
	};

	static Color GetPriorityColor(int priority) => priority switch
	{
		0 => Color.FromArgb("#6B7280"),
		1 => Color.FromArgb("#2563EB"),
		2 => Color.FromArgb("#F97316"),
		3 => Color.FromArgb("#DC2626"),
		_ => Color.FromArgb("#6B7280")
	};

	public sealed record StatusOption(string Label, int? Value);
	public sealed record PriorityOption(string Label, int? Value);
}
