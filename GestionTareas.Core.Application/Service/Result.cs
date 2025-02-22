using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionTareas.Core.Application.Service
{
    public class Result<T>
    {
        public string StatusCode { get; }
        public bool IsSuccess { get; }
        public T Data { get; }
        public string Error { get; }

        private Result(string statusCode, bool isSuccess, T data, string error)
        {
            StatusCode = statusCode;
            IsSuccess = isSuccess;
            Data = data;
            Error = error;
        }

        public static Result<T> Success(T data, string statusCode)
            => new Result<T>(statusCode, true, data, null);

        public static Result<T> Failure(string statusCode, string error)
            => new Result<T>(statusCode, false, default, error);
    }

}
