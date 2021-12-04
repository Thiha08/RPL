namespace RPL.Core.Result
{
    public interface IResult
    {
        ResultStatus Status { get; }

        public string Message { get; set; }
    }
}
