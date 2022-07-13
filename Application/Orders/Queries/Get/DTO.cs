using Application.Common.Mappings;
using Domain.Entity;
using System;
using System.Collections.Generic;

namespace Application.Orders.Queries.Get
{
    public class OrderGetModel : IMapFrom<Order>
    {
        public int Id { get; set; }
        public string Number { get; set; }
        public DateTime Date { get; set; }
        public int ProviderId { get; set; }
        public Provider Provider { get; set; }
        public ICollection<OrderItemGetModel>? Items { get; set; }
    }

    public class OrderItemGetModel : IMapFrom<OrderItem>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Quantity { get; set; }
        public string Unit { get; set; }
    }
}