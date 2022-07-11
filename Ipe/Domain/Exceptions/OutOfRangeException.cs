namespace Ipe.Domain.Errors;

public class OutOfRangeException : BaseException
{
    public OutOfRangeException() : base("011", "Out of Range")  { }
}
