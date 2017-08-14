namespace Visyn.Mathematics
{
    public interface IEqualsToleranced<T>
    {
        bool Equals(T other, double tolerance);
    }
}