using System.Collections.Generic;
using System.Threading.Tasks;
using ContactManager.Models;

namespace ContactManager.Repositories
{
    public interface IContactRepository
    {
        Task<List<Contact>> GetAllAsync();
        Task CreateAsync(Contact contact);
        Task DeleteAsync(long id);
    }
}
