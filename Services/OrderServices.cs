using bislerium_cafe_pos.Models;
using bislerium_cafe_pos.Utils;
using System.Text.Json;

namespace bislerium_cafe_pos.Services
{
    public class OrderServices
    {
        // Retrieves a list of orders from the JSON file.
        public List<Order> GetOrdersFromJsonFile()
        {
            string orderListFilePath = AppUtils.GetOrderListFilePath();

            if (!File.Exists(orderListFilePath))
            {
                return new List<Order>();
            }

            var json = File.ReadAllText(orderListFilePath);

            return JsonSerializer.Deserialize<List<Order>>(json);
        }

        // Places a new order and appends the new order to the Orders.json file.
        public void PlaceOrder(Order order)
        {
            List<Order> orders = GetOrdersFromJsonFile();
            orders.Add(order);

            // Folder path where all the files related to app are stored.
            string appDataDirPath = AppUtils.GetDesktopDirectoryPath();
            string orderListFilePath = AppUtils.GetOrderListFilePath();

            if (!Directory.Exists(appDataDirPath))
            {
                Directory.CreateDirectory(appDataDirPath);
            }

            var json = JsonSerializer.Serialize(orders);

            File.WriteAllText(orderListFilePath, json);
        }
    }
}
