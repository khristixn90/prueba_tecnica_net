using AlicundeApi.Models;

namespace AlicundeApi.Interfaces
{
    public interface IData
    {
        Task<List<Bank>> SaveBanksAsync(List<Banks> banks);
    }
}
