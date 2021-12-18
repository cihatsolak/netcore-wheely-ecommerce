namespace Wheely.Service.Wheels
{
    public partial interface IWheelService
    {
        IDataResult<Wheel> GetWheelById(int id);
        IResult Update(Wheel wheel);
    }
}
