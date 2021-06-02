using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CardScheme.Domain.DTOs;
using CardScheme.Domain.Entities;
using CardScheme.Domain.Services;

namespace CardScheme.Domain.Interfaces
{
    /// <summary>
    /// The interface to be implemented
    /// </summary>
    public interface ICardService
    {
        Task<Response<Data>> Add(string cardNumber);

        Task<StatsToReturn> Get(int start, int limit);
    }
}
