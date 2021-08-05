namespace Goliath.Models
{
    public interface ICloneable<T> : System.ICloneable
    {
        T Clone();
    }
}