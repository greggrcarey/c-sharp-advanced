using System.Text.Json;
using WarehouseManagementSystem.Business;
using WarehouseManagementSystem.Domain;
using WarehouseManagementSystem.Domain.Extensions;

var items = new[]
   {
        new Item {Name = "PS1", Price = 50},
        new Item {Name = "PS2", Price = 60},
        new Item {Name = "PS4", Price = 70},
        new Item {Name = "PS5", Price = 80}
    };

var order = new Order(101, new(), items);

var ordersJson = JsonSerializer.Deserialize<Order[]>(File.ReadAllText("orders.json"));

var customer = new Customer("Gregg", "Carey")
{
    Address = new("123", "456")
};

var orderAsJson = JsonSerializer.Serialize(order, options: new() { WriteIndented = true });

Console.WriteLine(orderAsJson);