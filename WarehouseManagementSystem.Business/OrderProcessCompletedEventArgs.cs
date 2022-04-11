using WarehouseManagementSystem.Domain;

namespace WarehouseManagementSystem.Business
{
    public class OrderProcessCompletedEventArgs
    {
        public Order? Order { get; set; } //Anyone that uses this probery MUST check for nulls
    }
}