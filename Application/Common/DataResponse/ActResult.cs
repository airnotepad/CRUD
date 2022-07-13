using System;

namespace Application.Common.DataResponse
{
    public abstract class ActResult
    {
        public Metadata Metadata { get; set; }
        public Exception Exception { get; set; }
        public static ActResult<TResult> CreateResult<TResult>(TResult result, Exception exception = null)
        {
            var operation = new ActResult<TResult>
            {
                Result = result,
                Exception = exception
            };
            return operation;
        }
        public static ActResult<TResult> CreateResult<TResult>()
        {
            return CreateResult(default(TResult)!);
        }
    }

    public class ActResult<T> : ActResult
    {
        public T Result { get; set; }
        public bool Ok
        {
            get
            {
                if (Metadata == null)
                {
                    return Exception == null && Result != null;
                }
                return Exception == null
                       && Result != null
                       && Metadata?.Type != MetadataType.Error;
            }
        }
    }
}