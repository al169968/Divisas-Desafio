using Divisas_Desafio.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Policy;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;

namespace Divisas_Desafio.Apis
{

    internal class CurrencyLayerAPI
    {

        private const string Url = "http://api.currencylayer.com/live?access_key=c5075a3a35ea6753df199a8362418209&currencies=MXN&source=USD&format=1";
        public CurrencyLayerAPI() { }
        /*public void currencyapi()
        {
            string json = new WebClient().DownloadString("http://api.currencylayer.com/live?access_key=c5075a3a35ea6753df199a8362418209&currencies=MXN&source=USD&format=1");

            json = Regex.Unescape(json);

            Models.ModelCurrencyLayer deserializeJson = JsonSerializer.Deserialize<ModelCurrencyLayer>(json);
            /*Console.WriteLine(deserializeJson.success);
            Console.WriteLine(deserializeJson.terms);
            Console.WriteLine(deserializeJson.privacy);
            Console.WriteLine(deserializeJson.source);*/

            /*Console.WriteLine(deserializeJson.quotes.USDMXN.ToString());



        }*/
        //Obtener los datos de la api de CurrencyLayer (Se necesita un Access Key en el URL)
        //------------Solo se puede usar 1000 veces la version free.----------------
        //La version free no permite usar el protocolo https en el url
        public async Task<string> AsyncApiFunction()
        {
            // string Url = "http://api.currencylayer.com/live?access_key=c5075a3a35ea6753df199a8362418209&currencies=MXN&source=USD&format=1";
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    var response = await client.GetAsync(Url);
                    response.EnsureSuccessStatusCode();
                    if (response.StatusCode == HttpStatusCode.OK)
                    {

                        string message = await response.Content.ReadAsStringAsync();
                        //Console.WriteLine(message);
                        message = Regex.Unescape(message);

                        Models.ModelCurrencyLayer deserializeJson = JsonSerializer.Deserialize<ModelCurrencyLayer>(message);
                        return deserializeJson.quotes.USDMXN.ToString();
                        //Console.WriteLine(deserializeJson.quotes.USDMXN.ToString());

                    }
                    else
                    {
                        
                        Console.WriteLine("Hay un error: {0}", response.StatusCode);
                        return null;
                    }
                }
            }
            catch(Exception ex) 
            {
               
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public async void AsyncFunction()
        {
            // string Url = "http://api.currencylayer.com/live?access_key=c5075a3a35ea6753df199a8362418209&currencies=MXN&source=USD&format=1";
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    var response = await client.GetAsync(Url);
                    response.EnsureSuccessStatusCode();
                    if (response.StatusCode == HttpStatusCode.OK)
                    {

                        string message = await response.Content.ReadAsStringAsync();
                        //Console.WriteLine(message);
                        message = Regex.Unescape(message);

                        Models.ModelCurrencyLayer deserializeJson = JsonSerializer.Deserialize<ModelCurrencyLayer>(message);

                        //Console.WriteLine(deserializeJson.quotes.USDMXN.ToString());

                    }
                    else
                    {
                        Console.WriteLine("Hay un error: {0}", response.StatusCode);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

    }

    
}

/*public class ModelCurrencyLayer
    {
        public bool success { get; set; }
        public string terms { get; set; }
        public string privacy { get; set; }
        public int timestamp { get; set; }
        public string source { get; set; }
        public conversion quotes { get; set; }


    }
    public class conversion
    {
        public float USDMXN { get; set; }
    }*/
