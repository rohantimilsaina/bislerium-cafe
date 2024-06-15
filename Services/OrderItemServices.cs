using bislerium_cafe_pos.Models;

namespace bislerium_cafe_pos.Services
{
    public class OrderItemServices
    {

        // Adds an item to the list of order items and Increases the quantity if duplicates items exists.
        public void AddItemInOrderItemsList(List<OrderItem> _orderItems, Guid itemID, string itemName, String itemType, Double itemPrice)
        {

            OrderItem orderItem = _orderItems.FirstOrDefault(x => x.ItemID.ToString() == itemID.ToString() && x.ItemType == itemType);

            if (orderItem != null)
            {
                orderItem.Quantity++;
                orderItem.TotalPrice = orderItem.Quantity * itemPrice;

                return;
            }

            orderItem = new()
            {
                ItemID = itemID,
                ItemName = itemName,
                ItemType = itemType,
                Quantity = 1,
                Price = itemPrice,
                TotalPrice = itemPrice
            };

            _orderItems.Add(orderItem);

        }

        // Deletes an item from the list of order items.
        public void DeleteItemInOrderItemsList(List<OrderItem> _orderItems, Guid orderItemID)
        {
            OrderItem orderItem = _orderItems.FirstOrDefault(x => x.OrderItemID == orderItemID);

            if (orderItem != null)
            {
                _orderItems.Remove(orderItem);
            }
        }

        // Manages the quantity of an existing order item(add or subtract).
        public void ManageQuantityOfOrderItem(List<OrderItem> _orderItems, Guid orderItemID, string action)
        {
            OrderItem orderItem = _orderItems.FirstOrDefault(x => x.OrderItemID == orderItemID);

            if (orderItem != null)
            {
                if (action == "add")
                {
                    orderItem.Quantity++;
                    orderItem.TotalPrice = orderItem.Quantity * orderItem.Price;
                }
                else if (action == "subtract" && orderItem.Quantity > 1)
                {
                    orderItem.Quantity--;
                    orderItem.TotalPrice = orderItem.Quantity * orderItem.Price;
                }
            }
        }

        // Calculates the total amount of all order items.
        public double CalculateTotalAmount(IEnumerable<OrderItem> Elements)
        {
            double totalAmount = 0;

            foreach (var item in Elements)
            {
                totalAmount += item.TotalPrice;
            }
            return totalAmount;
        }

        // Redeems free coffees based on the totalFreeCoffeeCount and the items in the cart.
        public Dictionary<string, double> RedeemFreeCoffees(int totalFreeCoffeeCount, List<OrderItem> cartOrderItems)
        {
            // Initialize variables to track redeemed coffees and total discount amount.
            int totalRedeemedCoffeeCount = 0;
            double totalDiscountAmount = 0;

            // If the cart is empty or no free coffees available, return an empty dictionary.
            if (cartOrderItems.Count == 0 || totalFreeCoffeeCount <= 0)
            {
                return new Dictionary<string, double>();
            }

            //Caluclating total quantity of cart
            int totalItemsQuantityInCart = cartOrderItems
                                                         .Where(item => item.ItemType == "coffee")
                                                          .Sum(item => item.Quantity);

            // Filter and order coffee items in the cart by price in descending order.
            var coffeeItems = cartOrderItems
                .Where(item => item.ItemType == "coffee")
                .OrderByDescending(item => item.Price)
                .ToList();

            foreach (var orderItem in coffeeItems)
            {
                // Calculate the new quantity of the coffee item after applying free coffee redemption.
                int diffBetweenCartQuantityAndFreeCoffeeCount = Math.Max(0, orderItem.Quantity - totalFreeCoffeeCount);

                int reedeemedItemQuantity = diffBetweenCartQuantityAndFreeCoffeeCount == 0 ? orderItem.Quantity : diffBetweenCartQuantityAndFreeCoffeeCount;

                // Calculate the number of redeemed coffees based on the new quantity.
                totalRedeemedCoffeeCount += reedeemedItemQuantity;

                // Calculate the discount amount for the coffee item.
                totalDiscountAmount += (orderItem.Price * reedeemedItemQuantity);

                // Update the remaining free coffee count.
                totalFreeCoffeeCount -= reedeemedItemQuantity;

                // Break the loop if no more free coffees or no more coffee items in the cart.
                if (totalFreeCoffeeCount <= 0)
                {
                    break;
                }
            }

            // Return a dictionary containing the total discount amount and the count of redeemed coffees.
            return new Dictionary<string, double>
            {
                { "discount", totalDiscountAmount },
                { "redeemedCoffeeCount", totalRedeemedCoffeeCount }
            };
        }

    }


}
