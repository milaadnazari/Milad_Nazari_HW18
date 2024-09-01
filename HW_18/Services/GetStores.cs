using Dapper;
using HW_18.Models;
using System.Data;
using System.Data.SqlClient;

namespace HW_18.Services
{
    public class GetStores : IGetStores
    {
        private readonly IDbConnection _cS;

        public GetStores(IConfiguration configuration)
        {
            _cS = new SqlConnection(configuration.GetConnectionString("DefaultConnection"));
        }
        public async Task<List<Store>> Execute(string zip, string name)
        {
            using (var cS = _cS)
            {
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
}
