using HW_18.Models;

namespace HW_18.Services
{
    public interface IUpdateProduct
    {
        Task<string> Execute(Product product);
    }
}
