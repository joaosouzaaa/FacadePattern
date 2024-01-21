namespace FacadePattern.API.Entities;

public sealed class Order
{
    public int Id { get; set; }
    public required DateTime CreationDate { get; set; }

    public List<ProductOrder> ProductOrders { get; set; }
}
