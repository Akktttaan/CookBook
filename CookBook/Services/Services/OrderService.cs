using Dal.Interfaces;
using Domain.Dto;
using Domain.Models;
using Services.Interfaces;

namespace Services.Services;

public class OrderService : IOrderService
{
    private readonly IUnitOfWork _unitOfWork;

    public OrderService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task AddOrder(OrderDetailData[] orderDetails)
    {
        var orderRep = _unitOfWork.GetRepository<Order>();
        var orderDetailRep = _unitOfWork.GetRepository<OrderDetail>();

        var orderKey = Guid.NewGuid();
        orderRep.Add(new Order() { OrderDate = DateTime.Now, OrderKey = orderKey });
        var allOrder = await orderRep.GetAll();
        var currentOrderId = allOrder.FirstOrDefault(x => x.OrderKey == orderKey).Id;

        foreach (var orderDetail in orderDetails.Where(x => x.Quantity != 0))
        {
            orderDetailRep.Add(new OrderDetail()
            {
                DishRecipeId = orderDetail.DishRecipe.Id.Value,
                OrderId = currentOrderId,
                Quantity = orderDetail.Quantity
            });
        }
    }


    public async Task<OrderDetailData[]> GetDishRecipesForOrder()
    {
        var recipeRepository = _unitOfWork.GetRepository<DishRecipe>();
        var query = await recipeRepository.GetAll();
        return query.Select(recipe => new OrderDetailData()
            {
                DishRecipe = new DishRecipeData()
                {
                    Id = recipe.Id,
                    Name = recipe.Name,
                    Price = recipe.Price,
                    RecipeIngredients = new List<RecipeIngredientData>()
                }
            })
            .OrderBy(recipe => recipe.DishRecipe.Name)
            .ToArray();
    }
}