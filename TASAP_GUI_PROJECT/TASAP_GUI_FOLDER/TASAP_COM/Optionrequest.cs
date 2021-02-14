using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TASAP_COM
{
    public class Optionrequest
    {
        private const string rooturl = "http://127.0.0.1:5000/price";
        private string type;
        private double spot;
        private double strike;
        private int time;
        private double rate;
        private double vol;
        private List<object> list4request;


        public Optionrequest(string type, double spot, double strike, int time, double rate, double vol)
        {
            list4request = new List<object>();
            this.type = type;
            list4request.Add(type);
            this.spot = spot;
            list4request.Add(spot);
            this.strike = strike;
            list4request.Add(strike);
            this.time = time;
            list4request.Add(time);
            this.rate = rate;
            list4request.Add(rate);
            this.vol = vol;
            list4request.Add(vol);
            buildRequest();
        }

        public StringBuilder buildRequest()
        {
            StringBuilder request = new StringBuilder();
            request.Append(rooturl);
            foreach(object obj in list4request)
            {
                request.Append('/');
                request.Append(obj.ToString());           
            }
            return request;
        }
    }
}
