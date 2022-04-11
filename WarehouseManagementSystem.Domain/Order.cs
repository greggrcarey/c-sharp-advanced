using System.Diagnostics.CodeAnalysis;
using System.Text;
using System.Text.Json.Serialization;

namespace WarehouseManagementSystem.Domain
{
    public record Order(
        [property: JsonPropertyName("total")]
         decimal Total,
        
        [AllowNull]
         ShippingProvider ShippingProvider = default,
         IEnumerable<Item> LineItems = default,
         bool IsReadyForShipment = true
    )
    {
        [JsonIgnore]
        public ShippingProvider ShippingProvider { get; init; } = ShippingProvider ?? new();
        //ShippingProvider written this way allows for a null value to be passed to the constructor, but if so, 
        //the ShippingProvier property will be set to a new instance of the Property 
        public Guid OrderNumber { get; init; } = Guid.NewGuid();

        public override int GetHashCode()
        {
            return HashCode.Combine(OrderNumber, ShippingProvider, Total, IsReadyForShipment, LineItems);
        }
       
        public string GenerateReport(string email)
        {
            //This implementation will override our extension method with the same signature 
            //since the compiler will prefer methods in the class over extension methods with the same signature.
            throw new NotImplementedException();
        }

        public void Deconstruct(out decimal total, out bool ready)
        {
            total = Total;
            //ready = IsReadyForShipment && ShippingProvider is not null;// deconstructed parameters can be computed
            ready = IsReadyForShipment;
        }
        //Deconstructors can be overloaded.
        //Returned parameters can be ignored with discard, but the matching method is called regardless
        public void Deconstruct(out decimal total, out bool ready, out IEnumerable<Item> items)
        {
            total = Total;
            ready = IsReadyForShipment;
            items = LineItems;
        }
        //switch expression in an expression body member. 
        private decimal CalculateFreightCost(Order order)
        => order.ShippingProvider switch
        {
            SwedishPostalServiceShippingProvider { DeliverNextDay: true } => 100m,
            SwedishPostalServiceShippingProvider => 0m,
            _ => 50m
        };
        //switch expression that captures result of pattern and operates on it.
        //var pattern is used because discard cannot access the default result
        private decimal CalculateFreightCost_TypePatternDeclarationPatternAndVarPattern(Order order)
        => order.ShippingProvider switch
        {
            SwedishPostalServiceShippingProvider { DeliverNextDay: true } provider => provider.FreightCost + 50m,
            SwedishPostalServiceShippingProvider provider => provider.FreightCost - 50m,
            var provider => provider?.FreightCost ?? 50m
        };
        //overwrite the ToString/ PrintMembers of a record
        protected virtual bool PrintMemebers(StringBuilder builder)
        {
            builder.Append("A custom implementation");
            return true;
        }

    }

    public record ProcessedOrder(
          decimal Total,
         ShippingProvider ShippingProvider,
         IEnumerable<Item> LineItems,
         bool IsReadyForShipment = true) : Order(Total, ShippingProvider, LineItems, IsReadyForShipment) 
    {
       
    }

    public record ShippedOrder(
         decimal Total,
         ShippingProvider ShippingProvider,
         IEnumerable<Item> LineItems
        ): Order(Total, ShippingProvider, LineItems, false) 
    {
        public DateTime ShippedDate { get; set; }
    }

    public record CanceledOrder(
           decimal Total,
           ShippingProvider ShippingProvider,
           IEnumerable<Item> LineItems
          ) : Order(Total, ShippingProvider, LineItems, false)
    {
        public DateTime CanceledDate { get; set; }
    }

    public class Item
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public bool InStock { get; set; }

        public Item(string Name)
        {
            this.Name = Name;
        }
    }
}