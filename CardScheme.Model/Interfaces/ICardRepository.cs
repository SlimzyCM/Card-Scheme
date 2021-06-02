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
    public interface ICardRepository
    {
        void InsertCard(CardTable model);
        Task<List<CardTable>> GetAllCard();
        public Task<CardTable> GetCard(string cardNumber);
        void UpdateCard(CardTable model);
        Task Complete();
    }
}
