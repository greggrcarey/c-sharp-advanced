namespace WarehouseManagementSystem.Domain
{
    public class Order : IEquatable<Order?>
    {
        public Guid OrderNumber { get; init; }
        public ShippingProvider ShippingProvider { get; init; }
        public int Total { get; }
        public bool IsReadyForShipment { get; set; } = true;
        public IEnumerable<Item> LineItems { get; set; }

        public Order()
        {
            OrderNumber = Guid.NewGuid();
        }

        public override bool Equals(object? obj)
        {
            return Equals(obj as Order);
        }

        public bool Equals(Order? other)
        {
            return other != null &&
                   OrderNumber.Equals(other.OrderNumber) &&
                   EqualityComparer<ShippingProvider>.Default.Equals(ShippingProvider, other.ShippingProvider) &&
                   Total == other.Total &&
                   IsReadyForShipment == other.IsReadyForShipment &&
                   EqualityComparer<IEnumerable<Item>>.Default.Equals(LineItems, other.LineItems);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(OrderNumber, ShippingProvider, Total, IsReadyForShipment, LineItems);
        }

        public static bool operator ==(Order? left, Order? right)
        {
            return EqualityComparer<Order>.Default.Equals(left, right);
        }

        public static bool operator !=(Order? left, Order? right)
        {
            return !(left == right);
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


    }

    public class ProcessedOrder : Order { }

    public class Item
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public bool InStock { get; set; }
    }
}