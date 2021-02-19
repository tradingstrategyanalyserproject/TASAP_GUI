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

        double Variable { get; set; }

        double Spot { get; set; }

        double Strike { get; set; }

        double Time { get; set; }

        double Rate { get; set; }

        double Vol { get; set; }

    }
}
