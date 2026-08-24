namespace InvoiceSystem.Models;

public class Product{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public string Category { get; set; } = string.Empty;

    public decimal UnitPrice { get; set; }

    public bool IsHazardous { get; set; }

    public bool IsDiscountEligible { get; set; }
}