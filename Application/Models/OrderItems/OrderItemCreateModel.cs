using Application.Common.Mappings;
using Domain.Entity;

namespace Application.Models.OrderItems
{
    public class OrderItemCreateModel : IMapFrom<OrderItem>
    {
        public string Name { get; set; }
        public decimal Quantity { get; set; }
        public string Unit { get; set; }
    }
}
