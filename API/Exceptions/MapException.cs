using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Exceptions {
    public class MapException : Exception {
        public MapException(string message, Exception innerException) : base(message, innerException) {
        }
    }
}
