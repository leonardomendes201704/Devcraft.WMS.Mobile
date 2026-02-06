namespace DevcraftWMS.Mobile;

public partial class OutboundCheckExecutionPage : ContentPage
{
	readonly OutboundCheckDetailViewModel _viewModel;

	public OutboundCheckExecutionPage(OutboundCheckQueuePage.OutboundCheckDetail detail)
	{
		InitializeComponent();
		_viewModel = new OutboundCheckDetailViewModel(detail);
		BindingContext = _viewModel;
	}

	public sealed class OutboundCheckDetailViewModel
	{
		public string HeaderText { get; }
		public string StatusText { get; }
		public string PriorityText { get; }
		public IReadOnlyList<ItemView> Items { get; }

		public OutboundCheckDetailViewModel(OutboundCheckQueuePage.OutboundCheckDetail detail)
		{
			HeaderText = $"{detail.OrderNumber} | {detail.WarehouseName}";
			StatusText = $"Status: {OutboundCheckQueuePage.GetStatusText(detail.Status)}";
			PriorityText = $"Prioridade: {OutboundCheckQueuePage.GetPriorityText(detail.Priority)}";
			Items = detail.Items.Select(i => new ItemView(i)).ToList();
		}
	}

	public sealed class ItemView
	{
		public string ProductCode { get; }
		public string ProductName { get; }
		public string QuantityText { get; }
		public string DivergenceText { get; }

		public ItemView(OutboundCheckQueuePage.OutboundCheckItem item)
		{
			ProductCode = item.ProductCode;
			ProductName = item.ProductName;
			QuantityText = $"Esperado: {item.QuantityExpected} {item.UomCode} | Conferido: {item.QuantityChecked}";
			DivergenceText = string.IsNullOrWhiteSpace(item.DivergenceReason)
				? "Sem divergência"
				: $"Divergência: {item.DivergenceReason}";
		}
	}
}
