namespace Samauma.Domain.Errors;

public class OutOfRangeException : BaseException
{
    public OutOfRangeException() : base("002", "Out of Range")  { }
}
