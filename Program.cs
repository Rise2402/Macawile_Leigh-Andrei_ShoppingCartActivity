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
            Console.WriteLine(" [2] Search Product by Name");
            Console.WriteLine(" [3] Filter by Category");
            Console.WriteLine(" [4] Manage Cart");
            Console.WriteLine(" [5] View Order History");
            Console.WriteLine(" [6] Exit");
            Console.WriteLine("=======================================");
            Console.Write("Enter choice: ");

            string mainInput = Console.ReadLine();
            int mainOpt;
            bool mainValid = int.TryParse(mainInput, out mainOpt);

            if (mainValid == false || mainOpt < 1 || mainOpt > 6)
            {
                Console.WriteLine("Invalid choice. Please enter 1 to 6.");
                continue;
            }

            if (mainOpt == 6)
            {
                mainChoice = "EXIT";
                continue;
            }

            if (mainOpt == 5)
            {
                Console.WriteLine("\n========== ORDER HISTORY ==========");
                if (orderCount == 0)
                {
                    Console.WriteLine(" No orders yet.");
                }
                else
                {
                    for (int i = 0; i < orderCount; i++)
                    {
                        Console.WriteLine(
                            " Receipt #" + orderHistory[i].ReceiptNumber.ToString("D4") +
                            "  |  " + orderHistory[i].OrderDate.ToString("MMMM dd, yyyy hh:mm tt") +
                            "  |  Final Total: PHP " + orderHistory[i].FinalTotal.ToString("F2")
                        );
                    }
                }
                Console.WriteLine("====================================");
                continue;
            }

            if (mainOpt == 4)
            {
                bool stayInCart = true;
                while (stayInCart == true)
                {
                    Console.WriteLine("\n========== CART MENU ==========");
                    Console.WriteLine(" [1] View Cart");
                    Console.WriteLine(" [2] Update Item Quantity");
                    Console.WriteLine(" [3] Remove Item");
                    Console.WriteLine(" [4] Clear Cart");
                    Console.WriteLine(" [5] Checkout");
                    Console.WriteLine(" [6] Back to Main Menu");
                    Console.WriteLine("================================");
                    Console.Write("Enter choice: ");

                    string cartInput = Console.ReadLine();
                    int cartOpt;
                    bool cartValid = int.TryParse(cartInput, out cartOpt);

                    if (cartValid == false || cartOpt < 1 || cartOpt > 6)
                    {
                        Console.WriteLine("Invalid choice. Please enter 1 to 6.");
                        continue;
                    }

                    if (cartOpt == 6)
                    {
                        stayInCart = false;
                        continue;
                    }

                    if (cartOpt == 1)
                    {
                        ShowCart(cart, cartCount);
                        continue;
                    }

                    if (cartOpt == 2)
                    {
                        ShowCart(cart, cartCount);
                        if (cartCount == 0)
                        {
                            continue;
                        }

                        Console.Write("Enter product number to update: ");
                        string updateInput = Console.ReadLine();
                        int updateId;
                        bool updateValid = int.TryParse(updateInput, out updateId);

                        if (updateValid == false)
                        {
                            Console.WriteLine("Invalid input.");
                            continue;
                        }

                        int updateIndex = -1;
                        for (int i = 0; i < cartCount; i++)
                        {
                            if (cart[i].Product.Id == updateId)
                            {
                                updateIndex = i;
                            }
                        }

                        if (updateIndex == -1)
                        {
                            Console.WriteLine("Product not found in cart.");
                            continue;
                        }

                        Console.Write("Enter new quantity (available stock: " + (cart[updateIndex].Product.RemainingStock + cart[updateIndex].Quantity) + "): ");
                        string newQtyInput = Console.ReadLine();
                        int newQty;
                        bool newQtyValid = int.TryParse(newQtyInput, out newQty);

                        if (newQtyValid == false || newQty <= 0)
                        {
                            Console.WriteLine("Invalid quantity.");
                            continue;
                        }

                        int totalAvailable = cart[updateIndex].Product.RemainingStock + cart[updateIndex].Quantity;

                        if (newQty > totalAvailable)
                        {
                            Console.WriteLine("Not enough stock. Maximum available: " + totalAvailable);
                            continue;
                        }

                        cart[updateIndex].Product.RemainingStock = cart[updateIndex].Product.RemainingStock + cart[updateIndex].Quantity;
                        cart[updateIndex].Product.RemainingStock = cart[updateIndex].Product.RemainingStock - newQty;
                        cart[updateIndex].Quantity = newQty;
                        cart[updateIndex].Subtotal = cart[updateIndex].Product.Price * newQty;

                        Console.WriteLine("Quantity updated successfully!");
                        continue;
                    }

                    if (cartOpt == 3)
                    {
                        ShowCart(cart, cartCount);
                        if (cartCount == 0)
                        {
                            continue;
                        }

                        Console.Write("Enter product number to remove: ");
                        string removeInput = Console.ReadLine();
                        int removeId;
                        bool removeValid = int.TryParse(removeInput, out removeId);

                        if (removeValid == false)
                        {
                            Console.WriteLine("Invalid input.");
                            continue;
                        }

                        int removeIndex = -1;
                        for (int i = 0; i < cartCount; i++)
                        {
                            if (cart[i].Product.Id == removeId)
                            {
                                removeIndex = i;
                            }
                        }

                        if (removeIndex == -1)
                        {
                            Console.WriteLine("Product not found in cart.");
                            continue;
                        }

                        cart[removeIndex].Product.RemainingStock = cart[removeIndex].Product.RemainingStock + cart[removeIndex].Quantity;

                        for (int i = removeIndex; i < cartCount - 1; i++)
                        {
                            cart[i] = cart[i + 1];
                        }
                        cart[cartCount - 1] = null;
                        cartCount = cartCount - 1;

                        Console.WriteLine("Item removed from cart.");
                        continue;
                    }

                    if (cartOpt == 4)
                    {
                        string confirmClear = "";
                        while (confirmClear != "Y" && confirmClear != "N")
                        {
                            Console.Write("Are you sure you want to clear the cart? (Y/N): ");
                            confirmClear = Console.ReadLine().ToUpper();
                            if (confirmClear != "Y" && confirmClear != "N")
                            {
                                Console.WriteLine("Invalid input. Please enter Y or N only.");
                            }
                        }

                        if (confirmClear == "Y")
                        {
                            for (int i = 0; i < cartCount; i++)
                            {
                                cart[i].Product.RemainingStock = cart[i].Product.RemainingStock + cart[i].Quantity;
                                cart[i] = null;
                            }
                            cartCount = 0;
                            Console.WriteLine("Cart cleared.");
                        }
                        continue;
                    }

                    if (cartOpt == 5)
                    {
                        if (cartCount == 0)
                        {
                            Console.WriteLine("Your cart is empty. Nothing to checkout.");
                            continue;
                        }

                        double grandTotal = 0;
                        for (int i = 0; i < cartCount; i++)
                        {
                            grandTotal = grandTotal + cart[i].Subtotal;
                        }

                        double discount = 0;
                        double finalTotal = grandTotal;

                        if (grandTotal >= 5000)
                        {
                            discount = grandTotal * 0.10;
                            finalTotal = grandTotal - discount;
                        }

                        double payment = 0;
                        bool paymentOk = false;

                        while (paymentOk == false)
                        {
                            Console.Write("Enter payment amount (Final Total: PHP " + finalTotal.ToString("F2") + "): ");
                            string payInput = Console.ReadLine();
                            bool payValid = double.TryParse(payInput, out payment);

                            if (payValid == false)
                            {
                                Console.WriteLine("Invalid input. Please enter a number.");
                            }
                            else if (payment < finalTotal)
                            {
                                Console.WriteLine("Insufficient payment. Please enter at least PHP " + finalTotal.ToString("F2"));
                            }
                            else
                            {
                                paymentOk = true;
                            }
                        }

                        double change = payment - finalTotal;
                        DateTime now = DateTime.Now;

                        Console.WriteLine("\n==============================================");
                        Console.WriteLine("                   RECEIPT                  ");
                        Console.WriteLine("==============================================");
                        Console.WriteLine(" Receipt No : " + receiptNumber.ToString("D4"));
                        Console.WriteLine(" Date       : " + now.ToString("MMMM dd, yyyy hh:mm tt"));
                        Console.WriteLine("----------------------------------------------");
                        Console.WriteLine(" ITEM NAME        QTY     PRICE     SUBTOTAL");
                        Console.WriteLine("----------------------------------------------");

                        for (int i = 0; i < cartCount; i++)
                        {
                            Console.WriteLine(
                                cart[i].Product.Name.PadRight(18) +
                                cart[i].Quantity.ToString().PadRight(8) +
                                ("PHP " + cart[i].Product.Price.ToString("F2")).PadRight(11) +
                                ("PHP " + cart[i].Subtotal.ToString("F2"))
                            );
                        }

                        Console.WriteLine("----------------------------------------------");
                        Console.WriteLine(" TOTAL:".PadRight(35) + "PHP " + grandTotal.ToString("F2"));

                        if (discount > 0)
                        {
                            Console.WriteLine(" DISCOUNT (10%):".PadRight(35) + "-PHP " + discount.ToString("F2"));
                        }

                        Console.WriteLine(" FINAL TOTAL:".PadRight(35) + "PHP " + finalTotal.ToString("F2"));
                        Console.WriteLine(" PAYMENT:".PadRight(35) + "PHP " + payment.ToString("F2"));
                        Console.WriteLine(" CHANGE:".PadRight(35) + "PHP " + change.ToString("F2"));
                        Console.WriteLine("==============================================");

                        orderHistory[orderCount] = new OrderHistory(receiptNumber, now, finalTotal, payment, change);
                        orderCount = orderCount + 1;
                        receiptNumber = receiptNumber + 1;

                        bool hasLowStock = false;
                        for (int i = 0; i < menu.Length; i++)
                        {
                            if (menu[i].RemainingStock <= 5)
                            {
                                if (hasLowStock == false)
                                {
                                    Console.WriteLine("\n========== LOW STOCK ALERT ==========");
                                    hasLowStock = true;
                                }
                                Console.WriteLine(" WARNING: " + menu[i].Name + " has only " + menu[i].RemainingStock + " stock(s) left!");
                            }
                        }
                        if (hasLowStock == true)
                        {
                            Console.WriteLine("=====================================");
                        }

                        for (int i = 0; i < cartCount; i++)
                        {
                            cart[i] = null;
                        }
                        cartCount = 0;

                        Console.WriteLine("\nThank you for shopping at Kasamas Meat Shop!");
                        stayInCart = false;
                        continue;
                    }
                }
                continue;
            }

            if (mainOpt == 2)
            {
                Console.WriteLine("SEARCH BY NAME SUNOD.");
            }

        } while (mainChoice != "EXIT");

        Console.WriteLine("\nGoodbye! Thank you for visiting Leigh's Meat Shop!");
    }

    static void ShowCart(CartItem[] cart, int cartCount)
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
    }
}

