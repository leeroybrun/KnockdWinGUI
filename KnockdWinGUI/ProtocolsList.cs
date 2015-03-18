using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnockdWinGUI
{
    public class ProtocolsList : List<string>
    {
        public ProtocolsList()
        {
            this.Add("tcp");
            this.Add("udp");
        }
    }
}
