using System;

class Product
{
    private int id;
    private string name;
    private double price;
    private int remainingStock;
    private string category;

    public Product(int id, string name, double price, int stock, string category)
    {
        this.id = id;
        this.name = name;
        this.price = price;
        this.remainingStock = stock;
        this.category = category;
    }

    public int getId()
    {
        return id;
    }

    public void setId(int id)
    {
        this.id = id;
    }

    public string getName()
    {
        return name;
    }

    public void setName(string name)
    {
        this.name = name;
    }

    public double getPrice()
    {
        return price;
    }

    public void setPrice(double price)
    {
        this.price = price;
    }

    public int getRemainingStock()
    {
        return remainingStock;
    }

    public void setRemainingStock(int remainingStock)
    {
        this.remainingStock = remainingStock;
    }

    public string getCategory()
    {
        return category;
    }

    public void setCategory(string category)
    {
        this.category = category;
    }

    public void displayProduct()
    {
        Console.WriteLine("  [" + getId() + "] " + getName() + "  -  PHP " + getPrice().ToString("F2") + "  (Stock: " + getRemainingStock() + ")  [" + getCategory() + "]");
    }

    public double getItemTotal(int quantity)
    {
        double total = getPrice() * quantity;
        return total;
    }

    public bool hasEnoughStock(int quantity)
    {
        if (getRemainingStock() >= quantity)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void deductStock(int quantity)
    {
        setRemainingStock(getRemainingStock() - quantity);
    }

    public void addStock(int quantity)
    {
        setRemainingStock(getRemainingStock() + quantity);
    }
}

class CartItem
{
    private Product product;
    private int quantity;
    private double subtotal;

    public CartItem(Product product, int quantity)
    {
        this.product = product;
        this.quantity = quantity;
        this.subtotal = product.getPrice() * quantity;
    }

    public Product getProduct()
    {
        return product;
    }

    public void setProduct(Product product)
    {
        this.product = product;
    }

    public int getQuantity()
    {
        return quantity;
    }

    public void setQuantity(int quantity)
    {
        this.quantity = quantity;
    }

    public double getSubtotal()
    {
        return subtotal;
    }

    public void setSubtotal(double subtotal)
    {
        this.subtotal = subtotal;
    }
}

class OrderHistory
{
    private int receiptNumber;
    private DateTime orderDate;
    private double finalTotal;
    private double payment;
    private double change;

    public OrderHistory(int receiptNumber, DateTime orderDate, double finalTotal, double payment, double change)
    {
        this.receiptNumber = receiptNumber;
        this.orderDate = orderDate;
        this.finalTotal = finalTotal;
        this.payment = payment;
        this.change = change;
    }

    public int getReceiptNumber()
    {
        return receiptNumber;
    }

    public void setReceiptNumber(int receiptNumber)
    {
        this.receiptNumber = receiptNumber;
    }

    public DateTime getOrderDate()
    {
        return orderDate;
    }

    public void setOrderDate(DateTime orderDate)
    {
        this.orderDate = orderDate;
    }

    public double getFinalTotal()
    {
        return finalTotal;
    }

    public void setFinalTotal(double finalTotal)
    {
        this.finalTotal = finalTotal;
    }

    public double getPayment()
    {
        return payment;
    }

    public void setPayment(double payment)
    {
        this.payment = payment;
    }

    public double getChange()
    {
        return change;
    }

    public void setChange(double change)
    {
        this.change = change;
    }
}

class Program
{
    static void Main()
    {
        Product[] menu = new Product[6];
        menu[0] = new Product(1, "Pork Liempo",     189.00, 50, "Pork");
        menu[1] = new Product(2, "Chicken Breast",  145.00, 40, "Chicken");
        menu[2] = new Product(3, "Beef Bulalo Cut", 320.00, 30, "Beef");
        menu[3] = new Product(4, "Pork Ribs",       210.00,  6, "Pork");
        menu[4] = new Product(5, "Bangus",           99.00,  4, "Seafood");
        menu[5] = new Product(6, "Chicken Thigh",   130.00, 45, "Chicken");

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
                            " Receipt #" + orderHistory[i].getReceiptNumber().ToString("D4") +
                            "  |  " + orderHistory[i].getOrderDate().ToString("MMMM dd, yyyy hh:mm tt") +
                            "  |  Final Total: PHP " + orderHistory[i].getFinalTotal().ToString("F2")
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
                            if (cart[i].getProduct().getId() == updateId)
                            {
                                updateIndex = i;
                            }
                        }

                        if (updateIndex == -1)
                        {
                            Console.WriteLine("Product not found in cart.");
                            continue;
                        }

