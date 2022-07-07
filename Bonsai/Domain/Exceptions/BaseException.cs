namespace Bonsai.Domain.Errors
{
    public class BaseException: Exception
    {
        public BaseException(string Code, string Message)
        {
            Data.Add("Code", "BON-"+Code);
            Data.Add("Message", Message);
            Data.Add("ShortMessage", Message.Replace(" ", string.Empty));
        }

    }
}
