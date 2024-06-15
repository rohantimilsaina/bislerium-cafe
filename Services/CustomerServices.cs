using bislerium_cafe_pos.Models;
using bislerium_cafe_pos.Utils;
using System.Text.Json;

namespace bislerium_cafe_pos.Services
{
    public class CustomerServices
    {
        private OrderServices _orderServices;

        public CustomerServices(OrderServices orderServices)
        {
            _orderServices = orderServices;
        }

        // Retrieves the list of customers from the JSON file.
        public List<Customer> GetCustomerListFromJsonFile()
        {
            string customersFilePath = AppUtils.GetCustomersListFilePath();

            if (!File.Exists(customersFilePath))
            {
                return new List<Customer>();
            }

            var json = File.ReadAllText(customersFilePath);

            return JsonSerializer.Deserialize<List<Customer>>(json);

        }

        // Saves a list of customers to the JSON file.
        public void SaveCustomerListInJsonFile(List<Customer> customers)
        {
            string appDataDirPath = AppUtils.GetDesktopDirectoryPath();
            string customerListFilePath = AppUtils.GetCustomersListFilePath();

            if (!Directory.Exists(appDataDirPath))
            {
                Directory.CreateDirectory(appDataDirPath);
            }

            var json = JsonSerializer.Serialize(customers);

            File.WriteAllText(customerListFilePath, json);
        }

        // Retrieves a customer by their phone number from the JSON file
        public Customer GetCustomerByPhoneNum(string customerPhoneNum)
        {
            List<Customer> customers = GetCustomerListFromJsonFile();
            Customer customer = customers.FirstOrDefault(c => c.CustomerPhoneNum == customerPhoneNum);
            return customer;
        }

        // Adds a new customer to the list and updates JSON file.
        public void AddCustomer(Customer _customer)
        {

            Customer isCustomerExists = GetCustomerByPhoneNum(_customer.CustomerPhoneNum);

            if (isCustomerExists != null)
            {
                throw new Exception("Customer Already exists");
            }

            List<Customer> customers = GetCustomerListFromJsonFile();
            customers.Add(_customer);

            SaveCustomerListInJsonFile(customers);
        }

        // Updates a customer's order count and saves the updated list to the JSON file.
        // This method is called when a customer places an order.
        public void UpdateRedeemedCoffeeCount(string customerPhoneNum, int redeemedCoffeeCount)
        {
            List<Customer> customers = GetCustomerListFromJsonFile();
            Customer customer = customers.FirstOrDefault(c => c.CustomerPhoneNum == customerPhoneNum);
            customer.RedeemedCoffeeCount = redeemedCoffeeCount;

            SaveCustomerListInJsonFile(customers);
        }

        //This method counts the order of prev month
        // With the order count more than 28, the customer is eligible for a free coffee.
        public bool CheckIfCustomerIsReguralMember(string customerPhoneNum)
        {
            List<Order> orders = _orderServices.GetOrdersFromJsonFile();

            // This condition is for:
            // If month is January, then the previous month is December of the previous year.
            int month = DateTime.Now.Month - 1;
            int year = month == 12 ? DateTime.Now.Year - 1 : DateTime.Now.Year;

            // At first, the order is filtered by the customer's phone number and previous month.
            // Then, the order is grouped by day and the total count of the orders is calculated.
            int totalOrderCount = orders
                .Where( order => order.CustomerPhoneNum == customerPhoneNum && order.OrderDateTime.Month == month && order.OrderDateTime.Year == year)
                .GroupBy(order => order.OrderDateTime.Day)
                .ToList().Count();
            
            //Returns true if the total order count is more than 28 i.e. customer is regular Member.
            return totalOrderCount >= 26;
        }

        // This method counts the TotalFreeCoffeeCount of the customer.
        public int TotalFreeCoffeeCount(string customerPhoneNum)
        {

            List<Order> orders = _orderServices.GetOrdersFromJsonFile();

            int totalOrderCount = orders
                .Where(order => order.CustomerPhoneNum == customerPhoneNum)
                .ToList().Count();

            return totalOrderCount / 10;
        }
    }
}
