using SalesCrud.Data;

namespace SalesCrud.IServices
{
    public interface ISaleService
    {
        Task<IEnumerable<SalesDto>> GetSales();
        Task<bool> SaveSalesDetails(SalesDto sales);
        Task<SalesDto> GetSalesById(int id);
        Task<bool> DeleteSales(int id);
    }
}
