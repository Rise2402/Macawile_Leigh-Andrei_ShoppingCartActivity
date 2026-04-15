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

class Program
{
    static void Main()
    {
        Product[] menu = new Product[6];
        menu[0] = new Product(1, "Pork Liempo", 189.00, 50);
        menu[1] = new Product(2, "Chicken Breast", 145.00, 40);
        menu[2] = new Product(3, "Beef Bulalo Cut", 320.00, 30);
        menu[3] = new Product(4, "Pork Ribs", 210.00, 25);
        menu[4] = new Product(5, "Bangus", 99.00, 35);
        menu[5] = new Product(6, "Chicken Thigh", 130.00, 45);

        CartItem[] cart = new CartItem[10];
        int cartCount = 0;
        string choice = "";

        do
        {
            Console.WriteLine("\n-------- STORE MENU --------");
            for (int i = 0; i < menu.Length; i++)
            {
                menu[i].DisplayProduct();
            }
            Console.WriteLine("----------------------------");

            Console.Write("\nEnter product number: ");
            string productnum = Console.ReadLine();

            int productId;
            bool productvalid = int.TryParse(productnum, out productId);

            if (productvalid == false)
            {
                Console.WriteLine("Invalid input. Please enter a number.");
                Console.Write("Continue shopping? (Y/N): ");
                choice = Console.ReadLine().ToUpper();
                continue;
            }

            Product selected = null;
            for (int i = 0; i < menu.Length; i++)
            {
                if (menu[i].Id == productId)
                {
                    selected = menu[i];
                }
            }

            if (selected == null)
            {
                Console.WriteLine("Product not found. Please try again.");
                Console.Write("Continue shopping? (Y/N): ");
                choice = Console.ReadLine().ToUpper();
                continue;
            }

            if (selected.RemainingStock == 0)
            {
                Console.WriteLine("Sorry, " + selected.Name + " is out of stock.");
                Console.Write("Continue shopping? (Y/N): ");
                choice = Console.ReadLine().ToUpper();
                continue;
            }

            Console.Write("Enter quantity (available: " + selected.RemainingStock + "): ");
            string qtyinput = Console.ReadLine();

            int quantity;
            bool validqty = int.TryParse(qtyinput, out quantity);

            if (validqty == false)
            {
                Console.WriteLine("Invalid input. Please enter a number.");
                Console.Write("Continue shopping? (Y/N): ");
                choice = Console.ReadLine().ToUpper();
                continue;
            }

            if (quantity <= 0)
            {
                Console.WriteLine("Quantity must be greater than zero.");
                Console.Write("Continue shopping? (Y/N): ");
                choice = Console.ReadLine().ToUpper();
                continue;
            }

            if (selected.HasEnoughStock(quantity) == false)
            {
                Console.WriteLine("Not enough stock available. Only " + selected.RemainingStock + " left.");
                Console.Write("Continue shopping? (Y/N): ");
                choice = Console.ReadLine().ToUpper();
                continue;
            }

            // ✅ FIX: Add to cart + deduct stock
            if (cartCount >= cart.Length)
            {
                Console.WriteLine("Cart is full!");
                break;
            }

            CartItem item = new CartItem(selected, quantity);
            cart[cartCount] = item;
            cartCount++;

            selected.DeductStock(quantity);

            Console.WriteLine("Item added to cart successfully!");
            Console.Write("Continue shopping? (Y/N): ");
            choice = Console.ReadLine().ToUpper();

        } while (choice == "Y");

        Console.WriteLine("\n========== RECEIPT ==========");
        Console.WriteLine("Item                  Qty   Price       Subtotal");
        Console.WriteLine("--------------------------------------------------");

        double grandTotal = 0;

        if (cartCount == 0)
        {
            Console.WriteLine("Your cart is empty.");
        }
        else
        {
            for (int i = 0; i < cartCount; i++)
            {
                Console.WriteLine(cart[i].Product.Name + "  x" + cart[i].Quantity + "  PHP " + cart[i].Product.Price.ToString("F2") + "  PHP " + cart[i].Subtotal.ToString("F2"));
                grandTotal = grandTotal + cart[i].Subtotal;
            }
        }

        Console.WriteLine("--------------------------------------------------");
        Console.WriteLine("Grand Total: PHP " + grandTotal.ToString("F2"));

        double discount = 0;
        double finalTotal = grandTotal;

        if (grandTotal >= 5000)
        {
            discount = grandTotal * 0.10;
            finalTotal = grandTotal - discount;
            Console.WriteLine("Discount (10%): PHP " + discount.ToString("F2"));
        }

        Console.WriteLine("Final Total: PHP " + finalTotal.ToString("F2"));
        Console.WriteLine("=============================");

        Console.WriteLine("\n--- Updated Stock ---");
        for (int i = 0; i < menu.Length; i++)
        {
            menu[i].DisplayProduct();
        }

        Console.WriteLine("\nThank you for shopping!");
    }
}
