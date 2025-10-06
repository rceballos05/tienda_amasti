namespace TiendaPapeleria.Models;

public class CartItem
{
    public required Product Product { get; set; }
    public int Quantity { get; set; }
    public decimal Total => Product.Price * Quantity;
}
