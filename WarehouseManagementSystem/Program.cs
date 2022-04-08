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



var ordersJson = JsonSerializer.Deserialize<Order[]>(File.ReadAllText("orders.json"));

var processor = new OrderProcessor();

if (order.GetType() == typeof(ProcessedOrder))
{
    var processedOrder = order as ProcessedOrder;
    if(processedOrder.IsReadyForShipment)
    { }
}

//
if(order is ProcessedOrder { IsReadyForShipment: true})
{

}

if (order is ProcessedOrder processedOrder1)
{
    //processedOrder1.ShippingProvider
}