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
        private const string rooturl = "http://127.0.0.1:5000";
        private string type;
        private string spot;
        private string strike;
        private string time;
        private string rate;
        private string vol;
        private string variable;
        private string variablename;
        private string min;
        private string max;
        private string a;
        private string b;
        private List<object> list4request;


        public OptionRequest(string type, string spot, string strike, string time, string rate, string vol)
        {
            list4request = new List<object>();
            this.variable = "price";
            list4request.Add(variable);
            this.type = type;
            list4request.Add(this.type);
            this.spot = CheckifDouble(spot);
            list4request.Add(this.spot);
            this.strike = CheckifDouble(strike);
            list4request.Add(this.strike);
            this.time = time;
            list4request.Add(this.time);
            this.rate = CheckifDouble(rate);
            list4request.Add(this.rate);
            this.vol = CheckifDouble(vol);
            list4request.Add(this.vol);
            buildRequest();
        }

        public string CheckifDouble(string param)
        {
            // Console.WriteLine("The following paramters need to be corrected :" + (param.Contains(".") ? param : param + ".0"));
            return (param.Contains(".") ? param : param + ".0");
        }

        public OptionRequest(string variable, string varname, string min, string max, string type, string time, string rate, string a, string b)
        {
            list4request = new List<object>();
            this.variable = variable;
            list4request.Add(variable);
            this.variablename = varname;
            list4request.Add(variablename);
            this.min = CheckifDouble(min);
            list4request.Add(this.min);
            this.max = CheckifDouble(max);
            list4request.Add(this.max);
            this.type = type;
            list4request.Add(type);
            this.time = time;
            list4request.Add(time);
            this.rate = CheckifDouble(rate);
            list4request.Add(this.rate);
            this.a = CheckifDouble(a);
            list4request.Add(this.a);
            this.b = CheckifDouble(b);
            list4request.Add(this.b);
            buildRequest();
        }


        public string Type { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public double Spot { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public double Strike { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public double Time { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public double Rate { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public double Vol { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public double Variable { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public double Min { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public double Max { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

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

        public Greecs Rfq_Handler(bool mutiple)
        {
            // Initiallize test values for building the URL to get the Json we want to deserialize
            // URL example : htp://127.0.0.1:5000/price/call/100/100/12/5/30 (htp://127.0.0.1:5000/price/type/spot/strike/time/rate/vol)
            WebClient client = new WebClient();
            string jsonoptionanswer = client.DownloadString(this.buildRequest().ToString());

            // Check print : Total path 4 Json file
            Console.WriteLine(this.buildRequest().ToString());
            Console.WriteLine("Got the Json, Ready to implement the dictionnary ...");

            var settings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                MissingMemberHandling = MissingMemberHandling.Ignore
            };
            //Having technically no idea of what type of value the Json is composed we will use dynamics objects (eventhough i have never experienced them)
            dynamic dynobject = JsonConvert.DeserializeObject<dynamic>(jsonoptionanswer, settings);

            // Initializing the Greecs class to store the Json infos into a dictionnary
            Greecs answerGreecs = new Greecs();
            answerGreecs.GreecsJson(dynobject, mutiple);
            Dictionary<Object, Object> myTest = answerGreecs.greecsdico;

            // Check print : Total path 4 Json file
            Console.WriteLine("Checking if the dictionnary is well implemented ...");
            Console.WriteLine(answerGreecs.ToString());

            return answerGreecs;

        }
    }
}
