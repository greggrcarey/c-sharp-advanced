using WarehouseManagementSystem.Domain;

namespace WarehouseManagementSystem.Business
{
    public class OrderProcessor
    {
        
        public Func<Order,bool> OnOrderInitialized { get; set; }

        public event EventHandler OrderCreated;

        protected virtual void OnOrderCreated()
        {
            OrderCreated?.Invoke(this, EventArgs.Empty);
        }

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

            OnOrderCreated();

            onCompleted?.Invoke(order);
        }
    }
}