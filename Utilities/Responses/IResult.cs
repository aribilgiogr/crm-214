namespace Utilities.Responses
{
    public interface IResult
    {
        bool Success { get; }
        IEnumerable<string> Messages { get; }
    }

    public class Result : IResult
    {
        public bool Success { get; }
        public IEnumerable<string> Messages { get; } = [];

        public Result(bool success)
        {
            Success = success;
        }

        public Result(bool success, IEnumerable<string> messages)
        {
            Success = success;
            Messages = messages;
        }
    }

    public class SuccessResult : Result
    {
        public SuccessResult() : base(true) { }
        public SuccessResult(IEnumerable<string> messages) : base(true, messages) { }
    }

    public class ErrorResult : Result
    {
        public ErrorResult() : base(false) { }
        public ErrorResult(IEnumerable<string> messages) : base(false, messages) { }
    }
}
