using InvoiceSystem.Models;

namespace InvoiceSystem.DTOs;

public class CreateOrderDTO
{
    public int CustomerId { get; set; }
    public List<CreateOrderItemDTO> Items { get; set; } = null!;
}

public class CreateOrderItemDTO
{
    public int ProductId { get; set; }
    public int Quantity { get; set; }
}