using FluentResults;

namespace Ipe.Util
{
    public class ReasonBuilder : IReason
    {
        public string Message { get; set; }

#nullable enable
        public Dictionary<string, object>? Metadata { get; set; }
#nullable disable

        public ReasonBuilder(string message, Dictionary<string, object> metadata = null)
        {
            Message = message;
            Metadata = metadata;
        }
    }
}
