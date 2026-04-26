using AspNetWeek1.Api.Models;

namespace AspNetWeek1.Api.Services;

public class CustomerService
{
    private readonly List<Customer> _customers =
    [
        new Customer { Id = 1, Name = "Nguyen Van A", Email = "a@gmail.com", Phone = "0123456789", Address = "HCM", IsActive = true },
        new Customer { Id = 2, Name = "Tran Thi B", Email = "b@gmail.com", Phone = "0987654321", Address = "Ha Noi", IsActive = false },
        new Customer { Id = 3, Name = "Le Van C", Email = "c@gmail.com", Phone = "0111222333", Address = "Da Nang", IsActive = true },
        new Customer { Id = 4, Name = "Pham Thi D", Email = "d@gmail.com", Phone = "0222333444", Address = "Can Tho", IsActive = true },
        new Customer { Id = 5, Name = "Hoang Van E", Email = "e@gmail.com", Phone = "0333444555", Address = "Hue", IsActive = false },
        new Customer { Id = 6, Name = "Vu Thi F", Email = "f@gmail.com", Phone = "0444555666", Address = "Nha Trang", IsActive = true },
        new Customer { Id = 7, Name = "Do Van G", Email = "g@gmail.com", Phone = "0555666777", Address = "Vinh", IsActive = true },
        new Customer { Id = 8, Name = "Bui Thi H", Email = "h@gmail.com", Phone = "0666777888", Address = "Da Lat", IsActive = true }
    ];

    // ===== READ =====
    public List<Customer> GetAll() => _customers;

    public Customer? GetById(int id)
        => _customers.FirstOrDefault(x => x.Id == id);

    // ===== CREATE =====
    public Customer Add(Customer customer)
    {
        var newId = _customers.Max(x => x.Id) + 1;
        customer.Id = newId;
        _customers.Add(customer);
        return customer;
    }

    // ===== UPDATE =====
    public bool Update(int id, Customer updatedCustomer)
    {
        var customer = GetById(id);
        if (customer == null) return false;

        customer.Name = updatedCustomer.Name;
        customer.Email = updatedCustomer.Email;
        customer.Phone = updatedCustomer.Phone;
        customer.Address = updatedCustomer.Address;
        customer.IsActive = updatedCustomer.IsActive;

        return true;
    }

    // ===== DELETE (soft delete) =====
    public bool Delete(int id)
    {
        var customer = GetById(id);
        if (customer == null) return false;

        customer.IsActive = false;
        return true;
    }

    // ===== FILTER =====
    public List<Customer> Search(string? name, string? email)
    {
        var query = _customers.AsQueryable();

        if (!string.IsNullOrEmpty(name))
            query = query.Where(x => x.Name.ToLower().Contains(name.ToLower()));

        if (!string.IsNullOrEmpty(email))
            query = query.Where(x => x.Email.ToLower().Contains(email.ToLower()));

        return query.ToList();
    }

    // ===== STATS =====
    public object GetStats()
    {
        var total = _customers.Count;
        var active = _customers.Count(x => x.IsActive);
        var inactive = total - active;

        return new
        {
            TotalCustomers = total,
            ActiveCustomers = active,
            InactiveCustomers = inactive
        };
    }

    // ===== STATUS =====
    public string GetCustomerStatus(bool isActive)
    {
        return isActive ? "Active" : "Inactive";
    }
}