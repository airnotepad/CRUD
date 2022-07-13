using Application.Common.Mappings;
using Domain.Entity;
using System;
using System.Collections.Generic;

namespace Application.Orders.Queries.List
{
    public class OrderGetListModel
    {
        public IEnumerable<OrderGetListItemModel> List { get; set; }
    }

    public class OrderGetListItemModel : IMapFrom<Order>
    {
        public int Id { get; set; }
        public string Number { get; set; }
        public DateTime Date { get; set; }
        public Provider Provider { get; set; }
    }
}