using HW_18.Models;

namespace HW_18.Services
{
    public interface IGetProduct
    {
        Task<List<Product>> Execute(int id);
    }
}
