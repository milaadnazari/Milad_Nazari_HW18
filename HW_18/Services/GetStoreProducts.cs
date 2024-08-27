using Dapper;
using HW_18.Models;
using System.Data.SqlClient;
using System.Xml.Linq;

namespace HW_18.Services
{
    public class GetStoreProducts : IGetStoreProducts
    {
        private readonly IConfiguration _iConfiguration;
        private string _connectionString;
        public GetStoreProducts(IConfiguration iConfiguration)
        {
            _iConfiguration = iConfiguration;
            _connectionString = _iConfiguration.GetConnectionString("DefaultConnection");
        }
        public async Task<List<Product>> Execute(int StoreId, bool isAsc)
        {
            string sortString = isAsc ? "" : "DESC";
            using (var cS = new SqlConnection(_connectionString))
            {
                cS.Open();
                string sql = $@"SELECT  
	   [product_id]
      ,[product_name]
      ,[brand_id]
      ,[category_id]
      ,[model_year]
      ,[list_price]
  FROM [BikeStores].[production].[products]
  WHERE product_id IN (SELECT product_id FROM [BikeStores].[production].[stocks] WHERE store_id = {StoreId})
  ORDER BY [list_price] {sortString}";
                var result = await cS.QueryAsync<Product>(sql);
                var resultList = result.ToList();
                
                return resultList;
            }
        }
    }
}
