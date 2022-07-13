using System.Threading.Tasks;
using System.Threading;
using Microsoft.EntityFrameworkCore;
using Domain.Entity;
using Application.Common.DataResponse;
using Application.Common.Exceptions;
using Application.Common.MediatoR;
using Application.Common.UnitOfWork;
using AutoMapper;

namespace Application.Orders.Queries.Get
{
    public record OrderGetByIdRequest(int Id) : ActResultRequestBase<OrderGetModel>;

    public class OrderGetByIdRequestHandler : ActResultRequestHandlerBase<OrderGetByIdRequest, OrderGetModel>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public OrderGetByIdRequestHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public override async Task<ActResult<OrderGetModel>> Handle(OrderGetByIdRequest request, CancellationToken cancellationToken)
        {
            var operation = ActResult.CreateResult<OrderGetModel>();

            var entity = await _unitOfWork.GetRepository<Order>()
                .GetFirstOrDefaultAsync(
                    predicate: x => x.Id == request.Id,
                    include: i => i
                                   .Include(x => x.Items)
                                   .Include(x => x.Provider));

            if (entity is null)
            {
                operation.AddError(new DataNotFoundException($"Order not found with Id: {request.Id}"));
                return operation;
            }

            operation.Result = _mapper.Map<OrderGetModel>(entity); ;
            return operation;
        }
    }
}