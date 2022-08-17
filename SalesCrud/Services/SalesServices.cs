using Dapper;
using SalesCrud.Data;
using SalesCrud.IServices;
using System.Data;
using System.Data.SqlClient;

namespace SalesCrud.Services
{
    public class SalesServices : ISaleService
    {
        public IConfiguration _configuration { get; }
        public string _connectionString { get; }

        public SalesServices(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("DevDB");
        }

        public async Task<bool> DeleteSales(int id)
        {
            var parameters = new DynamicParameters();
            parameters.Add("SalesId", id, DbType.Int32);

            using(var conn = new SqlConnection(_connectionString))
            {
                if (conn.State == ConnectionState.Closed)
                    conn.Open();

                try
                {
                    await conn.ExecuteAsync("DeleteSales", parameters, commandType: CommandType.StoredProcedure);
                }
                catch (Exception)
                {
                    throw;
                }
                finally
                {
                    if (conn.State == ConnectionState.Open)
                        conn.Close();
                }
            }

            return true;
        }

        public async Task<IEnumerable<SalesDto>> GetSales()
        {
            IEnumerable<SalesDto> salesEntries;
            using (var conn = new SqlConnection(_connectionString))
            {
                if (conn.State == ConnectionState.Closed)
                    conn.Open();

                try
                {
                    salesEntries = await conn.QueryAsync<SalesDto>("GetSalesDetails", commandType: CommandType.StoredProcedure);
                }
                catch (Exception)
                {
                    throw;
                }
                finally
                {
                    if (conn.State == ConnectionState.Open)
                        conn.Close();
                }
            }

            return salesEntries;
        }

        public async Task<SalesDto> GetSalesById(int id)
        {
            var parameters = new DynamicParameters();
            parameters.Add("SalesId", id, DbType.Int32);
            SalesDto sales = new SalesDto();

            using (var conn = new SqlConnection(_connectionString))
            {
                if (conn.State == ConnectionState.Closed)
                    conn.Open();

                try
                {
                    sales = await conn.QueryFirstOrDefaultAsync<SalesDto>("GetSalesById", parameters, commandType: CommandType.StoredProcedure);
                }
                catch (Exception)
                {
                    throw;
                }
                finally
                {
                    if (conn.State == ConnectionState.Open)
                        conn.Close();
                }
            }

            return sales;
        }

        public async Task<bool> SaveSalesDetails(SalesDto sales)
        {
            var parameters = new DynamicParameters();
            parameters.Add("ProductName", sales.ProductName, DbType.String);
            parameters.Add("Quantity", sales.Quantity, DbType.Int32);

            using (var conn = new SqlConnection(_connectionString))
            {
                if (conn.State == ConnectionState.Closed)
                    conn.Open();

                try
                {
                    if (sales.IsUpdate)
                    {
                        parameters.Add("SalesId", sales.SalesId, DbType.Int32);
                        await conn.ExecuteAsync("UpdateSales", parameters, commandType: CommandType.StoredProcedure);
                    }
                    else
                    {
                        await conn.ExecuteAsync("SaveSalesDetails", parameters, commandType: CommandType.StoredProcedure);
                    }
                }
                catch (Exception)
                {
                    throw;
                }
                finally
                {
                    if (conn.State == ConnectionState.Open)
                        conn.Close();
                }
            }

            return true;
        }
    }
}
