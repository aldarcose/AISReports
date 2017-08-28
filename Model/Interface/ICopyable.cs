namespace Model.Interface
{
    public interface ICopyable<T>
    {
        void Copy(T original);
    }
}