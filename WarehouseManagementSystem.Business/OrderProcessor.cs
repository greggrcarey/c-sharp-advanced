using WarehouseManagementSystem.Domain;

namespace WarehouseManagementSystem.Business
{
    public class OrderProcessor
    {
        //delegate declaration with property on the class
        //public delegate bool OrderInitialized(Order order);
        //public OrderInitialized OnOrderInitialized { get; set; }

        //public delegate void ProcessCompleted(Order order);
        //Refactored OnOrderInitialized with Func
        public Func<Order,bool> OnOrderInitialized { get; set; }


        private void Initialize(Order order)
        {
            ArgumentNullException.ThrowIfNull(order);

            if (OnOrderInitialized?.Invoke(order) == false)
            {
                throw new Exception($"Coudn't initialize {order.OrderNumber}");
            }


        }

        public void Process(Order order, Action<Order> onCompleted = default)
        {
            // Run some code..

            Initialize(order);


            onCompleted?.Invoke(order);
        }
    }
}