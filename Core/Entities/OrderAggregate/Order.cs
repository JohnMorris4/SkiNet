using System;
using System.Collections.Generic;

namespace Core.Entities.OrderAggregate
{
    public class Order : BaseEntity
    {
        public Order()
        { }
        
        public Order(
            
            string buyerEmail, 
            Address shipToAddress, 
            DeliveryMethod deliveryMethod, 
            IReadOnlyList<OrderItem> orderItems, 
            decimal subTotal
            //decimal tax
            )
        {
            BuyerEmail = buyerEmail;
            ShipToAddress = shipToAddress;
            DeliveryMethod = deliveryMethod;
            OrderItems = orderItems;
            SubTotal = subTotal;
            //Tax = tax;
                
        }

        public string BuyerEmail { get; set; }
        public DateTimeOffset OrderDate { get; set; } = DateTimeOffset.Now;
        public Address ShipToAddress { get; set; }
        public DeliveryMethod DeliveryMethod { get; set; }
        public IReadOnlyList<OrderItem> OrderItems { get; set; }
        public decimal SubTotal { get; set; }
        public decimal Tax { get; set; } //Testing This out to see if it will work like I want it to
        public OrderStatus Status { get; set; } = OrderStatus.Pending;
        public string PaymentIntentId { get; set; }

        public decimal GetTotal()
        {
            return SubTotal  + DeliveryMethod.Price;
        }
    }
}