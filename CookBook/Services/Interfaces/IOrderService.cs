using Domain.Dto;

namespace Services.Interfaces;

public interface IOrderService
{
    Task<OrderData> AddOrder(IEnumerable<OrderDetailData> order);
    Task<ReportData> GetAllOrder(DateTime dateFrom, DateTime dateTo);
    Task<OrderDetailData[]> GetDishRecipesForOrder();
}