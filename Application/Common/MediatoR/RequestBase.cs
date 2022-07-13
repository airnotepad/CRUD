using Application.Common.DataResponse;
using MediatR;

namespace Application.Common.MediatoR
{
    public abstract record RequestBase : INamedRequest
    {
        public virtual string RequestName => GetType().Name;
    }

    public abstract record RequestBase<TResponse> : RequestBase, IRequest<TResponse>;
    public abstract record ActResultRequestBase<TResponse> : RequestBase<ActResult<TResponse>>;
}