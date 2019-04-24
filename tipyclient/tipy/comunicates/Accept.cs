using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KomunikatorTIP.comunicates
{
    [Serializable]
    public class Accept : IComunicates
    {
        public readonly int code = 1;
        public string CallAcceptedBy { get; set; }
        public string IP_CallAcceptedBy { get; set; }
        public string CallAcceptedFrom { get; set; }
        public string IP_CallAcceptedFrom { get; set; }
        public int Code => code;
    }
}
