using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using CardScheme.Domain.DTOs;
using CardScheme.Domain.Entities;
using CardScheme.Domain.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using Payload = CardScheme.Domain.DTOs.Payload;

namespace CardScheme.Api.Controllers
{
    [Authorize]
    [Route("card-scheme")]
    [ApiController]
    
    public class CardController : ControllerBase
    {
        /// <summary>
        /// The card controller 
        /// </summary>
        private ILogger<CardController> _logger;
        private readonly ICardService _cardService;

        public CardController(ILogger<CardController> logger, ICardService cardService)
        {
            _logger = logger;
            _cardService = cardService;
        }

        
        [HttpGet("verify/{cardNumber}")]
        public async Task<IActionResult> Verify(string cardNumber)
        {
            try
            {
               var res = await _cardService.Add(cardNumber);
               return Ok(res);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpGet("stats")]
        public async Task<IActionResult> Statistics(int start, int limit)
        {
            try
            {
                if (start < 1 || limit < 1)
                {
                    return BadRequest("Start and Limit should be 1 or greater");
                }
                var allCard = await _cardService.Get(start, limit);

                return Ok(allCard);
            }
            catch (Exception ex)
            {
                return BadRequest($"{ex.Message}, Try again later");
            }
            
        }


    }
}
