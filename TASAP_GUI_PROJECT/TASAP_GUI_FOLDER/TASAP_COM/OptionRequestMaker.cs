using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TASAP_COM
{
    public class OptionRequestMaker
    {
        public static OptionRequest MakeOptionRequest(string type, int spot, int strike, int time, int rate, int vol)
        {
            return new OptionRequest(type, spot, strike, time, rate, vol);
        }

        public static OptionRequest MakeOptionRequest(int variable, int min, int max, string type, int strike, int spot, int time, int rate, int vol)
        {
            return new OptionRequest(variable, min, max, type, strike, spot, time, rate, vol);
        }
    }
}
