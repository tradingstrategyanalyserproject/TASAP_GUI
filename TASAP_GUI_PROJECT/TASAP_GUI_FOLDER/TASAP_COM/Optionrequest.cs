using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace TASAP_COM
{
    public class OptionRequest : IOptionRequest
    {
        private const string rooturl = "http://127.0.0.1:5000/price";
        private string type;
        private int spot;
        private int strike;
        private int time;
        private int rate;
        private int vol;
        private int variable;
        private int min;
        private int max;
        private List<object> list4request;


        public OptionRequest(string type, int spot, int strike, int time, int rate, int vol)
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

        public OptionRequest(int variable, int min, int max, string type, int strike, int spot, int time, int rate, int vol)
        {
            list4request = new List<object>();
            this.variable = variable;
            list4request.Add(min);
            this.min = min;
            list4request.Add(max);
            this.max = max;
            list4request.Add(variable);
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


        public string Type { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public int Spot { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public int Strike { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public int Time { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public int Rate { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public int Vol { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public int Variable { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public int Min { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public int Max { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

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

        public int CompareTo(IOptionRequest other)
        {
            throw new NotImplementedException();
        }

        public Greecs Rfq_Handler()
        {
            // Initiallize test values for building the URL to get the Json we want to deserialize
            // URL example : htp://127.0.0.1:5000/price/call/100/100/12/5/30 (htp://127.0.0.1:5000/price/type/spot/strike/time/rate/vol)
            WebClient client = new WebClient();
            string jsonoptionanswer = client.DownloadString(this.buildRequest().ToString());

            // Check print : Total path 4 Json file
            Console.WriteLine(this.buildRequest().ToString());
            Console.WriteLine("Got the Json, Ready to implement the dictionnary ...");

            //Having technically no idea of what type of value the Json is composed we will use dynamics objects (eventhough i have never experienced them)
            dynamic dynobject = JsonConvert.DeserializeObject<dynamic>(jsonoptionanswer);

            // Initializing the Greecs class to store the Json infos into a dictionnary
            Greecs answerGreecs = new Greecs();
            answerGreecs.GreecsJson(dynobject);
            Dictionary<Object, Object> myTest = answerGreecs.greecsdico;

            // Check print : Total path 4 Json file
            Console.WriteLine("Checking if the dictionnary is well implemented ...");
            Console.WriteLine(answerGreecs.ToString());

            return answerGreecs;

        }
    }
}
