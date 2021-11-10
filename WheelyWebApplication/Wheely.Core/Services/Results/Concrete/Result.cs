using Wheely.Core.Services.Results.Abstract;

namespace Wheely.Core.Services.Results.Concrete
{
    public class Result : IResult
    {
        #region Constructor
        public Result(bool success)
        {
            Success = success;
        }

        public Result(bool success, string message) : this(success)
        {
            Message = message;
        }
        #endregion

        #region Properties
        public bool Success { get; }
        public string Message { get; }
        #endregion
    }
}
