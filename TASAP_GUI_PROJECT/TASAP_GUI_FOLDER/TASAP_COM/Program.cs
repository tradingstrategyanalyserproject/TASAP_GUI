using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace TASAP_COM
{
    class Program
    {
        static void Main(string[] args)
        {
            // Initiallize test values for building the URL to get the Json we want to deserialize
            // URL example : htp://127.0.0.1:5000/price/call/100/100/12/5/30 (htp://127.0.0.1:5000/price/type/spot/strike/time/rate/vol)
            WebClient client = new WebClient();
            Optionrequest optionrequest = new Optionrequest("put",100,100,12,5,30);
            string jsonoptionanswer = client.DownloadString(optionrequest.buildRequest().ToString());

            // Check print : Total path 4 Json file
            Console.WriteLine(optionrequest.buildRequest().ToString());
            Console.WriteLine("Got the Json, Ready to implement the dictionnary ...");

            //Having technically no idea of what type of value the Json is composed we will use dynamics objects (eventhough i have never experienced them)
            dynamic dynobject = JsonConvert.DeserializeObject<dynamic>(jsonoptionanswer);

            // Initializing the Greecs class to store the Json infos into a dictionnary
            Greecs answerGreecs = new Greecs();
            answerGreecs.GreecsJson(dynobject);
            Dictionary<Object, string> myTest = answerGreecs.greecsdico;

            // Check print : Total path 4 Json file
            Console.WriteLine("Checking if the dictionnary is well implemented ...");
            Console.WriteLine(answerGreecs.ToString());

            // The while loop is here only for the terminal to not close and show u the values but should be remove at end ...
            while (true) { }
        }

    }
}
