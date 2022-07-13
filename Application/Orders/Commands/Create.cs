using Application.Common.DataResponse;
using Application.Common.MediatoR;
using Application.Common.UnitOfWork;
using Application.Models.Orders;
using AutoMapper;
using Domain.Entity;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Orders.Commands
{
    public record OrderCreateRequest(OrderCreateModel Model) : ActResultRequestBase<Unit>;

    public class OrderCreateRequestHandler : ActResultRequestHandlerBase<OrderCreateRequest, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public OrderCreateRequestHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public override async Task<ActResult<Unit>> Handle(OrderCreateRequest request, CancellationToken cancellationToken)
        {
            var operation = ActResult.CreateResult<Unit>();

            var repository = _unitOfWork.GetRepository<Order>();
            var order = _mapper.Map<Order>(request.Model);

            var validProvider = await _unitOfWork.GetRepository<Provider>().ExistsAsync(x => x.Id == order.ProviderId);
            if (!validProvider)
            {
                operation.AddError($"Provider not created with id: {order.ProviderId}");
                return operation;
            }

            await repository.InsertAsync(order, cancellationToken);

            await _unitOfWork.SaveChangesAsync();
            if (_unitOfWork.LastSaveChangesResult.IsOk)
            {
                operation.AddSuccess($"Order {order.Id} successfully created");
                operation.Result = Unit.Value;
                return operation;
            }

            operation.AddError(_unitOfWork.LastSaveChangesResult.Exception);
            return operation;
        }
    }
}