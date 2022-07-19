namespace Samauma.Domain.Errors;
public class InvalidTreeIdException : BaseException
{
    public InvalidTreeIdException() : base("005", "Invalid Tree Id") { }
}