using InvoiceSystem.Data;
using InvoiceSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace InvoiceSystem.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CustomersController : ControllerBase
{
    private readonly DBContext _context;

    public CustomersController(DBContext context)
    {
        _context = context;
    }

    //GET /api/customers
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Customer>>> GetCustomers()
    {
        var customers = await _context.Customers.ToListAsync();
        return Ok(customers);
    }

    //POST /api/customers
    [HttpPost]
    public async Task<ActionResult<Customer>> CreateProduct(Customer customer)
    {
        _context.Customers.Add(customer);
        await _context.SaveChangesAsync();

        return CreatedAtAction(
            nameof(GetCustomers),
            new { id = customer.Id },
            customer
        );
    }
}