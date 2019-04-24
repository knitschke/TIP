using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KomunikatorTIP.comunicates
{
    [Serializable]
    public class Decline : IComunicates
    {
        public readonly int code = 2;
        public string CallDeclinedBy { get; set; }
        public string IP_CallDeclinedBy { get; set; }
        public string CallDeclinedFrom { get; set; }
        public string IP_CallDeclinedFrom { get; set; }
        public int Code => code;
    }
}
