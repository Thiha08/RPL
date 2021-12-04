using RPL.Core.Extensions;

namespace RPL.Core.Result
{
    public class Result : IResult
    {
        public Result() { }

        public ResultStatus Status { get; set; }

        public string Message { get; set; }

        private Result(ResultStatus status)
        {
            Status = status;
        }

        public static Result Ok(string message = null)
        {
            return new Result(ResultStatus.Ok) { Message = message ?? ResultStatus.Ok.ToDescription() };
        }

        //public static Result Created(string message = null)
        //{
        //    return new Result(ResultStatus.Created) { Message = message ?? ResultStatus.Created.ToDescription() };
        //}

        //public static Result Accepted(string message = null)
        //{
        //    return new Result(ResultStatus.Accepted) { Message = message ?? ResultStatus.Accepted.ToDescription() };
        //}

        //public static Result NoContent(string message = null)
        //{
        //    return new Result(ResultStatus.NoContent) { Message = message ?? ResultStatus.NoContent.ToDescription() };
        //}

        public static Result BadRequest(string message = null)
        {
            return new Result(ResultStatus.BadRequest) { Message = message ?? ResultStatus.BadRequest.ToDescription() };
        }

        //public static Result Unauthorized(string message = null)
        //{
        //    return new Result(ResultStatus.Unauthorized) { Message = message ?? ResultStatus.Unauthorized.ToDescription() };
        //}

        //public static Result Forbidden(string message = null)
        //{
        //    return new Result(ResultStatus.Forbidden) { Message = message ?? ResultStatus.Forbidden.ToDescription() };
        //}

        //public static Result NotFound(string message = null)
        //{
        //    return new Result(ResultStatus.NotFound) { Message = message ?? ResultStatus.NotFound.ToDescription() };
        //}

        public static Result InternalServerError(string message = null)
        {
            return new Result(ResultStatus.InternalServerError) { Message = message ?? ResultStatus.InternalServerError.ToDescription() };
        }

    }

    public class Result<T> : Result
    {
        protected Result(T data)
        {
            Data = data;
        }

        private Result(ResultStatus status)
        {
            Status = status;
        }

        public T Data { get; set; }

        public static implicit operator T(Result<T> result) => result.Data;
        public static implicit operator Result<T>(T value) => Ok(value);

        public PagedResult<T> ToPagedResult(PagedInfo pagedInfo)
        {
            var pagedResult = new PagedResult<T>(pagedInfo, Data)
            {
                Status = Status,
                Message = Message
            };

            return pagedResult;
        }

        public static Result<T> Ok(T data, string message = null)
        {
            return new Result<T>(data)
            {
                Status = ResultStatus.Ok,
                Message = message ?? ResultStatus.Ok.ToDescription()
            };
        }

        //public static Result<T> Created(T data, string message = null)
        //{
        //    return new Result<T>(data)
        //    {
        //        Status = ResultStatus.Created,
        //        Message = message ?? ResultStatus.Created.ToDescription()
        //    };
        //}

        //public static Result<T> Accepted(T data, string message = null)
        //{
        //    return new Result<T>(data)
        //    {
        //        Status = ResultStatus.Accepted,
        //        Message = message ?? ResultStatus.Accepted.ToDescription()
        //    };
        //}

        //public static new Result<T> NoContent(string message = null)
        //{
        //    return new Result<T>(ResultStatus.NoContent) { Message = message ?? ResultStatus.Accepted.ToDescription() };
        //}

        public static new Result<T> BadRequest(string message = null)
        {
            return new Result<T>(ResultStatus.BadRequest) { Message = message ?? ResultStatus.BadRequest.ToDescription() };
        }

        //public static new Result<T> Unauthorized(string message = null)
        //{
        //    return new Result<T>(ResultStatus.Unauthorized) { Message = message ?? ResultStatus.Unauthorized.ToDescription() };
        //}

        //public static new Result<T> Forbidden(string message = null)
        //{
        //    return new Result<T>(ResultStatus.Forbidden) { Message = message ?? ResultStatus.Forbidden.ToDescription() };
        //}

        //public static new Result<T> NotFound(string message = null)
        //{
        //    return new Result<T>(ResultStatus.NotFound) { Message = message ?? ResultStatus.NotFound.ToDescription() };
        //}

        public static new Result<T> InternalServerError(string message = null)
        {
            return new Result<T>(ResultStatus.InternalServerError) { Message = message ?? ResultStatus.InternalServerError.ToDescription() };
        }
    }
}