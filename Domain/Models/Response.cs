using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Response<T>
    {
        public bool Status { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }
        
        public static Response<T> Success(T data, string message = "Operation successful")
        {
            return new Response<T>
            {
                Status = true,
                Message = message,
                Data = data
            };
        }

        public static Response<T> Failure(string message)
        {
            return new Response<T>
            {
                Status = false,
                Message = message,
                Data = default
            };
        }
    }
}
