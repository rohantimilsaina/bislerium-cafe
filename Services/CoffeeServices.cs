using bislerium_cafe_pos.Models;
using bislerium_cafe_pos.Utils;
using System.Text.Json;

namespace bislerium_cafe_pos.Services
{
    public class CoffeeServices
    {

        // Creating a list of Coffee objects 
        private readonly List<Coffee> _coffeeList = new()
        {
            new() { CoffeeType = "Cappuccino", Price = 150.0 },
            new() { CoffeeType = "Latte", Price = 170.0 },
            new() { CoffeeType = "Espresso", Price = 120.0 },
            new() { CoffeeType = "Americano", Price = 140.0 },
            new() { CoffeeType = "Mocha", Price = 180.0 },
            new() { CoffeeType = "Macchiato", Price = 160.0 },
            new() { CoffeeType = "Flat White", Price = 160.0 },
            new() { CoffeeType = "Affogato", Price = 200.0 },
            new() { CoffeeType = "Irish Coffee", Price = 190.0 },
            new() { CoffeeType = "Turkish Coffee", Price = 130.0 },
            new() { CoffeeType = "Ristretto", Price = 110.0 }
        };

        // Adds a new coffee to the list and saves it to the JSON file.
        public List<Coffee> AddCoffee(String coffeeType, double price)

        {
            Coffee coffee = new()
            {
                CoffeeType = coffeeType,
                Price = price
            };

            List<Coffee> coffeeList = GetCoffeeListFromJsonFile();

            coffeeList.Add(coffee);

            SaveCoffeeListInJsonFile(coffeeList);

            return coffeeList;
        }

        // Saves a list of coffees to the JSON file.
        public void SaveCoffeeListInJsonFile(List<Coffee> coffeeList)
        {
            // Folder path where all the files related to app are stored
            string appDataDirPath = AppUtils.GetDesktopDirectoryPath();

            string coffeeListFilePath = AppUtils.GetCofeeListFilePath();

            if (!Directory.Exists(appDataDirPath))
            {
                Directory.CreateDirectory(appDataDirPath);
            }

            var json = JsonSerializer.Serialize(coffeeList);

            File.WriteAllText(coffeeListFilePath, json);
        }

        // Retrieves the coffee list from the JSON file.
        public List<Coffee> GetCoffeeListFromJsonFile()
        {
            string coffeeListFilePath = AppUtils.GetCofeeListFilePath();

            if (!File.Exists(coffeeListFilePath))
            {
                return new List<Coffee>();
            }

            var json = File.ReadAllText(coffeeListFilePath);

            return JsonSerializer.Deserialize<List<Coffee>>(json);
        }

        // Seeds the JSON file with initial coffees if the JSON file is empty.
        public void SeedCofeeDetails()
        {
            List<Coffee> coffeeList = GetCoffeeListFromJsonFile();

            if (coffeeList.Count == 0)
            {
                SaveCoffeeListInJsonFile(_coffeeList);
            }
        }

        // Retrieves a coffee by its ID from the JSON file.
        public Coffee GetCofeeDetailsByID(String coffeeID)
        {
            List<Coffee> coffeeList = GetCoffeeListFromJsonFile();
            Coffee coffee = coffeeList.FirstOrDefault(coffee => coffee.Id.ToString() == coffeeID);
            return coffee;
        }

        // Updates an existing coffee in the list and in JSON file
        public void UpdateCofeeDetails(Coffee coffee)
        {
            List<Coffee> coffeeList = GetCoffeeListFromJsonFile();

            Coffee coffeeToUpdate = coffeeList.FirstOrDefault(_coffee => _coffee.Id.ToString() == coffee.Id.ToString());

            if (coffeeToUpdate == null)
            {
                throw new Exception("Coffee not found");
            }

            coffeeToUpdate.CoffeeType = coffee.CoffeeType;
            coffeeToUpdate.Price = Math.Round(coffee.Price, 2);

            SaveCoffeeListInJsonFile(coffeeList);
        }

        // Deletes a coffee by its ID and updates the JSON file.
        public List<Coffee> DeleteCoffeeType(Guid coffeeTypeID)
        {
            List<Coffee> coffeeList = GetCoffeeListFromJsonFile();

            Coffee coffee = coffeeList.FirstOrDefault(coffee => coffee.Id.ToString() == coffeeTypeID.ToString());

            if (coffee != null)
            {
                coffeeList.Remove(coffee);
                SaveCoffeeListInJsonFile(coffeeList);
            }

            return coffeeList;
        }

    }
}

