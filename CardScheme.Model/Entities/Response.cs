using System;
using System.Collections.Generic;
using System.Text;

namespace CardScheme.Domain.Entities
{
    /// <summary>
    /// generic class
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Response<T>
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public T Payload { get; set; }
    }
}
