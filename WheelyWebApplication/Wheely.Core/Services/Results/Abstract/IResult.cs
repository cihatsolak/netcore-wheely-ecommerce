namespace Wheely.Core.Services.Results.Abstract
{
    public partial interface IResult
    {
        bool Success { get; }
        string Message { get; }
    }
}
