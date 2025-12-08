namespace TransportManagement.Application.Wrappers
{
    public class Result
    {
        public bool Succeeded { get; set; }
        public string Message { get; set; } = string.Empty;
        public List<string>? Errors { get; set; }
        public int StatusCode { get; set; }

        // نجاح بدون داتا
        public static Result Success(string message = "", int statusCode = 200)
            => new Result
            {
                Succeeded = true,
                Message = message,
                StatusCode = statusCode
            };

        // فشل برسالة واحدة
        public static Result Failure(string error, int statusCode = 500)
            => new Result
            {
                Succeeded = false,
                Message = error,
                StatusCode = statusCode,
                Errors = new List<string> { error }
            };

        // فشل بقائمة أخطاء
        public static Result Failure(IEnumerable<string> errors, int statusCode = 500, string message = "Validation failed")
            => new Result
            {
                Succeeded = false,
                Message = message,
                StatusCode = statusCode,
                Errors = errors.ToList()
            };
    }


    public class Result<T> : Result
    {
        public T? Data { get; set; }

        // نجاح مع داتا
        public static Result<T> Success(T data, string message = "", int statusCode = 200)
            => new Result<T>
            {
                Succeeded = true,
                Message = message,
                StatusCode = statusCode,
                Data = data
            };

        // فشل مع Error واحد
        public new static Result<T> Failure(string error, int statusCode = 500)
            => new Result<T>
            {
                Succeeded = false,
                Message = error,
                StatusCode = statusCode,
                Errors = new List<string> { error }
            };

        // فشل مع Errors متعددة
        public new static Result<T> Failure(IEnumerable<string> errors, int statusCode = 500, string message = "Validation failed")
            => new Result<T>
            {
                Succeeded = false,
                Message = message,
                StatusCode = statusCode,
                Errors = errors.ToList()
            };
    }
}