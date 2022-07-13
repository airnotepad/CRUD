using MediatR;
using Microsoft.AspNetCore.Mvc;
using Application.Providers.Queries.GetAll;

namespace Web.Controllers
{
    public class DashboardController : Controller
    {
        private readonly ILogger<DashboardController> _logger;
        private readonly ISender _mediator;

        public DashboardController(ILogger<DashboardController> logger, ISender mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        public async Task<IActionResult> IndexAsync()
        {
            var providers = await _mediator.Send(new ProvidersGetAllRequest());
            ViewBag.Providers = providers.Result.List;
            return View();
        }
    }
}