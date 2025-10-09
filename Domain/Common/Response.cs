using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Common
{
    public class Response<T>
    {
        public T Data { get; set; } = default!;
        public bool IsSuccess { get; set; }
        public string Message { get; set; } = string.Empty;
        public List<string> Errors { get; set; }

        public Response()
        {
            Errors = new List<string>();
        }

        public Response<T> Succes(T data, string message = "")
        {
            return new Response<T>
            {
                Data = data,
                IsSuccess = true,
                Message = message,
                Errors = new List<string>()
            };
        }

        public Response<T> Failure(List<string> errors, string message = "")
        {
            return new Response<T>
            {
                Data = default!,
                IsSuccess = false,
                Message = message,
                Errors = errors
            };
        }
    }
}
