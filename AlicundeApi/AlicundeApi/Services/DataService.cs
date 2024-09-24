using AlicundeApi.Context;
using AlicundeApi.Interfaces;
using AlicundeApi.Models;

namespace AlicundeApi.Services
{
    public class DataService : IData
    {
        private readonly ApplicationDbContext _applicationDbContext;
        public DataService(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task<List<Bank>> SaveBanksAsync(List<Banks> banks)
        {
            var allBanks = new List<Bank>();
            foreach (var bank in banks)
            {
                Bank dataBank = new Bank();
                dataBank.name = bank.name;
                dataBank.bic = bank.bic;
                dataBank.country = bank.country;

                await _applicationDbContext.Bank.AddAsync(dataBank);
                allBanks.Add(dataBank);
            }
            await _applicationDbContext.SaveChangesAsync();
            return allBanks;
        }

    }
}
