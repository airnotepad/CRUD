using Application.Common.DataResponse;
using MediatR;
using System.Threading.Tasks;
using System.Threading;

namespace Application.Common.MediatoR
{
    public abstract class RequestHandlerBase<TRequest, TResponse> : IRequestHandler<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
        public abstract Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken);
    }
    public abstract class ActResultRequestHandlerBase<TRequest, TResponse> : RequestHandlerBase<TRequest, ActResult<TResponse>> where TRequest : IRequest<ActResult<TResponse>> { }
}