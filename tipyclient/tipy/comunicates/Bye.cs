using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KomunikatorTIP.comunicates
{
    [Serializable]
    public class Bye : IComunicates
    {
        public readonly int code = 3;
        public string ByeSentBy { get; set; }
        public string ByeSentTo { get; set; }
        public int Code => code;
    }
}
