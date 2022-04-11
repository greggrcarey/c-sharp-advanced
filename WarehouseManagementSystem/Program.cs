using System.Text.Json;
using WarehouseManagementSystem.Business;
using WarehouseManagementSystem.Domain;
using WarehouseManagementSystem.Domain.Extensions;

var items = new[]
   {
        new Item("PS1") { Price = 50},
        new Item("PS2") { Price = 60},
        new Item("PS4") { Price = 70},
        new Item("PS5") { Price = 80}
    };

var order = new Order(101, new(), items);

var ordersJson = JsonSerializer.Deserialize<Order[]>(File.ReadAllText("orders.json"));

var customer = new Customer("Gregg", "Carey")
{
    Address = new("123", "456")
};

void Processor_Completed(object? sender, OrderProcessCompletedEventArgs args)
{
    ArgumentNullException.ThrowIfNull(args.Order);//Also a null check
        var orderNumber = args.Order.OrderNumber;

        string orderNumberAsString  = orderNumber.ToString();
    

    ShippingProvider provider = args.Order!.ShippingProvider ?? new();//null forgiving operator (!). You telling the compiler this won't be null

    provider ??= new();//Null coalesing assignement. If left side is null, the right side will be assigned

    if(ShippingProviderValidator.ValidateShippingProvider(provider))
    {
        var name = provider.Name;
    }
}

var orderAsJson = JsonSerializer.Serialize(order, options: new() { WriteIndented = true });

Console.WriteLine(orderAsJson);