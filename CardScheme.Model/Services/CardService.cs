using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CardScheme.Commons;
using CardScheme.Domain.DTOs;
using CardScheme.Domain.Entities;
using CardScheme.Domain.Interfaces;

namespace CardScheme.Domain.Services
{
    /// <summary>
    /// class that implements ICardService to abstract and make the controller clean
    /// </summary>
    public class CardService : ICardService
    {
        private readonly IBinCodeCheckerService _codeChecker;
        private readonly ICardRepository _cardRepo;

        public CardService(IBinCodeCheckerService codeChecker, ICardRepository cardRepo)
        {
            _codeChecker = codeChecker;
            _cardRepo = cardRepo;
        }

        public async Task<Response<Data>> Add(string cardNumber)
        {
            var cN = Utilities.NormalizeCardNumber(cardNumber);

            if (!Utilities.IsCardNumberValid(cN))
                return new Response<Data> { Success = false, Message = "Invalid Card number" };

            var res = await _codeChecker.CheckBinDetails(cardNumber);

            if (res.Data == null) return new Response<Data> { Success = false, Message = "Invalid Card number" };

            var model = await _cardRepo.GetCard(cN);

            if (model != null)
            {
                model.HitCount++;
                _cardRepo.UpdateCard(model);

            }
            else
            {
                model = new CardTable()
                {
                    BinCode = res.Data.Bin,
                    CardNumber = cN,
                    HitCount = 1

                };
                _cardRepo.InsertCard(model);

            }

            await _cardRepo.Complete();

            return new Response<Data> { Success = res.Success, Message = res.Message, Payload = res.Data };

        }

        public async Task<StatsToReturn> Get(int start, int limit)
        {
            var data = await _cardRepo.GetAllCard();

            return new StatsToReturn()
            {
                Success = data.Any(),
                Start = start,
                Limit = limit,
                Size = data.Count(),
                Payload = data.Select( x => new Payload
                {
                    BinCode = x.BinCode,
                    HitCount = x.HitCount
                }).ToList()
            };

        }
    }


    
}
