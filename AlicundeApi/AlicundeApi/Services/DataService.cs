using AlicundeApi.Context;
using AlicundeApi.Interfaces;
using AlicundeApi.Models;
using Microsoft.EntityFrameworkCore;

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

        public async Task<Bank> GetBankByPrimaryKey(int id)
        {
            var bank = await _applicationDbContext.Bank
                .FirstOrDefaultAsync(c => c.id == id);

            return bank;
        }

    }
}
