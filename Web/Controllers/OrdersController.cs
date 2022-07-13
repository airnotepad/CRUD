using Application.Models.OrderItems;
using Application.Models.Orders;
using Application.Orders.Queries.Get;
using Application.Providers.Queries.GetAll;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    public class OrdersController : Controller
    {
        private readonly ILogger<OrdersController> _logger;
        private readonly ISender _mediator;

        public OrdersController(ILogger<OrdersController> logger, ISender mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        public async Task<IActionResult> Update(int? Id)
        {
            var providers = await _mediator.Send(new ProvidersGetAllRequest());
            ViewBag.Providers = providers.Result.List;

            if (Id.HasValue)
            {
                var data = await _mediator.Send(new OrderGetByIdRequest(Id.Value));
                ViewBag.Data = data.Result;
            }
            else
                ViewBag.Data = new OrderCreateModel()
                {
                    Date = DateTime.Now,
                    Items = new List<OrderItemCreateModel>()
                };

            return View();
        }
    }
}