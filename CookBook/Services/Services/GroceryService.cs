using Dal.Interfaces;
using Domain.Dto;
using Domain.Models;
using Services.Interfaces;

namespace Services.Services;

public class GroceryService : IGroceryService
{
    private readonly IUnitOfWork _unitOfWork;

    public GroceryService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<GroceryItemData[]> GetGroceryItems()
    {
        var groceryItemRepository = _unitOfWork.GetRepository<GroceryItem>();
        var unitOfMeasureRepository = _unitOfWork.GetRepository<UnitOfMeasure>();
        var query = await groceryItemRepository.GetAll();
        return query.Select(x => new GroceryItemData()
        {
            Name = x.Name,
            UnitOfMeasureDescription = x.UnitOfMeasureId is not null
                ? unitOfMeasureRepository.GetById(x.UnitOfMeasureId.Value).Result.Name
                : string.Empty,
            Id = x.Id,
            Price = x.Price,
            UnitOfMeasureId = x.UnitOfMeasureId,
        }).ToArray();
    }

    public async Task AddGroceryItem(GroceryItem groceryItem)
    {
        await _unitOfWork.GetRepository<GroceryItem>().Add(groceryItem);
    }

    public async Task UpdateGroceryItem(GroceryItem groceryItem)
    {
        await _unitOfWork.GetRepository<GroceryItem>().Update(groceryItem);
    }

    public async Task DeleteGroceryItem(int id)
    {
        await _unitOfWork.GetRepository<GroceryItem>().Delete(id);
    }

    public async Task<UnitOfMeasure[]> GetUnitOfMeasures()
    {
        var query = await _unitOfWork.GetRepository<UnitOfMeasure>().GetAll();

        return query.ToArray();
    }

    public async Task AddUnitOfMeasure(UnitOfMeasure unitOfMeasure)
    {
        await _unitOfWork.GetRepository<UnitOfMeasure>().Add(unitOfMeasure);
    }

    public async Task UpdateUnitOfMeasure(UnitOfMeasure unitOfMeasure)
    {
        await _unitOfWork.GetRepository<UnitOfMeasure>().Update(unitOfMeasure);
    }

    public async Task DeleteUnitOfMeasure(int id)
    {
        await _unitOfWork.GetRepository<UnitOfMeasure>().Delete(id);
    }

    public async Task<GroceryItemData> GetGroceryItem(int id)
    {
        var groceryItemRepository = _unitOfWork.GetRepository<GroceryItem>();
        var unitOfMeasureRepository = _unitOfWork.GetRepository<UnitOfMeasure>();
        var query = await groceryItemRepository.GetById(id);
        return new GroceryItemData()
        {
            Name = query.Name,
            UnitOfMeasureDescription = query.UnitOfMeasureId is not null
                ? unitOfMeasureRepository.GetById(query.UnitOfMeasureId.Value).Result.Name
                : string.Empty,
            Id = query.Id,
            Price = query.Price,
            UnitOfMeasureId = query.UnitOfMeasureId,
        };
    }
}