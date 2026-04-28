using System;

class Product
{
    public int Id;
    public string Name;
    public double Price;
    public int RemainingStock;
    public string Category;

    public Product(int id, string name, double price, int stock, string category)
    {
        Id = id;
        Name = name;
        Price = price;
        RemainingStock = stock;
        Category = category;
    }

    public void DisplayProduct()
    {
        Console.WriteLine("  [" + Id + "] " + Name + "  -  PHP " + Price.ToString("F2") + "  (Stock: " + RemainingStock + ")  [" + Category + "]");
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

    public void AddStock(int quantity)
    {
        RemainingStock = RemainingStock + quantity;
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

class OrderHistory
{
    public int ReceiptNumber;
    public DateTime OrderDate;
    public double FinalTotal;
    public double Payment;
    public double Change;

    public OrderHistory(int receiptNumber, DateTime orderDate, double finalTotal, double payment, double change)
    {
        ReceiptNumber = receiptNumber;
        OrderDate = orderDate;
        FinalTotal = finalTotal;
        Payment = payment;
        Change = change;
    }
}

class Program
{
    static void Main()
    {
        Product[] menu = new Product[6];
        menu[0] = new Product(1, "Pork Liempo", 189.00, 50, "Pork");
        menu[1] = new Product(2, "Chicken Breast", 145.00, 40, "Chicken");
        menu[2] = new Product(3, "Beef Bulalo Cut", 320.00, 30, "Beef");
        menu[3] = new Product(4, "Pork Ribs", 210.00, 6, "Pork");
        menu[4] = new Product(5, "Bangus", 99.00, 4, "Seafood");
        menu[5] = new Product(6, "Chicken Thigh", 130.00, 45, "Chicken");

        CartItem[] cart = new CartItem[10];
        int cartCount = 0;

        OrderHistory[] orderHistory = new OrderHistory[50];
        int orderCount = 0;
        int receiptNumber = 1;

        Console.WriteLine("==============================================");
        Console.WriteLine("       WELCOME TO LEIGH'S MEAT SHOP          ");
        Console.WriteLine("==============================================");

        string mainChoice = "";

        do
        {
            Console.WriteLine("\n============== MAIN MENU ==============");
            Console.WriteLine(" [1] Browse & Add to Cart");
            Console.WriteLine(" [2] Manage Cart");
            Console.WriteLine(" [3] Exit");
            Console.WriteLine("=======================================");
            Console.Write("Enter choice: ");

            string mainInput = Console.ReadLine();
            int mainOpt;
            bool mainValid = int.TryParse(mainInput, out mainOpt);

            if (mainValid == false || mainOpt < 1 || mainOpt > 3)
            {
                Console.WriteLine("Invalid choice. Please enter 1 to 3.");
                continue;
            }

            if (mainOpt == 3)
            {
                mainChoice = "EXIT";
                continue;
            }

            if (mainOpt == 2)
            {
                bool stayInCart = true;
                while (stayInCart == true)
                {
                    Console.WriteLine("\n========== CART MENU ==========");
                    Console.WriteLine(" [1] View Cart");
                    Console.WriteLine(" [2] Back to Main Menu");
                    Console.WriteLine("================================");
                    Console.Write("Enter choice: ");

                    string cartInput = Console.ReadLine();
                    int cartOpt;
                    bool cartValid = int.TryParse(cartInput, out cartOpt);

                    if (cartValid == false || cartOpt < 1 || cartOpt > 2)
                    {
                        Console.WriteLine("Invalid choice. Please enter 1 or 2.");
                        continue;
                    }

                    if (cartOpt == 2)
                    {
                        stayInCart = false;
                        continue;
                    }

                    if (cartOpt == 1)
                    {
                        Console.WriteLine("\n========== YOUR CART ==========");
                        if (cartCount == 0)
                        {
                            Console.WriteLine(" Cart is empty.");
                        }
                        else
                        {
                            Console.WriteLine(" ID   ITEM NAME           QTY     SUBTOTAL");
                            Console.WriteLine(" ------------------------------------------");
                            for (int i = 0; i < cartCount; i++)
                            {
                                Console.WriteLine(
                                    " " +
                                    cart[i].Product.Id.ToString().PadRight(5) +
                                    cart[i].Product.Name.PadRight(20) +
                                    cart[i].Quantity.ToString().PadRight(8) +
                                    "PHP " + cart[i].Subtotal.ToString("F2")
                                );
                            }
                        }
                        Console.WriteLine("================================");
                        continue;
                    }
                }
                continue;
            }

            if (mainOpt == 1)
            {
                Console.WriteLine("browse and add to cart next.");
            }

        } while (mainChoice != "EXIT");

        Console.WriteLine("\nGoodbye! Thank you for visiting Leigh's Meat Shop!");
    }
}
