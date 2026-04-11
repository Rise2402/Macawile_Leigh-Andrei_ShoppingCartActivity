using System;

class Product
{
    public int Id;
    public string Name;
    public double Price;
    public int RemainingStock;

    public Product(int id, string name, double price, int stock)
    {
        Id = id;
        Name = name;
        Price = price;
        RemainingStock = stock;
    }

    public void DisplayProduct()
    {
        Console.WriteLine("  [" + Id + "] " + Name + "  -  PHP " + Price.ToString("F2") + "  (Stock: " + RemainingStock + ")");
    }

    public double GetItemTotal(int quantity)
    {
        double total = Price * quantity;
        return total;
    }

    public bool HasEnoughStock(int quantity)
    {
        if (RemainingStock >= quantity)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void DeductStock(int quantity)
    {
        RemainingStock = RemainingStock - quantity;
    }
}

class CartItem
{
    public Product Product;
    public int Quantity;
    public double Subtotal;

    public CartItem(Product product, int quantity)
    {
        Product = product;
        Quantity = quantity;
        Subtotal = product.Price * quantity;
    }
}