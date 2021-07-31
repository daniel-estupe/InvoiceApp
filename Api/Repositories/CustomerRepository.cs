using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Core;
using Api.Data;
using Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Api.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly InvoiceContext _context;

        public CustomerRepository(InvoiceContext context)
        {
            _context = context;
        }

        public async Task<ICollection<Customer>> getAll(string filter = "")
        {
            if (filter=="" || filter==null)
                return await _context.Customers.ToListAsync();
            
            return await _context.Customers
                .Where(c => c.BillingNo.Equals(filter) || c.Name.Contains(filter))
                .ToListAsync();
                
        }
    }
}