using Dapper;
using HW_18.Models;
using System.Data.SqlClient;

namespace HW_18.Services
{
    public class GetStores : IGetStores
    {
        private readonly IConfiguration _configuration;
        private readonly string _cS;

        public GetStores(IConfiguration configuration)
        {
            _configuration = configuration;
            _cS = _configuration.GetConnectionString("DefaultConnection");
        }
        public async Task<List<Store>> Execute(string zip, string name)
        {
            var cS = new SqlConnection(_cS);
            cS.Open();
            string sql = @"SELECT * FROM [BikeStores].[sales].[stores]";
            var result = await cS.QueryAsync<Store>(sql);
            var resultList = result.ToList();
            if (!string.IsNullOrEmpty(name)) 
            {
                resultList = resultList.Where(a => a.store_name.ToLower().Contains(name.ToLower())).ToList();
            }
            if (!string.IsNullOrEmpty(zip))
            {
                resultList = resultList.Where(a => a.zip_code.Contains(zip)).ToList();
            }
            return resultList;
        }
    }
}
