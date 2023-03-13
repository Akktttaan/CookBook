namespace Domain.Dto;

public class GroceryItemReport
{
    public string Name { get; set; }

    public int Quantity { get; set; }

    public string UnitOfMeasure { get; set; }

    public decimal Sum { get; set; }
}