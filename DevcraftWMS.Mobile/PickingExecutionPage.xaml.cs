using System.Collections.ObjectModel;

namespace DevcraftWMS.Mobile;

[QueryProperty(nameof(TaskId), "taskId")]
public partial class PickingExecutionPage : ContentPage
{
	const string CustomerHeader = "X-Customer-Id";
	readonly ObservableCollection<PickingTaskItem> _items = new();
	readonly Dictionary<Guid, decimal> _pickedByItemId = new();
	Guid _taskId;
	int _currentIndex;
	PickingTaskDetail? _task;

	public string? TaskId
	{
		get => _taskId == Guid.Empty ? null : _taskId.ToString();
		set
		{
			if (Guid.TryParse(value, out var parsed))
			{
				_taskId = parsed;
			}
		}
	}

	public string HeaderText => _task is null
		? "Carregando..."
		: $"Tarefa: {_task.OrderNumber} | {_task.WarehouseName}";

	public string ItemSummary => CurrentItem is null
		? string.Empty
		: $"{CurrentItem.ProductCode} - {CurrentItem.ProductName}";

	public string LocationSummary => CurrentItem is null
		? string.Empty
		: $"Local: {CurrentItem.LocationCode ?? "-"} | Lote: {CurrentItem.LotCode ?? "-"}";

	public string QuantitySummary => CurrentItem is null
		? string.Empty
		: $"Qtd planejada: {CurrentItem.QuantityPlanned} | Qtd separada: {GetPicked(CurrentItem.Id)}";

	PickingTaskItem? CurrentItem => _items.Count == 0 || _currentIndex < 0 || _currentIndex >= _items.Count
		? null
		: _items[_currentIndex];

	public PickingExecutionPage()
	{
		InitializeComponent();
		BindingContext = this;
	}

