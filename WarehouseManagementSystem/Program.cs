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

var order2 = new Order
{
    LineItems = new[]
    {
        new Item {Name="Xbox", InStock = true, Price = 50},
        new Item {Name="Xbox 360", InStock = true, Price = 60},
        new Item {Name="PS1", InStock = false, Price = 50},
        new Item {Name="PS1", Price = 50}
    }
};

var order3 = new Order
{
    LineItems = new[]
    {
        new Item {Name = "PS1", Price = 50},
        new Item {Name = "PS2", Price = 60},
        new Item {Name = "PS4", Price = 70},
        new Item {Name = "PS5", Price = 80}
    }
};

var compare = order.Equals(order2);
var compare2 = order == order2;
var hashcode1 = order.GetHashCode();
var hashcode2 = order2.GetHashCode();
var equals_as_object = order.Equals(order2 as object);

var compare3 = order.Equals(order3);
var compare4 = order == order3;
var hashcode4 = order.GetHashCode();
var hashcode5 = order3.GetHashCode();
var equals_as_object2 = order.Equals(order3 as object);


var processor = new OrderProcessor
{
    OnOrderInitialized = (order) => order.IsReadyForShipment
};

var expensivePlaystations = order.LineItems.Find(item => item.Price >= 60);

Console.WriteLine(order.GenerateReport());
Console.WriteLine(order.GenerateReport(recipient: "Gregg"));//By naming the parameter, we can call the extension specifically


Action<Order> OnCompleted = (order) =>
{
    Console.WriteLine($"Processed {order.OrderNumber}");
};

processor.OrderCreated += (sender, args) =>
{
    Thread.Sleep(1000);
    Console.WriteLine("1");
};


processor.OrderCreated += Log;

processor.Process(order);

void Log(object sender, OrderCreatedEventArgs args)
{
    Console.WriteLine("Order Created");
}



bool SendMessageToWarehouse(Order order)//The parameters mus be defined in the method signature if needed, not in the assignement
{
    Console.WriteLine($"Please pack the order: {order.OrderNumber}");
    return true;
}

void SendConfirmaitonEmail(Order order)
{
    Console.WriteLine($"Order Confirmation Email: {order.OrderNumber}");
}