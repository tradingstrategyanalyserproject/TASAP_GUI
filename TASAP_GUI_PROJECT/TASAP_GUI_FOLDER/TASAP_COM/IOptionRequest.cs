using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TASAP_COM
{
    public interface IOptionRequest : IComparable<IOptionRequest>
    {
   
        String Type { get; set; }

        int Variable { get; set; }

        int Spot { get; set; }

        int Strike { get; set; }

        int Time { get; set; }

        int Rate { get; set; }

        int Vol { get; set; }

    }
}
