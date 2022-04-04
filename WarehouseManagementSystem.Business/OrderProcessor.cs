using WarehouseManagementSystem.Domain;

namespace WarehouseManagementSystem.Business
{
    public class OrderProcessor
    {
        //delegate declaration
        public delegate void OrderInitialized();
        public delegate void ProcessCompleted();

        //This property can refernce ANY void method without paramters
        //Not just the delegate we defined above
        public OrderInitialized OnOrderInitialized { get; set; }

        private void Initialize(Order order)
        {
            ArgumentNullException.ThrowIfNull(order);

            OnOrderInitialized?.Invoke();
            //Call the delegate through the property
            //The delegate is a type, so it has its own properties also, even if it is null
        }

        public void Process(Order order, ProcessCompleted onCompleted = default)
        {
            // Run some code..

            Initialize(order);

            //By passing the delegate as a parameter to the method,
            //we are able to more tightly controll access to the delegate
            onCompleted?.Invoke();
        }
    }
}