using Dal.Interfaces;
using Domain.Models;
using Services.Interfaces;

namespace Services.Services;

public class CuisineCategoryService : ICuisineCategoryService
{
    private readonly IUnitOfWork _unitOfWork;

    public CuisineCategoryService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<CuisineCategory[]> GetCuisineCategories()
    {
        var rep = _unitOfWork.GetRepository<CuisineCategory>();
        var query = await rep.GetAll();
        return query.ToArray();
    }

    public async Task AddCuisineCategory(CuisineCategory cuisineCategory)
    {
        await _unitOfWork.GetRepository<CuisineCategory>().Add(cuisineCategory);
    }

    public async Task UpdateCuisineCategory(CuisineCategory cuisineCategory)
    {
        await _unitOfWork.GetRepository<CuisineCategory>().Update(cuisineCategory);
    }

    public async Task DeleteCuisineCategory(int id)
    {
        await _unitOfWork.GetRepository<CuisineCategory>().Delete(id);
    }

    public async Task<DishType[]> GetDishTypes()
    {
        var query = await _unitOfWork.GetRepository<DishType>().GetAll();

        return query.ToArray();
    }

    public async Task AddDishType(DishType dishType)
    {
        await _unitOfWork.GetRepository<DishType>().Add(dishType);
    }

    public async Task UpdateDishType(DishType dishType)
    {
        await _unitOfWork.GetRepository<DishType>().Update(dishType);
    }

    public async Task DeleteDishType(int id)
    {
        await _unitOfWork.GetRepository<DishType>().Delete(id);
    }
}