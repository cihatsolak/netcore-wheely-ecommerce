using Wheely.Core.Services.Results.Abstract;

namespace Wheely.Core.Services.Results.Concrete
{
    public class DataResult<T> : Result, IDataResult<T>
    {
        #region Constructor
        public DataResult(T data, bool success) : base(success)
        {
            Data = data;
        }

        public DataResult(T data, bool success, string message) : base(success, message)
        {
            Data = data;
        }
        #endregion

        #region Properties
        public T Data { get; }
        #endregion
    }
}
