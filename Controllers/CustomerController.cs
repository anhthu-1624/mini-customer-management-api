using AspNetWeek1.Api.Models;
using AspNetWeek1.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace AspNetWeek1.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CustomersController : ControllerBase
{
    private readonly CustomerService _customerService;

    public CustomersController(CustomerService customerService)
    {
        _customerService = customerService;
    }

    // ===== GET ALL =====
    [HttpGet]
    public IActionResult GetAll()
    {
        var customers = _customerService.GetAll()
            .Select(c => new
            {
                c.Id,
                c.Name,
                c.Email,
                c.Phone,
                c.Address,
                c.IsActive,
                Status = _customerService.GetCustomerStatus(c.IsActive)
            });

        return Ok(customers);
    }

    // ===== GET BY ID =====
    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        var c = _customerService.GetById(id);
        if (c == null) return NotFound();

        return Ok(new
        {
            c.Id,
            c.Name,
            c.Email,
            c.Phone,
            c.Address,
            c.IsActive,
            Status = _customerService.GetCustomerStatus(c.IsActive)
        });
    }

    // ===== CREATE =====
    [HttpPost]
    public IActionResult Create(Customer customer)
    {
        var newCustomer = _customerService.Add(customer);
        return Ok(newCustomer);
    }

    // ===== UPDATE =====
    [HttpPut("{id}")]
    public IActionResult Update(int id, Customer customer)
    {
        var result = _customerService.Update(id, customer);
        if (!result) return NotFound();

        return Ok("Updated successfully");
    }

    // ===== DELETE =====
    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var result = _customerService.Delete(id);
        if (!result) return NotFound();

        return Ok("Deleted (soft) successfully");
    }

    // ===== SEARCH =====
    [HttpGet("search")]
    public IActionResult Search(string? name, string? email)
    {
        var result = _customerService.Search(name, email)
            .Select(c => new
            {
                c.Id,
                c.Name,
                c.Email,
                c.Phone,
                c.Address,
                c.IsActive,
                Status = _customerService.GetCustomerStatus(c.IsActive)
            });

        return Ok(result);
    }

    // ===== STATS =====
    [HttpGet("stats")]
    public IActionResult GetStats()
    {
        return Ok(_customerService.GetStats());
    }
}