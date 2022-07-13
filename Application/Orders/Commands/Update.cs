using Application.Common.DataResponse;
using Application.Common.Exceptions;
using Application.Common.MediatoR;
using Application.Common.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using Domain.Entity;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Application.Models.Orders;

namespace Application.Orders.Commands
{
    public record OrderUpdateRequest(OrderUpdateModel Model) : ActResultRequestBase<Unit>;

    public class OrderUpdateRequestHandler : ActResultRequestHandlerBase<OrderUpdateRequest, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public OrderUpdateRequestHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public override async Task<ActResult<Unit>> Handle(
            OrderUpdateRequest request,
            CancellationToken cancellationToken)
        {
            var operation = ActResult.CreateResult<Unit>();

            var repository = _unitOfWork.GetRepository<Order>();
            var order = await repository.GetFirstOrDefaultAsync(
                predicate: x => x.Id == request.Model.Id,
                include: i => i.Include(x => x.Items),
                disableTracking: false);

            if (order is null)
            {
                operation.AddError(new DataNotFoundException($"Order with Id {request.Model.Id} not found"));
                return operation;
            }

            _mapper.Map(request.Model, order);

            repository.Update(order);

            await _unitOfWork.SaveChangesAsync();
            if (_unitOfWork.LastSaveChangesResult.IsOk)
            {
                operation.AddSuccess($"Order {request.Model.Id} successfully updated");
                operation.Result = Unit.Value;
                return operation;
            }

            operation.AddError(_unitOfWork.LastSaveChangesResult.Exception);
            return operation;

        }
    }
}