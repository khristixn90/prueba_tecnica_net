using AlicundeApi.Models;

namespace AlicundeApi.Interfaces
{
    public interface IBank
    {
        Task<List<Banks>> GetAllBanks();
    }
}
