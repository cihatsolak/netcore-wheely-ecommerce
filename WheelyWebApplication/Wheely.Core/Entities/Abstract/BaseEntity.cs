namespace Wheely.Core.Entities.Abstract
{
    public abstract class BaseEntity<T> : IEntity where T : struct
    {
        public T Id { get; set; }
    }
}
