using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Books.DAL
{
    public class BadDataException: Exception
    {
        private DateTime ErrorTime { get; set; }
        public DateTime PrevTime { get; set; }
        public BadDataException(DateTime errorTime, DateTime prevTime, string message) : base(message)
        {
            ErrorTime = errorTime;
            PrevTime = prevTime;
        }
    }
}
