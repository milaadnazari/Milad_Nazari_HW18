
using Dapper;
using HW_18.Models;
using System.Data;
using System.Data.SqlClient;

namespace HW_18.Services
{
    public class UpdateProduct : IUpdateProduct
    {
        private readonly IConfiguration _configuration;
        private readonly IDbConnection _connection;
        public UpdateProduct(IConfiguration configuration)
        {
            _configuration = configuration;
            _connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection"));
        }
        public async Task<bool> Execute(Product product)
        {
            bool result = false;
            using (var conn = _connection)
            {
                conn.Open();
                string sql = @"UPDATE [BikeStores].[production].[products]
SET product_name = @product_name,
	brand_id = @brand_id,
	category_id = @category_id,
	model_year = @model_year,
	list_price = @list_price
WHERE [product_id] = @product_id";
                var rowAffected = await conn.ExecuteAsync(sql, product);
                if (rowAffected > 0) result = true;
            }
            return result;
        }
    }
}
