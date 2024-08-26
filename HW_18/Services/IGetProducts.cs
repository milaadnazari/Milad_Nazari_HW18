using HW_18.Models;

namespace HW_18.Services
{
    public interface IGetProducts
    {
        Task<List<Product>> Execute(int id, bool isAsc);
    }
}
