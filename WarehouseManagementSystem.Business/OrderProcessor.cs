using WarehouseManagementSystem.Domain;

namespace WarehouseManagementSystem.Business
{
    public class OrderProcessor
    {
        
        public Func<Order,bool> OnOrderInitialized { get; set; }

        public event EventHandler<OrderCreatedEventArgs> OrderCreated;
        public event EventHandler<OrderProcessCompletedEventArgs> OrderProcessCompleted;

        protected virtual void OnOrderCreated(OrderCreatedEventArgs args)
        {
            OrderCreated?.Invoke(this, args);
        }

        protected virtual void OnOrderProcessCompleted(OrderProcessCompletedEventArgs args)
        {
            OrderProcessCompleted?.Invoke(this, args);
        }

        private void Initialize(Order order)
        {
            ArgumentNullException.ThrowIfNull(order);

            if (OnOrderInitialized?.Invoke(order) == false)
            {
                throw new Exception($"Coudn't initialize {order.OrderNumber}");
            }


        }

        public void Process(Order order)
        {
            // Run some code..

            Initialize(order);

            OnOrderCreated(new()
            {
                Order = order,
                OldTotal = 100,
                NewTotal = 80
            });

            OnOrderProcessCompleted(new() { Order = order });
        }

        public void Process(IEnumerable<Order> orders)
        {
            var summaries = orders.Select(order =>
            {
                return new
                {
                    Order = order.OrderNumber,
                    Items = order.LineItems.Count(),
                    Total = order.LineItems.Sum(item => item.Price)
                };
            });

            //Still have access to the anonomyous type since we are still in the method
            var orderedSummaries = summaries.OrderBy(summary => summary.Total);

            var summary = orderedSummaries.First();

            //with keyword allows changing the anonomyous type and adding new properties
            //reference types only have their reference coppied
            var summaryWithTax = summary with
            {
                Total = summary.Total * 1.25m
            };

        }
    }
}