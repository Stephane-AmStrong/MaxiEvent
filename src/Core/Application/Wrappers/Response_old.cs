using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Wrappers
{
    public class Response_old<T>
    {
        public Response_old()
        {
        }

        public Response_old(T data,string message = null)
        {
            Succeeded = true;
            Message = message;
            Data = data;
        }

        public Response_old(string message)
        {
            Succeeded = false;
            Message = message;
        }
        public bool Succeeded { get; set; }
        public string Message { get; set; }
        public List<string> Errors { get; set; }
        public T Data { get; set; }
    }
}
