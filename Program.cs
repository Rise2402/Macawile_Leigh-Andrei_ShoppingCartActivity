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
        return Price * quantity;
    }

    public bool HasEnoughStock(int quantity)
    {
        return RemainingStock >= quantity;
    }

    public void DeductStock(int quantity)
    {
        RemainingStock -= quantity;
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
    static string GetYesNo()
    {
        string input = Console.ReadLine().ToUpper();

        while (input != "Y" && input != "N")
        {
            Console.Write("Invalid input. Enter Y or N only: ");
            input = Console.ReadLine().ToUpper();
        }

        return input;
    }

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
            Console.WriteLine("\n==============================================");
            Console.WriteLine("                 MEAT SHOP MENU              ");
            Console.WriteLine("==============================================");
            Console.WriteLine(" ID   ITEM NAME           PRICE      STOCK");
            Console.WriteLine("----------------------------------------------");

            for (int i = 0; i < menu.Length; i++)
            {
                Console.WriteLine(
                    menu[i].Id.ToString().PadRight(5) +
                    menu[i].Name.PadRight(20) +
                    ("PHP " + menu[i].Price.ToString("F2")).PadRight(12) +
                    menu[i].RemainingStock
                );
            }

            Console.WriteLine("==============================================");

            Console.Write("\nEnter product number: ");
            string productnum = Console.ReadLine();

            int productId;
            bool productvalid = int.TryParse(productnum, out productId);

            if (!productvalid)
            {
                Console.WriteLine("Invalid input. Please enter a number.");
                Console.Write("Continue shopping? (Y/N): ");
                choice = GetYesNo();
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
                choice = GetYesNo();
                continue;
            }

            if (selected.RemainingStock == 0)
            {
                Console.WriteLine("Sorry, " + selected.Name + " is out of stock.");
                Console.Write("Continue shopping? (Y/N): ");
                choice = GetYesNo();
                continue;
            }

            Console.Write("Enter quantity (available: " + selected.RemainingStock + "): ");
            string qtyinput = Console.ReadLine();

            int quantity;
            bool validqty = int.TryParse(qtyinput, out quantity);

            if (!validqty)
            {
                Console.WriteLine("Invalid input. Please enter a number.");
                Console.Write("Continue shopping? (Y/N): ");
                choice = GetYesNo();
                continue;
            }

            if (quantity <= 0)
            {
                Console.WriteLine("Quantity must be greater than zero.");
                Console.Write("Continue shopping? (Y/N): ");
                choice = GetYesNo();
                continue;
            }

            if (!selected.HasEnoughStock(quantity))
            {
                Console.WriteLine("Not enough stock available. Only " + selected.RemainingStock + " left.");
                Console.Write("Continue shopping? (Y/N): ");
                choice = GetYesNo();
                continue;
            }

            int existingIndex = -1;

            for (int i = 0; i < cartCount; i++)
            {
                if (cart[i].Product.Id == selected.Id)
                {
                    existingIndex = i;
                    break;
                }
            }

            if (existingIndex >= 0)
            {
                cart[existingIndex].Quantity += quantity;
                cart[existingIndex].Subtotal =
                    cart[existingIndex].Product.Price * cart[existingIndex].Quantity;
            }
            else
            {
                if (cartCount >= cart.Length)
                {
                    Console.WriteLine("Cart is full!");
                    break;
                }

                cart[cartCount] = new CartItem(selected, quantity);
                cartCount++;
            }

            selected.DeductStock(quantity);

            Console.WriteLine("Item added to cart successfully!");
            Console.Write("Continue shopping? (Y/N): ");
            choice = GetYesNo();

        } while (choice == "Y");

        Console.WriteLine("\n==============================================");
        Console.WriteLine("                   RECEIPT                  ");
        Console.WriteLine("==============================================");
        Console.WriteLine(" ITEM NAME        QTY     PRICE     SUBTOTAL");
        Console.WriteLine("----------------------------------------------");

        double grandTotal = 0;

        if (cartCount == 0)
        {
            Console.WriteLine(" Your cart is empty.");
        }
        else
        {
            for (int i = 0; i < cartCount; i++)
            {
                Console.WriteLine(
                    cart[i].Product.Name.PadRight(18) +
                    cart[i].Quantity.ToString().PadRight(8) +
                    ("PHP " + cart[i].Product.Price.ToString("F2")).PadRight(11) +
                    ("PHP " + cart[i].Subtotal.ToString("F2"))
                );

                grandTotal += cart[i].Subtotal;
            }
        }

        Console.WriteLine("----------------------------------------------");
        Console.WriteLine(" TOTAL:".PadRight(35) + "PHP " + grandTotal.ToString("F2"));

        double discount = 0;
        double finalTotal = grandTotal;

        if (grandTotal >= 5000)
        {
            discount = grandTotal * 0.10;
            finalTotal = grandTotal - discount;
            Console.WriteLine(" DISCOUNT (10%):".PadRight(35) + "-PHP " + discount.ToString("F2"));
        }

        Console.WriteLine(" FINAL TOTAL:".PadRight(35) + "PHP " + finalTotal.ToString("F2"));
        Console.WriteLine("==============================================");

        Console.WriteLine("\n--- Updated Stock ---");
        for (int i = 0; i < menu.Length; i++)
        {
            menu[i].DisplayProduct();
        }

        Console.WriteLine("\nThank you for shopping!");
    }
}
