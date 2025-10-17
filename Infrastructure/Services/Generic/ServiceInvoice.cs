using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Generic
{
    public class ServiceInvoice : IServicesInvoice
    {
        private readonly IRepositoryInvoice _repositoryInvoice;

        public ServiceInvoice(IRepositoryInvoice repositoryInvoice)
        {
            _repositoryInvoice = repositoryInvoice;
        }

        public System.Threading.Tasks.Task AddInvoiceAsync(Invoice invoice)
        {
            throw new NotImplementedException();
        }

        public Task<int> Count() => _repositoryInvoice.Count();

        public System.Threading.Tasks.Task DeleteInvoiceAsync(int invoiceId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Invoice>> GetAllInvoicesAsync() => _repositoryInvoice.GetAllInvoicesAsync();

        public Task<IEnumerable<Invoice>> GetAllInvoicesAsync(int pageNumber, int pageSize) => _repositoryInvoice.GetAllInvoicesAsync(pageNumber, pageSize);

        public Task<Invoice> GetInvoiceByIdAsync(int invoiceId)
        {
            throw new NotImplementedException();
        }

        public System.Threading.Tasks.Task UpdateInvoiceAsync(Invoice invoice)
        {
            throw new NotImplementedException();
        }
    }
}
