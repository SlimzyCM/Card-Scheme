using System;
using System.Collections.Generic;
using System.Text;

namespace CardScheme.Domain.DTOs
{
    public class StatsToReturn
    {
        public bool Success { get; set; }
        public int Start { get; set; }
        public int Limit { get; set; }
        public int Size { get; set; }
        public List<Payload> Payload { get; set; }
    }

    public class Payload
    {
        public string BinCode { get; set; }
        public int HitCount { get; set; }
    }

}