	protected override async void OnAppearing()
	{
		base.OnAppearing();
		await LoadAsync();
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

			var headers = new Dictionary<string, string?> { [CustomerHeader] = customerId.Value.ToString() };
			var result = await ApiClient.GetAsync<PickingTaskDetail>($"api/picking-tasks/{_taskId}", headers);
			if (result is null)
			{
				StatusLabel.Text = "Não foi possível carregar a tarefa.";
				return;
			}

			_task = result;
			_items.Clear();
			foreach (var item in result.Items)
			{
				_items.Add(item);
				_pickedByItemId[item.Id] = item.QuantityPicked;
			}

			_currentIndex = 0;
			ResetInputs();
			UpdateBindings();
		}
		catch (Exception ex)
		{
			StatusLabel.Text = $"Erro ao carregar: {ex.Message}";
		}
	}

	void UpdateBindings()
	{
		OnPropertyChanged(nameof(HeaderText));
		OnPropertyChanged(nameof(ItemSummary));
		OnPropertyChanged(nameof(LocationSummary));
		OnPropertyChanged(nameof(QuantitySummary));
	}

	void ResetInputs()
	{
		LocationEntry.Text = CurrentItem?.LocationCode ?? string.Empty;
		SkuEntry.Text = CurrentItem?.ProductCode ?? string.Empty;
		LotEntry.Text = CurrentItem?.LotCode ?? string.Empty;
		QuantityEntry.Text = CurrentItem is null ? string.Empty : CurrentItem.QuantityPlanned.ToString("0.##");
		PartialCheck.IsChecked = false;
		ReasonEntry.Text = string.Empty;
	}

	decimal GetPicked(Guid itemId) => _pickedByItemId.TryGetValue(itemId, out var value) ? value : 0m;

	bool ValidateScans(PickingTaskItem item, out string message)
	{
		var locationInput = (LocationEntry.Text ?? string.Empty).Trim();
		var skuInput = (SkuEntry.Text ?? string.Empty).Trim();
		var lotInput = (LotEntry.Text ?? string.Empty).Trim();

		if (!string.IsNullOrWhiteSpace(item.LocationCode) &&
			!string.Equals(item.LocationCode, locationInput, StringComparison.OrdinalIgnoreCase))
		{
			message = "Localização não confere.";
			return false;
		}

		if (!string.Equals(item.ProductCode, skuInput, StringComparison.OrdinalIgnoreCase))
		{
			message = "Produto/SKU não confere.";
			return false;
		}

		if (!string.IsNullOrWhiteSpace(item.LotCode) &&
			!string.Equals(item.LotCode, lotInput, StringComparison.OrdinalIgnoreCase))
		{
			message = "Lote não confere.";
			return false;
		}

		message = string.Empty;
		return true;
	}

	async void OnConfirmItemClicked(object? sender, EventArgs e)
	{
		StatusLabel.Text = string.Empty;
		var item = CurrentItem;
		if (item is null)
		{
			StatusLabel.Text = "Nenhum item disponível.";
			return;
		}

		if (!ValidateScans(item, out var validationMessage))
		{
			await DisplayAlert("Validação", validationMessage, "OK");
			return;
		}

		if (!decimal.TryParse(QuantityEntry.Text?.Replace(',', '.'), System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out var qty))
		{
			await DisplayAlert("Validação", "Quantidade inválida.", "OK");
			return;
		}

		if (qty < 0)
		{
			await DisplayAlert("Validação", "Quantidade não pode ser negativa.", "OK");
			return;
		}

		if (qty > item.QuantityPlanned)
		{
			await DisplayAlert("Validação", "Quantidade maior que o planejado.", "OK");
			return;
		}

		if (qty < item.QuantityPlanned && !PartialCheck.IsChecked)
		{
			await DisplayAlert("Validação", "Marque Parcial para separar quantidade menor.", "OK");
			return;
		}

		if (qty < item.QuantityPlanned && string.IsNullOrWhiteSpace(ReasonEntry.Text))
		{
			await DisplayAlert("Validação", "Informe o motivo da falta.", "OK");
			return;
		}

		_pickedByItemId[item.Id] = qty;
		await SendConfirmAsync();

		if (_currentIndex < _items.Count - 1)
		{
			_currentIndex++;
			ResetInputs();
			UpdateBindings();
		}
		else
		{
			UpdateBindings();
			await DisplayAlert("Concluído", "Todos os itens foram confirmados. Use Finalizar para concluir a tarefa.", "OK");
		}
	}

	async void OnFinishClicked(object? sender, EventArgs e)
	{
		await SendConfirmAsync();
		await DisplayAlert("Finalizado", "Tarefa atualizada com sucesso.", "OK");
		await Shell.Current.GoToAsync("..");
	}

	async Task SendConfirmAsync()
	{
		var customerId = await AuthStorage.GetCustomerIdAsync();
		if (customerId is null)
		{
			StatusLabel.Text = "Contexto de cliente não definido.";
			return;
		}

		var headers = new Dictionary<string, string?> { [CustomerHeader] = customerId.Value.ToString() };
		var payload = new ConfirmPickingTaskRequest(
			_pickedByItemId.Select(kvp => new ConfirmPickingTaskItemRequest(kvp.Key, kvp.Value)).ToList(),
			ReasonEntry.Text);

		try
		{
			await ApiClient.PostAsync<PickingTaskDetail>($"api/picking-tasks/{_taskId}/confirm", payload, headers);
		}
		catch (Exception ex)
		{
			StatusLabel.Text = $"Erro ao confirmar: {ex.Message}";
		}
	}

	public sealed class PickingTaskItem
	{
		public Guid Id { get; init; }
		public Guid OutboundOrderItemId { get; init; }
		public Guid ProductId { get; init; }
		public Guid UomId { get; init; }
		public Guid? LotId { get; init; }
		public Guid? LocationId { get; init; }
		public string ProductCode { get; init; } = string.Empty;
		public string ProductName { get; init; } = string.Empty;
		public string UomCode { get; init; } = string.Empty;
		public string? LotCode { get; init; }
		public string? LocationCode { get; init; }
		public decimal QuantityPlanned { get; init; }
		public decimal QuantityPicked { get; init; }
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
		public IReadOnlyList<PickingTaskItem> Items { get; init; } = Array.Empty<PickingTaskItem>();
	}

	public sealed record ConfirmPickingTaskItemRequest(Guid PickingTaskItemId, decimal QuantityPicked);
	public sealed record ConfirmPickingTaskRequest(IReadOnlyList<ConfirmPickingTaskItemRequest> Items, string? Notes);
}
