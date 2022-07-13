using MediatR;
using Microsoft.AspNetCore.Mvc;
using Web.Models;
using Application.Models.Orders;
using Application.Common.DataResponse;
using Application.Providers.Queries.GetAll;
using Application.Orders.Commands;
using Application.Orders.Queries.List;
using Application.Orders.Queries.Get;

namespace Web.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class APIController : ControllerBase
    {
        private readonly ILogger<APIController> _logger;
        private readonly ISender _mediator;

        public APIController(ILogger<APIController> logger, ISender mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<ActResult<ProvidersGetAll>>> Providers() => await _mediator.Send(new ProvidersGetAllRequest());

        [HttpPost]
        public async Task<ActionResult<ActResult<OrderGetListModel>>> Table([FromBody] TableFilter filter)
        {
            var request = new OrderGetListRequest(
                providerIds: filter.providers?.ToArray(),
                number: filter.number,
                fromDate: filter.from,
                toDate: filter.to,
                itemName: filter.itemName,
                itemUnit: filter.itemUnit);
            return await _mediator.Send(request);
        }

        [HttpPost]
        public async Task<ActionResult<ActResult<Unit>>> Create([FromBody] OrderCreateModel input) => await _mediator.Send(new OrderCreateRequest(input));

        [HttpGet]
        public async Task<ActionResult<ActResult<OrderGetModel>>> Get(int id) => await _mediator.Send(new OrderGetByIdRequest(id));

        [HttpPost]
        public async Task<ActionResult<ActResult<Unit>>> Update([FromBody] OrderUpdateModel input) => await _mediator.Send(new OrderUpdateRequest(input));

        [HttpGet]
        public async Task<ActionResult<ActResult<Unit>>> Remove(int id) => await _mediator.Send(new OrderRemoveRequest(id));
    }
}