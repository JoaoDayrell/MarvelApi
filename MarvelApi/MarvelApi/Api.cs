using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using RestSharp;
using RestSharp.Authenticators;
using Newtonsoft.Json;
using System.Data;

namespace MarvelApi
{
    class Api
    {
        string link;
        public Api()
        {
            //link da API da Marvel
           link = "https://gateway.marvel.com:443";
        }

        public Personagens.Root getPersonagens(int offset)
        {
            // definie o link de acesso
            var client = new RestClient(link + "/v1/public/characters");
            //define o metodo como get
            var request = new RestRequest(Method.GET);
            //define o header da execução
            request.AddHeader("Content-Type", "application/json");
            //Define a Api Key
            request.AddParameter("apikey", "0056edc0d9c5766675698288a1a48ce0");
            //Define o Time stamp e o hash gerado a partir do MD5 do private e public API key
            request.AddParameter("ts", "1");
            request.AddParameter("hash", "70d91967af5eeaa00f8c14ea52f53419");
            //Define o limite máximo de requisições 
            request.AddParameter("limit", 100);
            //Testa e define o offset
            if(offset != 0)
            {
                request.AddParameter("offset", offset);
            }
            IRestResponse<Personagens.Root> response = client.Execute<Personagens.Root>(request);
            return response.Data;
        }
    }
}
