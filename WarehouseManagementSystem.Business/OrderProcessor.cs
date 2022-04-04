using WarehouseManagementSystem.Domain;

namespace WarehouseManagementSystem.Business
{
    public class OrderProcessor
    {
        //delegate declaration
        public delegate bool OrderInitialized(Order order);
        public delegate void ProcessCompleted(Order order);

       
        public OrderInitialized OnOrderInitialized { get; set; }

        private void Initialize(Order order)
        {
            ArgumentNullException.ThrowIfNull(order);

            if(OnOrderInitialized?.Invoke(order) == false)
            {
                throw new Exception($"Coudn't initialize {order.OrderNumber}");
            }
            
         
        }

        public void Process(Order order, ProcessCompleted onCompleted = default)
        {
            // Run some code..

            Initialize(order);

          
            onCompleted?.Invoke(order);
        }
    }
}