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

