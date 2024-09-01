using HW_18.Models;
using HW_18.ViewModels;

namespace HW_18.Services
{
    public interface IGetStoreProducts
    {
        Task<ProductViewModel> Execute(int storeId, bool isAsc, int page = 1);
    }
}
