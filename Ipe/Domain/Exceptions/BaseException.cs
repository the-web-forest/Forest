namespace Ipe.Domain.Errors
{
    public class BaseException: Exception
    {
        public BaseException(string Code, string Message)
        {
            Data.Add("Code", "IPE-"+Code);
            Data.Add("Message", Message);
            Data.Add("ShortMessage", Message.Replace(" ", string.Empty));
        }

    }
}
