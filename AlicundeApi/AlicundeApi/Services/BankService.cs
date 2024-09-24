using AlicundeApi.Context;
using AlicundeApi.Models;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Runtime;

namespace AlicundeApi.Services
{
    public class BankService
    {
        private readonly HttpClient _httpClient;
        private readonly Settings _settings;
        public BankService(HttpClient httpClient, IOptions<Settings> settings)
        {
            _httpClient = httpClient;
            _settings = settings.Value;
        }

        public async Task<List<Banks>> GetAllBanks()
        {
            try
            {
                HttpResponseMessage response = await
                _httpClient.GetAsync(_settings.UrlApi);
                response.EnsureSuccessStatusCode();

                string responseJson = await
                    response.Content.ReadAsStringAsync();
                var banks =
                    JsonConvert.DeserializeObject<List<Banks>>(responseJson);
                return banks;
            }
            catch (Exception ex)
            {
                throw new Exception("Error base", ex);
            }
        }

    }
}
