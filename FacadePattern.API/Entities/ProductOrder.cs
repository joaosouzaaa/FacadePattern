namespace FacadePattern.API.Entities;

public sealed class ProductOrder
{
    public int Id { get; set; }
    public required int Quantity { get; set; }
    public required decimal TotalValue { get; set; }

    public required int ProductId { get; set; }
    public Product Product { get; set; }
    public int OrderId { get; set; }
    public Order Order { get; set; }
}
