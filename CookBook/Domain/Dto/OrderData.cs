namespace Domain.Dto;

public class OrderData
{
    public int Id { get; set; }

    public DateTime OrderDate { get; set; }

    public IList<OrderDetailData> OrderDetails { get; set; }
}