namespace Wheely.Core.Services.Results.Abstract
{
    public partial interface IResult
    {
        bool Succeeded { get; }
        string Message { get; }
    }
}
