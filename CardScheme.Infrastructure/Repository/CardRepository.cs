using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CardScheme.Domain.Entities;
using CardScheme.Domain.Interfaces;
using CardScheme.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace CardScheme.Infrastructure.Repository
{
    /// <summary>
    /// Card crud repository
    /// </summary>
    public class CardRepository : ICardRepository
    {
        private readonly DataContext _context;

        public CardRepository(DataContext context)
        {
            _context = context;
        }

        public void InsertCard(CardTable model)
        {
            _context.CardTables.AddAsync(model);
        }
        public Task<CardTable> GetCard(string cardNumber)
        {
            return _context.CardTables.FirstOrDefaultAsync(cc => cc.CardNumber == cardNumber);
        }

        public Task<List<CardTable>> GetAllCard()
        {
            return _context.CardTables.ToListAsync();
        }
        public void UpdateCard(CardTable model)
        {
            _context.Entry(model).State = EntityState.Modified;
        }

        public async Task Complete()
        {
            await _context.SaveChangesAsync();
        }

    }

    
}
