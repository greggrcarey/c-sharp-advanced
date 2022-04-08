using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarehouseManagementSystem.Domain.Extensions
{
    /*One note about Extension Methods is that they could only be written for classes that you 
     * don't control. This is only an example 
     */
    public static class OrderExtensions
    {
        public static string GenerateReport(this Order order)
        {
            var status = order switch
            {
                //Pattern matching in the switch statement requires access to a Deconstruct 
                //method, either explicit or complier generated or a TUPLE
                ( > 100, true) => "High Priority Order",
                ( > 50 and <= 100, true) => "Priority Order",
                (_, true) => "Order is ready",
                (_, false) => "Order is not ready",
                _ => throw new NullReferenceException()
            };

            //positional pattern
            var shippingProviderStatus = (order.ShippingProvider, order.LineItems.Count(), order.IsReadyForShipment)
                switch
            {
                (_, > 10, true) => "Multiple shipments!",
                (SwedishPostalServiceShippingProvider, 1, _) => "Manual pickup required",
                (_, _, true) => "Ready for shipment",
                _ => "Not ready for shipment"

            };

            return $"ORDER REPORT ({order.OrderNumber})" +
                $"{Environment.NewLine}" +
                $"Items: {order.LineItems.Count()}" +
                $"{Environment.NewLine}" +
                $"Total: {order.Total}" +
                $"{Environment.NewLine}" +
                $"{status} {shippingProviderStatus}";
        }
        public static string GenerateReport(this Order order, string recipient)
        {
            return $"ORDER REPORT ({order.OrderNumber})" +
                $"{Environment.NewLine}" +
                $"Items: {order.LineItems.Count()}" +
                $"{Environment.NewLine}" +
                $"Total: {order.Total}" +
                $"{Environment.NewLine}" +
                $"Send to: {recipient}";
        }

        public static string GenerateReport(this (Guid, int, decimal, IEnumerable<Item> ) order)
        {
            return $"ORDER REPORT ({order.Item1})" +
                $"{Environment.NewLine}" +
                $"Items: {order.Item2}" +
                $"{Environment.NewLine}" +
                $"Total: {order.Item3}" +
                $"{Environment.NewLine}";
                
        }
        public static void Deconstruct(
            this Order order,
            out Guid orderNumber,
            out decimal totalNumberOfItems,
            out IEnumerable<Item> items,
            out decimal averagePrice)
        {
            orderNumber = order.OrderNumber;
            totalNumberOfItems = order.LineItems.Count();
            items = order.LineItems;
            averagePrice = order.LineItems.Average(item => item.Price);
        }
    }
}
