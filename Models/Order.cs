namespace InvoiceSystem.Models;

public class Order
{
    public int Id { get; set; }
    public int CustomerID { get; set; }
    public Customer? Customer { get; set; }
    public DateTime OrderDate { get; set; }
    public List<OrderItem> Items { get; set; } = new();
    public decimal Total { get; set; }
}