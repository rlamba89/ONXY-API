namespace Onyx.Core.Exceptions
{
    public class InvalidRequestBodyException : Exception
    {
        public string[] Errors { get; set; }
    }
}
