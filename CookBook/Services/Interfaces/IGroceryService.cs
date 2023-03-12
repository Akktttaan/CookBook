using Domain.Dto;
using Domain.Models;

namespace Services.Interfaces;

public interface IGroceryService
{
    public Task<GroceryItemData> GetGroceryItem(int id);

    public Task DeleteUnitOfMeasure(int id);

    public Task UpdateUnitOfMeasure(UnitOfMeasure unitOfMeasure);

    public Task AddUnitOfMeasure(UnitOfMeasure unitOfMeasure);

    public Task<UnitOfMeasure[]> GetUnitOfMeasures();

    public Task DeleteGroceryItem(int id);

    public Task UpdateGroceryItem(GroceryItem groceryItem);

    public Task AddGroceryItem(GroceryItem groceryItem);

    public Task<GroceryItemData[]> GetGroceryItems();
}