using bislerium_cafe_pos.Models;
using bislerium_cafe_pos.Utils;
using System.Text.Json;

namespace bislerium_cafe_pos.Services
{
    public class AddInItemsServices
    {
        // Creating a list of AddIns objects with proper names and prices in NPR
        private readonly List<AddInItem> _addInItemsList = new()
        {
            new() { Name = "Extra Sugar", Price = 10.0 },
            new() { Name = "Whipped Cream", Price = 15.0 },
            new() { Name = "Chocolate Syrup", Price = 12.0 },
            new() { Name = "Vanilla Extract", Price = 18.0 },
            new() { Name = "Caramel Drizzle", Price = 22.0 },
            new() { Name = "Hazelnut Flavor", Price = 20.0 },
            new() { Name = "Cinnamon Powder", Price = 17.0 },
            new() { Name = "Almond Milk", Price = 25.0 },
            new() { Name = "Whiskey Shot", Price = 30.0 },
            new() { Name = "Special Syrup Blend", Price = 28.0 }
        };

        // Adds a new AddInItem
        public void AddAddInItem(String name, double price)

        {
            AddInItem addInItem = new()
            {
                Name = name,
                Price = price
            };

            List<AddInItem> addInItemList = GetAddInItemsListListFromJsonFile();

            addInItemList.Add(addInItem);

            SaveAddInItemsListInJsonFile(addInItemList);

        }


        // Saves the AddInItems list to the JSON file
        public void SaveAddInItemsListInJsonFile(List<AddInItem> addInItemList)
        {
            // Folder path where all the files related to app are stored
            string appDataDirPath = AppUtils.GetDesktopDirectoryPath();
            string addInItemsListListFilePath = AppUtils.GetAddInItemsListFilePath();

            // Ensure the directory exists or create it if not
            if (!Directory.Exists(appDataDirPath))
            {
                Directory.CreateDirectory(appDataDirPath);
            }

            var json = JsonSerializer.Serialize(addInItemList);

            File.WriteAllText(addInItemsListListFilePath, json);
        }

        // Retrieves the AddInItems list from the JSON file.
        public List<AddInItem> GetAddInItemsListListFromJsonFile()
        {
            string addInsItemsListListFilePath = AppUtils.GetAddInItemsListFilePath();

            if (!File.Exists(addInsItemsListListFilePath))
            {
                return new List<AddInItem>();
            }

            var json = File.ReadAllText(addInsItemsListListFilePath);

            return JsonSerializer.Deserialize<List<AddInItem>>(json);
        }

        // Seed the Add-In items list with predefined items if there is not any Add-In items in the JSON file
        public void SeedAddInItemsList()
        {
            List<AddInItem> addInsList = GetAddInItemsListListFromJsonFile();

            if (addInsList.Count == 0)
            {
                SaveAddInItemsListInJsonFile(_addInItemsList);
            }
        }

        // Retrieves an AddInItem by its ID) from the list.
        public AddInItem GetAddInItemDetailsByID(String addInItemID)
        {
            List<AddInItem> addInItemList = GetAddInItemsListListFromJsonFile();
            AddInItem addInItem = addInItemList.FirstOrDefault(addIn => addIn.Id.ToString() == addInItemID);
            return addInItem;
        }

        // Updates an existing AddInItem
        public void UpdateAddInItemDetails(AddInItem addInItem)
        {
            List<AddInItem> addInItemsList = GetAddInItemsListListFromJsonFile();

            AddInItem addInItemToUpdate = addInItemsList.FirstOrDefault(_addInItem => _addInItem.Id.ToString() == addInItem.Id.ToString());

            // Handle the case where the Add-In item to update is not found
            if (addInItemToUpdate == null)
            {
                throw new Exception("Add-In item not found");
            }

            addInItemToUpdate.Name = addInItem.Name;
            addInItemToUpdate.Price = Math.Round(addInItem.Price, 2);

            SaveAddInItemsListInJsonFile(addInItemsList);
        }

        // Deletes an AddInItem by ID
        public List<AddInItem> DeletAddInItem(Guid addInItemID)
        {
            List<AddInItem> addInItemsList = GetAddInItemsListListFromJsonFile();
            AddInItem addInItem = addInItemsList.FirstOrDefault(item => item.Id.ToString() == addInItemID.ToString());

            if (addInItem != null)
            {
                addInItemsList.Remove(addInItem);
                SaveAddInItemsListInJsonFile(addInItemsList);
            }

            return addInItemsList;
        }
    }
}
