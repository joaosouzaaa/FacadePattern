namespace FacadePattern.API.DataTransferObjects.Product;

public sealed record ProductSave(string Name, 
                                 decimal Price,
                                 int QuantityAvailable);
