using System.Text.Json;
using WarehouseManagementSystem.Business;
using WarehouseManagementSystem.Domain;
using WarehouseManagementSystem.Domain.Extensions;

var order = new Order
{
    LineItems = new[]
    {
        new Item {Name = "PS1", Price = 50},
        new Item {Name = "PS2", Price = 60},
        new Item {Name = "PS4", Price = 70},
        new Item {Name = "PS5", Price = 80}
    }
};
//Anonymous Types 
//Purpose is to encapsulate a set of read only properties for convinence without creating a class

var instance = new { Total = 100, AmountOfItems = 10 };
var instance2 = new { Total = 100, AmountOfItems = 10 };

Console.WriteLine(instance.Equals(instance2)); //By Properties - true
Console.WriteLine(instance == instance2);//By Refecence - False


var subset = new {
    order.OrderNumber, 
    order.Total,
    AveragePrice = order.LineItems.Average(item => item.Price) //Named parameter
};
Console.WriteLine(subset.OrderNumber);
Console.WriteLine(subset.AveragePrice);//Names can be explicit or infered

//You cannot add a method to an anonymous type. You can invoke a delegate, but don't 

var processor = new OrderProcessor();

IEnumerable<Order> orders = JsonSerializer.Deserialize<Order[]>(File.ReadAllText("orders.json"));

if(orders.Any())
    processor.Process(orders);
