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

var processor = new OrderProcessor
{
    OnOrderInitialized = SendMessageToWarehouse
    //Point the delegate to the SendMessageToWarehouse method
    //One "issue" with public delegates is that they can be invoked from anywhere
};

processor.Process(order, SendConfirmaitonEmail);

void SendMessageToWarehouse()
{
    Console.WriteLine("Please pack the order");
}

void SendConfirmaitonEmail()
{
    Console.WriteLine("Order Confirmation Email");
}