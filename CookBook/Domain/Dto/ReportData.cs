using Domain.Models;

namespace Domain.Dto;

public class ReportData
{
    public IList<DishReport> DishRecipes { get; set; }

    public IList<GroceryItemReport> GroceryItems { get; set; }
}