namespace Pong
{
    public interface IPoolHandler<T>
    {
        void ReturnToPool(T item);
    }
}
