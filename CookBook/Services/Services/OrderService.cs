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

    public async Task<OrderData> AddOrder(IEnumerable<OrderDetailData> orderDetails)
    {
        var orderRep = _unitOfWork.GetRepository<Order>();
        var orderDetailRep = _unitOfWork.GetRepository<OrderDetail>();
        var groceryItemRep = _unitOfWork.GetRepository<GroceryItem>();
        var recipeIngredientsRep = _unitOfWork.GetRepository<RecipeIngredient>();
        var unitOfMeasureRep = _unitOfWork.GetRepository<UnitOfMeasure>();
        var allGroceryItems = await groceryItemRep.GetAll();
        var unitOfMeasures = await unitOfMeasureRep.GetAll();

        var orderKey = Guid.NewGuid();
        var newOrder = new Order() { OrderDate = DateTime.Now, OrderKey = orderKey };
        await orderRep.Add(newOrder);
        var allOrder = await orderRep.GetAll();
        var currentOrderId = allOrder.First(x => x.OrderKey == orderKey).Id;
        var orderedRecipe = orderDetails.Where(x => x.Quantity != 0);

        foreach (var orderDetail in orderedRecipe)
        {
            await orderDetailRep.Add(new OrderDetail()
            {
                DishRecipeId = orderDetail.DishRecipe.Id.Value,
                OrderId = currentOrderId,
                Quantity = orderDetail.Quantity
            });
        }

        foreach (var recipe in orderedRecipe)
        {
            var allIngredients = await recipeIngredientsRep.GetAll();
            recipe.DishRecipe.RecipeIngredients = allIngredients
                .Where(x => x.DishRecipeId == recipe.DishRecipe.Id)
                .Select(x => new RecipeIngredientData()
                {
                    DishRecipeId = x.DishRecipeId,
                    Quantity = x.Quantity,
                    GroceryItem = allGroceryItems
                        .Where(y => y.Id == x.GroceryItemId)
                        .Select(z => new GroceryItemData()
                        {
                            Id = z.Id,
                            Name = z.Name,
                            Price = z.Price,
                            UnitOfMeasureId = z.UnitOfMeasureId,
                            UnitOfMeasureDescription = unitOfMeasures
                                .Where(u => u.Id == z.UnitOfMeasureId)
                                .Select(x => x.Name)
                                .First()
                        }).First()
                }).ToList();
        }

        return new OrderData()
        {
            Id = currentOrderId,
            OrderDate = newOrder.OrderDate,
            OrderDetails = orderedRecipe.ToList()
        };
    }

    public async Task<ReportData> GetAllOrder(DateTime dateFrom, DateTime dateTo)
    {
        dateFrom = dateFrom.AddDays(1);
        dateTo = dateTo.AddDays(1);
        var orderRepository = _unitOfWork.GetRepository<Order>();

        var dishRecipes = await _unitOfWork.GetRepository<DishRecipe>().GetAll();
        var recipeIngredients = await _unitOfWork.GetRepository<RecipeIngredient>().GetAll();
        var groceryItems = await _unitOfWork.GetRepository<GroceryItem>().GetAll();
        var orderDetails = await _unitOfWork.GetRepository<OrderDetail>().GetAll();
        var unitOfMeasures = await _unitOfWork.GetRepository<UnitOfMeasure>().GetAll();

        var allOrder = await orderRepository.GetAll();
        var allOrderData = new List<OrderData>();

        foreach (var order in allOrder
                     .Where(x => x.OrderDate > dateFrom && x.OrderDate < dateTo)
                     .Select(x => new OrderData()
                     {
                         Id = x.Id,
                         OrderDate = x.OrderDate
                     }))
        {
            order.OrderDetails = orderDetails
                .Where(x => x.OrderId == order.Id)
                .Select(y => new OrderDetailData()
                {
                    Quantity = y.Quantity,
                    OrderId = y.OrderId,
                    DishRecipe = dishRecipes
                        .Where(u => u.Id == y.DishRecipeId)
                        .Select(o => new DishRecipeData()
                        {
                            Id = o.Id,
                            CuisineCategoryId = o.CuisineCategoryId,
                            DishTypeId = o.DishTypeId,
                            Margin = o.Margin,
                            Name = o.Name,
                            RecipeIngredients = recipeIngredients
                                .Where(t => t.DishRecipeId == o.Id)
                                .Select(r => new RecipeIngredientData()
                                {
                                    DishRecipeId = r.DishRecipeId,
                                    GroceryItemId = r.GroceryItemId,
                                    Id = r.Id,
                                    Quantity = r.Quantity,
                                    GroceryItem = groceryItems
                                        .Where(a => a.Id == r.GroceryItemId)
                                        .Select(i => new GroceryItemData()
                                        {
                                            Id = i.Id,
                                            Name = i.Name,
                                            Price = i.Price,
                                            UnitOfMeasureId = i.UnitOfMeasureId,
                                            UnitOfMeasureDescription = unitOfMeasures
                                                .Where(x => x.Id == i.UnitOfMeasureId)
                                                .Select(x => x.Name)
                                                .First()
                                        })
                                        .First()
                                })
                                .ToList()
                        })
                        .First()
                })
                .ToList();
            allOrderData.Add(order);
        }

        var report = new ReportData()
        {
            GroceryItems = new List<GroceryItemReport>(),
            DishRecipes = new List<DishReport>()
        };

        var dishDict = new Dictionary<string, DishReport>();
        var groceryItemDict = new Dictionary<string, GroceryItemReport>();

        foreach (var orderDetail in allOrderData.SelectMany(order => order.OrderDetails))
        {
            if (dishDict.TryGetValue(orderDetail.DishRecipe.Name, out var dishReport))
            {
                dishReport.Quantity += orderDetail.Quantity;
                dishReport.Sum += orderDetail.DishRecipe.RecipeIngredients
                                      .Sum(x => x.GroceryItem.Price * x.Quantity) * orderDetail.DishRecipe.Margin *
                                  orderDetail.Quantity;
            }
            else
            {
                dishDict.Add(orderDetail.DishRecipe.Name, new DishReport()
                {
                    Name = orderDetail.DishRecipe.Name,
                    Quantity = orderDetail.Quantity,
                    Sum = orderDetail.DishRecipe.RecipeIngredients
                              .Sum(x => x.GroceryItem.Price * x.Quantity) * orderDetail.DishRecipe.Margin *
                          orderDetail.Quantity
                });
            }

            foreach (var recipeIngredient in orderDetail.DishRecipe.RecipeIngredients)
            {
                if (groceryItemDict.TryGetValue(recipeIngredient.GroceryItem.Name, out var groceryItemReport))
                {
                    groceryItemReport.Quantity += recipeIngredient.Quantity;
                    groceryItemReport.Sum += recipeIngredient.GroceryItem.Price * recipeIngredient.Quantity;
                }
                else
                {
                    groceryItemDict.Add(recipeIngredient.GroceryItem.Name, new GroceryItemReport()
                    {
                        Name = recipeIngredient.GroceryItem.Name,
                        Sum = recipeIngredient.GroceryItem.Price * recipeIngredient.Quantity,
                        Quantity = recipeIngredient.Quantity,
                        UnitOfMeasure = recipeIngredient.GroceryItem.UnitOfMeasureDescription
                    });
                }
            }
        }

        report.DishRecipes = dishDict.Values.ToList();
        report.GroceryItems = groceryItemDict.Values.ToList();

        return report;
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
                    Margin = recipe.Margin,
                    RecipeIngredients = new List<RecipeIngredientData>()
                }
            })
            .OrderBy(recipe => recipe.DishRecipe.Name)
            .ToArray();
    }
}