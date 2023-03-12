using Domain.Models;

namespace Services.Interfaces;

public interface ICuisineCategoryService
{
    public Task DeleteDishType(int id);
    public Task UpdateDishType(DishType dishType);
    public Task AddDishType(DishType dishType);
    public Task<DishType[]> GetDishTypes();
    public Task DeleteCuisineCategory(int id);
    public Task UpdateCuisineCategory(CuisineCategory cuisineCategory);
    public Task AddCuisineCategory(CuisineCategory cuisineCategory);
    public Task<CuisineCategory[]> GetCuisineCategories();
}