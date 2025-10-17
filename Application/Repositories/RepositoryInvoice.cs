using AutoMapper;
using Domain.Interfaces.Repositories;
using Domain.Models;
using Infraestructure.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Infraestructure.Repositories
{
    public class RepositoryInvoice : IRepositoryInvoice
    {
        private readonly NimacOptiWorkContext _context;
        private readonly IMapper _mapper;

        public RepositoryInvoice(NimacOptiWorkContext context, IMapper mapper) 
        { 
            _context = context;
            _mapper = mapper;
        }

        public System.Threading.Tasks.Task AddInvoiceAsync(Invoice invoice)
        {
            throw new NotImplementedException();
        }

        public async Task<int> Count()
        {
            try {
                var response = await _context.Invoices.CountAsync();
                return response;
            }
            catch (Exception ex) 
            { 
                throw;
            }
        }

        public System.Threading.Tasks.Task DeleteInvoiceAsync(int invoiceId)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Invoice>> GetAllInvoicesAsync()
        {
            try
            {
                var date = DateTime.Today;

                var response = await _context.Invoices
                    .Where(i => EF.Functions.DateDiffDay(i.DateEntry, date) ==0)
                    .OrderByDescending(i => i.InvoiceId)
                    .ToListAsync();
                
                if(response == null || response.Count == 0)
                {
                    return Enumerable.Empty<Invoice>();
                }

                var responseMapped = _mapper.Map<IEnumerable<Invoice>>(response);

                return responseMapped;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<IEnumerable<Invoice>> GetAllInvoicesAsync(int pageNumber, int pageSize)
        {
            try {
                var date = DateTime.Today;
                var response = await _context.Invoices
                    .Where(i => EF.Functions.DateDiffDay(i.DateEntry, date) == 0)
                    .OrderByDescending(i => i.InvoiceId)
                    .Skip((pageNumber) * pageSize)
                    .Take(pageSize)
                    .ToListAsync();

                if (response == null || response.Count == 0)
                {
                    return Enumerable.Empty<Invoice>();
                }
                var responseMapped = _mapper.Map<IEnumerable<Invoice>>(response);
                return responseMapped;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

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
