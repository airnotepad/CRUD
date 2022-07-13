using System.Threading.Tasks;
using System.Threading;
using Microsoft.EntityFrameworkCore;
using Domain.Entity;
using Application.Common.DataResponse;
using Application.Common.Exceptions;
using Application.Common.MediatoR;
using Application.Common.UnitOfWork;
using AutoMapper;
using System.Linq;
using System.Collections.Generic;
using Application.Common.Predicate;
using System.Linq.Expressions;
using System;

namespace Application.Orders.Queries.List
{
    public record OrderGetListRequest(string[] providerIds, string number, DateTime? fromDate, DateTime? toDate, string? itemName, string? itemUnit) : ActResultRequestBase<OrderGetListModel>;

    public class OrderGetListRequestHandler : ActResultRequestHandlerBase<OrderGetListRequest, OrderGetListModel>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public OrderGetListRequestHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public override async Task<ActResult<OrderGetListModel>> Handle(OrderGetListRequest request, CancellationToken cancellationToken)
        {
            var operation = ActResult.CreateResult<OrderGetListModel>();

            var predicate = BuildPredicate(request);

            var entity = await _unitOfWork.GetRepository<Order>()
                .GetAllAsync(
                predicate: predicate,
                include: i => i.Include(x => x.Provider),
                orderBy: o => o.OrderByDescending(x => x.Date));

            if (entity is null)
            {
                operation.AddError(new DataNotFoundException($"Db empty"));
                return operation;
            }

            operation.Result = new OrderGetListModel()
            {
                List = _mapper.Map<IList<OrderGetListItemModel>>(entity)
            };
            return operation;
        }

        //Фильтрация данных
        private Expression<Func<Order, bool>> BuildPredicate(OrderGetListRequest request)
        {
            var predicate = PredicateBuilder.True<Order>();

            if (request.providerIds is not null && request.providerIds.Any())
            {
                predicate = predicate.And(x => request.providerIds.Contains(x.ProviderId.ToString()));
            }

            if (!string.IsNullOrWhiteSpace(request.number))
            {
                predicate = predicate.And(x => x.Number.Contains(request.number));
            }

            if (request.fromDate is not null && request.fromDate.HasValue)
            {
                predicate = predicate.And(x => x.Date.Date >= request.fromDate.Value.Date);
            }

            if (request.toDate is not null && request.toDate.HasValue)
            {
                predicate = predicate.And(x => x.Date.Date <= request.toDate.Value.Date);
            }

            if (!string.IsNullOrWhiteSpace(request.itemName))
            {
                predicate = predicate.And(x => x.Items.Select(s => s.Name).Any(i => i.Contains(request.itemName)));
            }

            if (!string.IsNullOrWhiteSpace(request.itemUnit))
            {
                predicate = predicate.And(x => x.Items.Select(s => s.Unit).Any(i => i.Contains(request.itemUnit)));
            }

            return predicate;
        }
    }
}