                        int totalAvailable = cart[updateIndex].getProduct().getRemainingStock() + cart[updateIndex].getQuantity();
                        Console.Write("Enter new quantity (available stock: " + totalAvailable + "): ");
                        string newQtyInput = Console.ReadLine();
                        int newQty;
                        bool newQtyValid = int.TryParse(newQtyInput, out newQty);

                        if (newQtyValid == false || newQty <= 0)
                        {
                            Console.WriteLine("Invalid quantity.");
                            continue;
                        }

                        if (newQty > totalAvailable)
                        {
                            Console.WriteLine("Not enough stock. Maximum available: " + totalAvailable);
                            continue;
                        }

                        cart[updateIndex].getProduct().setRemainingStock(cart[updateIndex].getProduct().getRemainingStock() + cart[updateIndex].getQuantity());
                        cart[updateIndex].getProduct().setRemainingStock(cart[updateIndex].getProduct().getRemainingStock() - newQty);
                        cart[updateIndex].setQuantity(newQty);
                        cart[updateIndex].setSubtotal(cart[updateIndex].getProduct().getPrice() * newQty);

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
                            if (cart[i].getProduct().getId() == removeId)
                            {
                                removeIndex = i;
                            }
                        }

                        if (removeIndex == -1)
                        {
                            Console.WriteLine("Product not found in cart.");
                            continue;
                        }

