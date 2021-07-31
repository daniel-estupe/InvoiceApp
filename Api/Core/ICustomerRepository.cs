using System.Collections.Generic;
using System.Threading.Tasks;
using Api.Models;

namespace Api.Core
{
    public interface ICustomerRepository
    {
         Task<ICollection<Customer>> getAll(string filter);
    }
}