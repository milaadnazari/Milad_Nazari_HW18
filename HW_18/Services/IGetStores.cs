using HW_18.Models;

namespace HW_18.Services
{
    public interface IGetStores
    {
        Task<List<Store>> Execute();
    }
}
