using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicketPlace2._0.Models;

namespace TicketPlace2._0.service
{
    public interface IEventService
    {
        Task<List<EvenementModel>> GetEvenementsPaginatedAsync(string search, int page, int pageSize);
        Task<int> GetTotalCountAsync(string search);
    }
}