                        cart[removeIndex].getProduct().setRemainingStock(cart[removeIndex].getProduct().getRemainingStock() + cart[removeIndex].getQuantity());

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
                                cart[i].getProduct().setRemainingStock(cart[i].getProduct().getRemainingStock() + cart[i].getQuantity());
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
                            grandTotal = grandTotal + cart[i].getSubtotal();
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
                                cart[i].getProduct().getName().PadRight(18) +
                                cart[i].getQuantity().ToString().PadRight(8) +
                                ("PHP " + cart[i].getProduct().getPrice().ToString("F2")).PadRight(11) +
                                ("PHP " + cart[i].getSubtotal().ToString("F2"))
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
                            if (menu[i].getRemainingStock() <= 5)
                            {
                                if (hasLowStock == false)
                                {
                                    Console.WriteLine("\n========== LOW STOCK ALERT ==========");
                                    hasLowStock = true;
                                }
                                Console.WriteLine(" WARNING: " + menu[i].getName() + " has only " + menu[i].getRemainingStock() + " stock(s) left!");
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

                        Console.WriteLine("\nThank you for shopping at Leigh's Meat Shop!");
                        stayInCart = false;
                        continue;
                    }
                }
                continue;
            }

            if (mainOpt == 2)
            {
                Console.Write("Enter product name to search: ");
                string searchInput = Console.ReadLine().ToLower();

                bool found = false;
                Console.WriteLine("\n--- Search Results ---");
                for (int i = 0; i < menu.Length; i++)
                {
                    if (menu[i].getName().ToLower().Contains(searchInput))
                    {
                        menu[i].displayProduct();
                        found = true;
                    }
                }

                if (found == false)
                {
                    Console.WriteLine(" No products found matching \"" + searchInput + "\".");
                }
                continue;
            }

            if (mainOpt == 3)
            {
                Console.WriteLine("\n--- Categories ---");
                Console.WriteLine(" [1] Pork");
                Console.WriteLine(" [2] Chicken");
                Console.WriteLine(" [3] Beef");
                Console.WriteLine(" [4] Seafood");
                Console.Write("Enter category number: ");

                string catInput = Console.ReadLine();
                int catOpt;
                bool catValid = int.TryParse(catInput, out catOpt);

                string catName = "";
                if (catValid == true && catOpt == 1) catName = "Pork";
                else if (catValid == true && catOpt == 2) catName = "Chicken";
                else if (catValid == true && catOpt == 3) catName = "Beef";
                else if (catValid == true && catOpt == 4) catName = "Seafood";
                else
                {
                    Console.WriteLine("Invalid category.");
                    continue;
                }

                Console.WriteLine("\n--- " + catName + " Products ---");
                bool catFound = false;
                for (int i = 0; i < menu.Length; i++)
                {
                    if (menu[i].getCategory() == catName)
                    {
                        menu[i].displayProduct();
                        catFound = true;
                    }
                }

                if (catFound == false)
                {
                    Console.WriteLine(" No products in this category.");
                }
                continue;
            }

            if (mainOpt == 1)
            {
                string addChoice = "";
                do
                {
                    Console.WriteLine("\n==============================================");
                    Console.WriteLine("                 MEAT SHOP MENU              ");
                    Console.WriteLine("==============================================");
                    Console.WriteLine(" ID   ITEM NAME           PRICE      STOCK    CATEGORY");
                    Console.WriteLine("-------------------------------------------------------");

                    for (int i = 0; i < menu.Length; i++)
                    {
                        Console.WriteLine(
                            menu[i].getId().ToString().PadRight(5) +
                            menu[i].getName().PadRight(20) +
                            ("PHP " + menu[i].getPrice().ToString("F2")).PadRight(12) +
                            menu[i].getRemainingStock().ToString().PadRight(9) +
                            menu[i].getCategory()
                        );
                    }
                    Console.WriteLine("==============================================");

                    Console.Write("\nEnter product number: ");
                    string productnum = Console.ReadLine();

                    int productId;
                    bool productvalid = int.TryParse(productnum, out productId);

                    if (productvalid == false)
                    {
                        Console.WriteLine("Invalid input. Please enter a number.");
                        addChoice = AskYesNo("Continue shopping? (Y/N): ");
                        continue;
                    }

                    Product selected = null;
                    for (int i = 0; i < menu.Length; i++)
                    {
                        if (menu[i].getId() == productId)
                        {
                            selected = menu[i];
                        }
                    }

                    if (selected == null)
                    {
                        Console.WriteLine("Product not found. Please try again.");
                        addChoice = AskYesNo("Continue shopping? (Y/N): ");
                        continue;
                    }

                    if (selected.getRemainingStock() == 0)
                    {
                        Console.WriteLine("Sorry, " + selected.getName() + " is out of stock.");
                        addChoice = AskYesNo("Continue shopping? (Y/N): ");
                        continue;
                    }

                    Console.Write("Enter quantity (available: " + selected.getRemainingStock() + "): ");
                    string qtyinput = Console.ReadLine();

                    int quantity;
                    bool validqty = int.TryParse(qtyinput, out quantity);

                    if (validqty == false)
                    {
                        Console.WriteLine("Invalid input. Please enter a number.");
                        addChoice = AskYesNo("Continue shopping? (Y/N): ");
                        continue;
                    }

                    if (quantity <= 0)
                    {
                        Console.WriteLine("Quantity must be greater than zero.");
                        addChoice = AskYesNo("Continue shopping? (Y/N): ");
                        continue;
                    }

                    if (selected.hasEnoughStock(quantity) == false)
                    {
                        Console.WriteLine("Not enough stock available. Only " + selected.getRemainingStock() + " left.");
                        addChoice = AskYesNo("Continue shopping? (Y/N): ");
                        continue;
                    }

                    int existingIndex = -1;
                    for (int i = 0; i < cartCount; i++)
                    {
                        if (cart[i].getProduct().getId() == selected.getId())
                        {
                            existingIndex = i;
                            break;
                        }
                    }

                    if (existingIndex >= 0)
                    {
                        cart[existingIndex].setQuantity(cart[existingIndex].getQuantity() + quantity);
                        cart[existingIndex].setSubtotal(cart[existingIndex].getProduct().getPrice() * cart[existingIndex].getQuantity());
                        Console.WriteLine(selected.getName() + " updated in cart. New qty: " + cart[existingIndex].getQuantity());
                    }
                    else
                    {
                        if (cartCount >= cart.Length)
                        {
                            Console.WriteLine("Cart is full!");
                            addChoice = AskYesNo("Continue shopping? (Y/N): ");
                            continue;
                        }

                        cart[cartCount] = new CartItem(selected, quantity);
                        cartCount = cartCount + 1;
                        Console.WriteLine(selected.getName() + " added to cart!");
                    }

                    selected.deductStock(quantity);

                    addChoice = AskYesNo("Add another item? (Y/N): ");

                } while (addChoice == "Y");
            }

        } while (mainChoice != "EXIT");

        Console.WriteLine("\nGoodbye! Thank you for visiting Leigh's Meat Shop!");
    }

    static string AskYesNo(string prompt)
    {
        string input = "";
        while (input != "Y" && input != "N")
        {
            Console.Write(prompt);
            input = Console.ReadLine().ToUpper();
            if (input != "Y" && input != "N")
            {
                Console.WriteLine("Invalid input. Please enter Y or N only.");
            }
        }
        return input;
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
                    cart[i].getProduct().getId().ToString().PadRight(5) +
                    cart[i].getProduct().getName().PadRight(20) +
                    cart[i].getQuantity().ToString().PadRight(8) +
                    "PHP " + cart[i].getSubtotal().ToString("F2")
                );
            }
        }
        Console.WriteLine("================================");
    }
}
