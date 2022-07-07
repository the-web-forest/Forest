namespace Samauma.Domain.Errors
{
    public class BaseException: Exception
    {
        public BaseException(string Code, string Message)
        {
            Data.Add("Code", "SAM-"+Code);
            Data.Add("Message", Message);
            Data.Add("ShortMessage", Message.Replace(" ", string.Empty));
        }
    }
}
