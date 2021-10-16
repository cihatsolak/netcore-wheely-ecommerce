namespace Wheely.Core.Services.Results.Abstract
{
    public interface IResult
    {
        bool Success { get; }
        string Message { get; }
    }
}
