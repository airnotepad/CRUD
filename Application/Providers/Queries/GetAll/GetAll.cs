using Application.Common.DataResponse;
using Application.Common.Exceptions;
using Application.Common.MediatoR;
using Application.Common.UnitOfWork;
using AutoMapper;
using Domain.Entity;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Providers.Queries.GetAll
{
    public record ProvidersGetAllRequest() : ActResultRequestBase<ProvidersGetAll>;

    public class ProvidersGetAllRequestHandler : ActResultRequestHandlerBase<ProvidersGetAllRequest, ProvidersGetAll>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ProvidersGetAllRequestHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public override async Task<ActResult<ProvidersGetAll>> Handle(ProvidersGetAllRequest request, CancellationToken cancellationToken)
        {
            var operation = ActResult.CreateResult<ProvidersGetAll>();

            var entity = await _unitOfWork.GetRepository<Provider>()
                .GetAllAsync(disableTracking: true);

            //добавление DISTINCT'а не сделано, поскольку значения вытягиваются из таблицы Провайдеров
            //даже при одинаковых Name, провайдеры имеют уникальный ID. Если использовать DISTINCT, то потеряется часть данных
            //ответственность за поле Name лежит на пользователе
            //если бы значения тянулись джоином из таблицы с ордерами, то тогда необходимо делать DISTINCT

            if (entity is null)
            {
                operation.AddError(new DataNotFoundException($"Db empty"));
                return operation;
            }

            operation.Result = new ProvidersGetAll()
            {
                List = entity
            };
            return operation;
        }
    }
}