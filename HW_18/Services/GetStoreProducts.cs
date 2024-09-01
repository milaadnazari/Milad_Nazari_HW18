using Dapper;
using HW_18.Models;
using HW_18.ViewModels;
using System.Data;
using System.Data.SqlClient;
using System.Xml.Linq;

namespace HW_18.Services
{
    public class GetStoreProducts : IGetStoreProducts
    {
        private readonly IDbConnection _connectionString;
        public GetStoreProducts(IConfiguration iConfiguration)
        {
            _connectionString = new SqlConnection(iConfiguration.GetConnectionString("DefaultConnection"));
        }
        public async Task<ProductViewModel> Execute(int StoreId, bool isAsc, int page = 1)
        {
            int rowCount = 0;
            int offsetRows;
            List<Product> products = new List<Product>();
            string sortString = isAsc ? "" : "DESC";
            using (var cS = _connectionString)
            {
                cS.Open();
                string sqlCount = $@"SELECT  
	   COUNT(*)
  FROM [BikeStores].[production].[products]
  WHERE product_id IN (SELECT product_id FROM [BikeStores].[production].[stocks] WHERE store_id = {StoreId});";
                rowCount = cS.ExecuteScalar<int>(sqlCount);
                int lastPage = (int)Math.Ceiling(((double)rowCount) / 10);
                if (page < 2) offsetRows = 0;
                else if (lastPage < page) offsetRows = (lastPage - 1) * 10;
                else offsetRows = (page - 1) * 10;
                if (rowCount > 0)
                {
                    string sqlData = $@"SELECT  
	   [product_id]
      ,[product_name]
      ,[brand_id]
      ,[category_id]
      ,[model_year]
      ,[list_price]
  FROM [BikeStores].[production].[products]
  WHERE product_id IN (SELECT product_id FROM [BikeStores].[production].[stocks] WHERE store_id = {StoreId})
  ORDER BY [list_price] {sortString}
OFFSET {offsetRows} ROWS
FETCH NEXT 10 ROWS ONLY;";
                    var result = await cS.QueryAsync<Product>(sqlData);
                    products = result.ToList();
                }
            }
            ProductViewModel productViewModel = new ProductViewModel()
            {
                products = products,
                IsAsc = isAsc,
                PageNumber = (offsetRows / 10) + 1
            };
            return productViewModel;
        }
    }
}
