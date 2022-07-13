using Application.Common.Mappings;
using Domain.Entity;

namespace Application.Models.OrderItems
{
    public class OrderItemUpdateModel : IMapFrom<OrderItem>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Quantity { get; set; }
        public string Unit { get; set; }
    }
}