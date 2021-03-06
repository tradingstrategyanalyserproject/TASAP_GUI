﻿
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TASAP_COM
{
    public class Greecs
    {
        public Dictionary<Object, Object> greecsdico;

        public Greecs()
        {
            this.greecsdico = new Dictionary<Object, Object>();
        }

        public void GreecsJson(dynamic dynamicObj, bool mutiple)
        {
            if (mutiple)
            {
                for(int i = 0; i < dynamicObj.Count; i++)
                {
                    foreach (var item in dynamicObj[i])
                    {
                        Console.WriteLine(item.ToString());
                        greecsdico[item.Name+i.ToString()] = item.Value;
                    }
                }
                
            }
            else
            {
                foreach (var item in dynamicObj)
                {
                    Console.WriteLine(item.ToString());
                    greecsdico[item.Name] = item.Value;
                }
            }
        }

        public string ToString()
        {
            StringBuilder mystb = new StringBuilder(); 
            foreach (var item in greecsdico)
            {
                mystb.Append(item.Key.ToString());
                mystb.Append(" : ");
                mystb.Append(greecsdico[item.Key].ToString());
                mystb.Append("\n");
            }
            return mystb.ToString();
        }
    }
}
