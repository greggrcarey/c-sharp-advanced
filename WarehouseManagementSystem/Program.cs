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

//tuple is defiend with () 
//tuple is value type
//tuple values are stored in fields
//tuple is not immutable
var group = (order.OrderNumber,
    order.LineItems,
    Sum: order.LineItems.Sum(item => item.Price));

//group.Item3//compiler assigned property from the.Sum above. Later updated to name

//anonymous type is a reference type
//values are stored in properties with backing fields 
//read-only
var groupAsAnonymousType = new
{
    order.OrderNumber,
    order.LineItems,
    Sum = order.LineItems.Sum(item => item.Price)
};

//assignment with concrete types. Proterties can be accessed with compiler assigned names
(Guid, IEnumerable<Item>, decimal) group1 = (order.OrderNumber,
    order.LineItems,
    order.LineItems.Sum(item => item.Price));

//assignment with concrete named types. Properties can be accessed with names
(Guid orderNumber, IEnumerable<Item> items, decimal sum) group2 = (order.OrderNumber,
    order.LineItems,
    order.LineItems.Sum(item => item.Price));

//Deconstructed tuple with compiler infered types
(var orderNumber, var items, var sum) = (order.OrderNumber,
    order.LineItems,
    order.LineItems.Sum(item => item.Price));

//Deconstructed tuple with compiler infered types
var (orderNumber1 ,items1, sum1) = (order.OrderNumber,
    order.LineItems,
    order.LineItems.Sum(item => item.Price));

//Automatic assignment to local defined variables through deconstruction. Mind blown
//If the vaiable is missing, just add the var keyword to the deconstruction for access
//Order Matters! 
Guid orderNumber2;
decimal sum2;

 (orderNumber2,var items2, sum2) = (order.OrderNumber,
    order.LineItems,
    order.LineItems.Sum(item => item.Price));

//discard _ any unwanted variables
//discards can reduce memory allocations
(var orderNumber3, _,var sum3) = (order.OrderNumber,
   order.LineItems,
   order.LineItems.Sum(item => item.Price));

var ordersJson = JsonSerializer.Deserialize<Order[]>(File.ReadAllText("orders.json"));

var processor = new OrderProcessor();

// Tuples are powerful and can be used in almost all cases that named classes can
// When data-binding, stick with anonomyous types or named classes

var dictionary = new Dictionary<string, Order>();

foreach (var (orderKey, theOrder) in dictionary)
{
    //Deconstruct the the values in the iterator 
    //Iterate here and do work
}