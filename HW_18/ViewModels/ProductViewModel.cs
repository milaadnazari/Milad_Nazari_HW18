using HW_18.Models;
using System.Reflection.Metadata.Ecma335;

namespace HW_18.ViewModels
{
    public class ProductViewModel
    {
        public List<Product> products { get; set; }
        public int PageNumber { get; set; }
        public bool IsAsc { get; set; }
    }
}
