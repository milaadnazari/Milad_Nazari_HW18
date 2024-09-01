using Dapper;
using HW_18.Models;
using System.Data;
using System.Data.SqlClient;

namespace HW_18.Services
{
    public class GetProduct : IGetProduct
    {
        private readonly IDbConnection _connectionString;
        public GetProduct(IConfiguration iConfiguration)
        {
            _connectionString = new SqlConnection(iConfiguration.GetConnectionString("DefaultConnection"));
        }
        public async Task<List<Product>> Execute(int id)
        {
            using (var cS = _connectionString)
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
  WHERE product_id = @id";
                var result = await cS.QueryAsync<Product>(sql, new {id = id});
                var resultList = result.ToList();

                return resultList;
            }
        }
    }
}
