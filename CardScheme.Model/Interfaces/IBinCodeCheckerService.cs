using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CardScheme.Domain.Entities;

namespace CardScheme.Domain.Interfaces
{
    /// <summary>
    /// The interface to be implemented
    /// </summary>
    public interface IBinCodeCheckerService
    {
        Task<CardDetails> CheckBinDetails(string bınCode);

    }
}
