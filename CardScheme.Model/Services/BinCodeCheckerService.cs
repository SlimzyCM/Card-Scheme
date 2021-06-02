using System;
using System.Net.Http;
using System.Threading.Tasks;
using CardScheme.Domain.Entities;
using CardScheme.Domain.Interfaces;
using Newtonsoft.Json;

namespace CardScheme.Domain.Services
{
    /// <summary>
    /// The class that implements the contract in the IBinCodeCheckerService Interface
    /// </summary>
    public class BinCodeCheckerService : IBinCodeCheckerService
    {
        private const string BaseEndpoint = "https://bins-su-api.vercel.app/";
        private readonly HttpClient _client;

        public BinCodeCheckerService()
        {
            _client = new HttpClient {BaseAddress = new Uri(BaseEndpoint)};
        }

        /// <summary>
        /// Call the api to return the BinCode Details
        /// </summary>
        /// <param name="bınCode"></param>
        /// <returns></returns>
        public async Task<CardDetails> CheckBinDetails(string bınCode)
        {
            var request = await _client.GetAsync($"api/{bınCode}");
            if (!request.IsSuccessStatusCode)
            {
                return new CardDetails { Success = false };
            }

            var json = await request.Content?.ReadAsStringAsync();
            return !string.IsNullOrEmpty(json)
                ? JsonConvert.DeserializeObject<CardDetails>(json)
                : null;
        }
    }
}
