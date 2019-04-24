using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KomunikatorTIP.comunicates
{
    [Serializable]
    public class Invite : IComunicates
    {
        public readonly int code = 0;
        public string RequestedBy { get; set; }
        public string IP_RequestedBy { get; set; }
        public string CalledUser { get; set; }
        public string IP_CalledUser { get; set; }
        public int Code => code;
    }
}
