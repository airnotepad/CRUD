using Application.Common.Mappings;
using Application.Models.OrderItems;
using Domain.Entity;
using System;
using System.Collections.Generic;

namespace Application.Models.Orders
{
    public class OrderCreateModel : IMapFrom<Order>
    {
        public string Number { get; set; }
        public DateTime? Date { get; set; }
        public int? ProviderId { get; set; }
        public ICollection<OrderItemCreateModel>? Items { get; set; }
    }
}