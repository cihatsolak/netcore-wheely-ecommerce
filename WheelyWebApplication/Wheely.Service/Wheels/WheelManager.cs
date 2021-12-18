namespace Wheely.Service.Wheels
{
    public sealed class WheelManager : IWheelService
    {
        #region Fields
        private readonly IWheelRepository _wheelRepository;
        #endregion

        #region Constructor
        public WheelManager(IWheelRepository wheelRepository)
        {
            _wheelRepository = wheelRepository;
        }
        #endregion

        #region Methods
        public IDataResult<Wheel> GetWheelById(int id)
        {
            ValidateId(id);

            var wheel = _wheelRepository.GetWheelWithRelatedTablesById(id);
            if (wheel is null)
                return new ErrorDataResult<Wheel>();

            return new SuccessDataResult<Wheel>(wheel);
        }

        public IResult Update(Wheel wheel)
        {
            _wheelRepository.Update(wheel);
            return new SuccessResult();
        }
        #endregion

        #region Utilities
        private static void ValidateId(int id)
        {
            if (0 >= id)
            {
                throw new ArgumentException($"id cannot be less than zero", nameof(id));
            }
        }
        #endregion
    }
}
