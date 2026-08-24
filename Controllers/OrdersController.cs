namespace InvoiceSystem.Controllers;

using InvoiceSystem.Data;
using InvoiceSystem.DTOs;
using InvoiceSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/[controller]")]
public class OrdersController : ControllerBase
{
    private readonly DBContext _context;

    public OrdersController(DBContext context)
    {
        _context = context;
    }

    //GET /api/orders
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Order>>> GetOrders()
    {
        var orders = await _context.Orders.Include(o => o.Customer).Include(o => o.Items).ThenInclude(i => i.Product).ToListAsync();
        return Ok(orders);
    }

    //POST /api/orders
    [HttpPost]
    public async Task<ActionResult<Order>> CreateOrder(CreateOrderDTO dto)
    {
        var customer = await _context.Customers.FindAsync(dto.CustomerId);

        // Check if customer exists
        if (customer == null)
        {
            return BadRequest("Customer not found");
        }

        // Check if order contains any item
        if (dto.Items.Count == 0)
        {
            return BadRequest("Order must contain at least one item");
        }

        var order = new Order
        {
            CustomerID = dto.CustomerId,
            OrderDate = DateTime.UtcNow,
            Total = 0,
        };

        foreach (var item in dto.Items)
        {
            if (item.Quantity <= 0)
            {
                return BadRequest("Quantity must be greater than 0");
            }

            var product = await _context.Products.FindAsync(item.ProductId);

            if (product == null)
            {
                return BadRequest($"Product with ID: {item.ProductId} not found");
            }

            decimal discount = product.IsDiscountEligible ? 20m : 0;

            decimal discountedUnitPrice = product.UnitPrice * (1 - discount / 100);

            var orderItem = new OrderItem
            {
                Quantity = item.Quantity,
                ProductId = product.Id,
                Product = product,
                UnitPrice = product.UnitPrice,
                Discount = discount
            };

            order.Items.Add(orderItem);

            order.Total += discountedUnitPrice * item.Quantity;
        }

        _context.Orders.Add(order);
        await _context.SaveChangesAsync();

        return CreatedAtAction(
            nameof(GetOrders),
            new { id = order.Id },
            order
        );
    }
}