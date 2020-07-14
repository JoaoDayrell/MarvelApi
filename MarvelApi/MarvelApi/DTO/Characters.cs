using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarvelApi
{
    public class Personagens
    {   //Criação das classes e variaveis necessaria para a importação dos dados
        public class Root
        {
            public Data data { get; set; }
        }
        public class Data
        {
          public List<Result> results { get; set; }
        }
        public class Result
        {           
            public int id { get ; set; }
            public string name { get; set; }
            public string description { get ; set ; }
            public DateTime modified { get ; set ; }
        }
    }
}

