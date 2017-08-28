namespace Model.Interface
{
    public interface IValidatable
    {
        bool Validate(out string errorMessage);
    }
}