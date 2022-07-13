using Application.Common.DataResponse;
using Application.Common.Exceptions;
using Application.Common.MediatoR;
using Application.Common.UnitOfWork;
using AutoMapper;
using Domain.Entity;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Orders.Commands
{
    public record OrderRemoveRequest(int Id) : ActResultRequestBase<Unit>;

    public class OrderRemoveRequestHandler : ActResultRequestHandlerBase<OrderRemoveRequest, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public OrderRemoveRequestHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public override async Task<ActResult<Unit>> Handle(
            OrderRemoveRequest request,
            CancellationToken cancellationToken)
        {
            var operation = ActResult.CreateResult<Unit>();

            var repository = _unitOfWork.GetRepository<Order>();
            var order = await repository.GetFirstOrDefaultAsync(
                predicate: x => x.Id == request.Id,
                disableTracking: true);

            if (order is null)
            {
                operation.AddError(new DataNotFoundException($"Order with Id {request.Id} not found"));
                return operation;
            }

            repository.Delete(order);

            await _unitOfWork.SaveChangesAsync();
            if (_unitOfWork.LastSaveChangesResult.IsOk)
            {
                operation.AddSuccess($"Order {request.Id} successfully updated");
                operation.Result = Unit.Value;
                return operation;
            }

            operation.AddError(_unitOfWork.LastSaveChangesResult.Exception);
            return operation;

        }
    }
}