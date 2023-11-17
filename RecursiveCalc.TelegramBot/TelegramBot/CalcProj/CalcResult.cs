using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelegramBot.Common
{
    public class CalcResult
    {
        public double Result { get; set; }
        public IEnumerable<string> History { get; set; }
    }
}
