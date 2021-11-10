namespace Wheely.Core.Services.Results.Abstract
{
    public partial interface IDataResult<T> : IResult
    {
        T Data { get; }
    }
}
