# Macawile_Leigh-Andrei_ShoppingCartActivity
"AI Usage in This Project”

Firstly, I used AI to help me understand how to properly start the project and how to structure the program. It guided me on how to approach the overall program, including how to organize the classes and the flow of the shopping cart system. I also used AI to help me understand how to structure the CartItem class and how to store a Product inside another class. This part was confusing for me at first because I was not sure how to access the fields of the Product object once it was inside another class.

I also used AI to help me with validation logic, especially for handling non-numeric input from the user. I already knew about int.TryParse(), but I was not confident in how to apply it properly in my program.

Another issue I encountered was preventing duplicate items in the cart. At first, when I buy the same product twice it does not add on to the initial quantity, instead the receipt prints out the same product twice, AI helped me understand that I should first check if the item already exists in the cart and then update its quantity instead of creating a duplicate entry.

In addition to the logic and structure of the program, I used AI to design and improve the menu and receipt layout. This helped make the output more presentable and closer to how a real store system looks.

WHY I USED AI?

I used AI because some parts of my project were difficult to figure out on my own, especially the structure of the program and the logic for the shopping cart system. I needed help in understanding how to properly organize classes, handle user input validation, and improve the overall design of the program. AI also helped me when I got stuck with errors or logic issues that I could not easily solve.

Prompts/questions I asked

• How do I start a C# shopping cart project, and what should the basic structure look like?

• I’m confused about how to use a class inside another class like Product inside CartItem.

• How do I use int.TryParse properly for input validation?

• (Pasted the code) I am having an issue where my cart is creating duplicate items instead of updating the quantity can you help me?

• How do I loop through an array to display all products in a menu?

• (Pasted the code) I was thinking about how I could make my menu and receipt look better and more presentable almost like a real shopping system can you help me?

"AI USAGE IN PART 2"

I used AI to improve specific features in my program. First, I learned how to add a Category field to my Product class and use it to filter products by category. This helped me understand how to use a string field to group and display related items. Next, I used AI to learn how to properly remove an item from an array and shift the remaining items to fill the gap so there are no empty slots in the middle of my cart. This helped me understand how array manipulation works when deleting elements. I also used AI to learn how to add a new method AddStock() to my Product class to restore stock when items are removed or updated in the cart. Next, I asked AI how to keep asking the user for payment using a loop until the amount entered is enough to cover the final total. This allowed me to properly complete the checkout feature. I also used AI to learn how to get the current date and time using DateTime.Now and how to format it to display nicely on the receipt. Lastly, I asked AI to review my program for possible bugs especially in the stock update logic when the user changes the quantity of an item already in the cart. Through this, I became more aware of how restoring and re-deducting stock correctly requires saving the old quantity first before applying the new one.

Prompts/questions I asked:

"How do I add a Category field to a class and filter an array by it in C#? (then I pasted my code)"

"How do I remove an item from an array and shift the remaining elements to fill the gap? (then I pasted my code)"

"How do I restore stock when a cart item is removed or updated?"

"How do I add a new method to an existing class in C#?"

"How do I keep looping until the user enters a valid payment amount in C#?"

"How do I get the current date and time in C# and display it on a receipt?"

"How do I format the date and time to look like April 30, 2026 08:30 PM?"

"How do I store completed orders in an array and display them later? (then I pasted my code)"

"How do I make a Y/N prompt keep asking until the user types Y or N only?"

"How do I display a receipt number that always shows 4 digits like 0001?"

"How do I check if a product is already in my cart and update it instead of adding a duplicate? (then I pasted my code)"

"Is there any bug in my stock update logic when the user changes quantity? (then I pasted my code)"

Changes I made after using AI:

-Added Category field to the Product class and updated all products with their correct categories

-Added AddStock() method to the Product class to properly restore stock when items are removed or updated

-Implemented the remove item feature with array shifting to avoid empty gaps in the cart

-Fixed the update quantity logic to restore the old stock first before deducting the new amount

-Added a payment validation loop that keeps prompting until the amount entered is enough

-Added DateTime.Now with proper formatting to display the date and time on each receipt

-Added receipt number formatting using ToString("D4") so it always shows as 0001, 0002, etc.

-Added order history that saves each completed transaction and displays it when requested

-Improved all Y/N prompts to strictly re-ask until the user enters only Y or N

-Fixed potential stock issues when the same product is added to the cart multiple times
