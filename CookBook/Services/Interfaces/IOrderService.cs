using Domain.Dto;

namespace Services.Interfaces;

public interface IOrderService
{
    Task AddOrder(OrderDetailData[] order);
    Task<OrderDetailData[]> GetDishRecipesForOrder();
}