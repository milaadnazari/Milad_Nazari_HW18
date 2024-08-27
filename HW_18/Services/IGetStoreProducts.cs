using HW_18.Models;

namespace HW_18.Services
{
    public interface IGetStoreProducts
    {
        Task<List<Product>> Execute(int storeId, bool isAsc);
    }
}
