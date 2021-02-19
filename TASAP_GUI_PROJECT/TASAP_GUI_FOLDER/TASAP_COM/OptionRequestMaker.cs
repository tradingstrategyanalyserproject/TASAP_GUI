using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TASAP_COM
{
    public class OptionRequestMaker
    {
        public static OptionRequest MakeOptionRequest(string type, string spot, string strike, string time, string rate, string vol)
        {
            return new OptionRequest(type, spot, strike, time, rate, vol);
        }

        public static OptionRequest MakeOptionRequest(string variable, string varname, string min, string max, string type, string time, string rate, string a, string b)
        {
            return new OptionRequest(variable, varname, min, max, type, time, rate, a, b);
        }

    }
}
