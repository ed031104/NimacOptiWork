using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.Repositories
{
    public interface IRepositoryInvoice
    {

        Task<Domain.Models.Invoice> GetInvoiceByIdAsync(int invoiceId);
        Task<IEnumerable<Domain.Models.Invoice>> GetAllInvoicesAsync();
        Task<IEnumerable<Domain.Models.Invoice>> GetAllInvoicesAsync(int pageNumber, int pageSize);
        Task AddInvoiceAsync(Domain.Models.Invoice invoice);
        Task UpdateInvoiceAsync(Domain.Models.Invoice invoice);
        Task DeleteInvoiceAsync(int invoiceId);
        Task<int> Count();
    }
}
