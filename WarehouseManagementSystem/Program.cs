using WarehouseManagementSystem.Business;
using WarehouseManagementSystem.Domain;

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

//This changed the delegate from passing a function (SendMessageToWarehouse) to sending the property of the order instance
var processor = new OrderProcessor
{
    OnOrderInitialized = (order) => order.IsReadyForShipment
};

//Updated to use the Action<Order> delegate instead of assigning the delegate long form
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