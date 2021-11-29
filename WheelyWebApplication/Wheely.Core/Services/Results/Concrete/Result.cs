using Wheely.Core.Services.Results.Abstract;

namespace Wheely.Core.Services.Results.Concrete
{
    public class Result : IResult
    {
        #region Constructor
        public Result(bool success)
        {
            Succeeded = success;
        }

        public Result(bool success, string message) : this(success)
        {
            Message = message;
        }
        #endregion

        #region Properties
        public bool Succeeded { get; }
        public string Message { get; }
        #endregion
    }
}
