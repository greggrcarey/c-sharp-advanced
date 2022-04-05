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

};

//Multi cast delegate example. Functions are executed in the sequence they are appended.
//Anyone can add and remove methods from publicly exposed property 
OrderProcessor.ProcessCompleted chain = One;
chain += Two;
chain += Three;

processor.Process(order, chain);

chain -= Two;

processor.Process(order, chain);


void One(Order order) => Console.WriteLine("One");
void Two(Order order) => Console.WriteLine("Two");
void Three(Order order) => Console.WriteLine("Three");

bool SendMessageToWarehouse(Order order)//The parameters mus be defined in the method signature if needed, not in the assignement
{
    Console.WriteLine($"Please pack the order: {order.OrderNumber}");
    return true;
}

void SendConfirmaitonEmail(Order order)
{
    Console.WriteLine($"Order Confirmation Email: {order.OrderNumber}");
}