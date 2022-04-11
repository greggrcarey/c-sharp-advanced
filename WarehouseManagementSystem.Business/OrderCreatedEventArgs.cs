using WarehouseManagementSystem.Domain;

namespace WarehouseManagementSystem.Business
{
    public class OrderCreatedEventArgs
    {
        public Order Order { get; set; }//it does not matter if this set is init for nullablilty, since init is only for immutablity, not instance creation
        public decimal OldTotal { get; set; }
        public decimal NewTotal { get; set; }

        //Updating the ctor to set instance reference types to an instance is one way to remove nullable types warnings
        public OrderCreatedEventArgs(Order order)
        {
            Order = order;
        }
    }
}