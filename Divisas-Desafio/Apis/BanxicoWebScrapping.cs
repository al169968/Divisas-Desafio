using Divisas_Desafio.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net;
using System.Security.Policy;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using HtmlAgilityPack;
using System.Windows.Controls;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Runtime.CompilerServices;
using System.Windows.Markup;

namespace Divisas_Desafio.Apis
{
    internal class BanxicoWebScrapping
    {
        private const string Url = "https://www.banxico.org.mx/tipcamb/tipCamMIAction.do";
        banxicoModel oBanxico = new banxicoModel();
        public async void AsyncWebScrapping()
        {
            
            using (HttpClient client = new HttpClient())
            {
                var response = await client.GetAsync(Url);
                response.EnsureSuccessStatusCode();
                if (response.StatusCode == HttpStatusCode.OK)
                {

                    var message = await response.Content.ReadAsStreamAsync();
                    
                    string mensaje = "";
                }
                else
                {
                    Console.WriteLine("Hay un error: {0}", response.StatusCode);
                }
            }
        }
        public object LoadBanxico()
        {
            try
            {
                string data = "";

                // creating the HAP object
                var web = new HtmlWeb();

                // visiting the target web page 
                var document =  web.Load(Url);

                // getting the list of HTML product nodes 
                //var productHTMLElements = document.DocumentNode.SelectNodes;
                var HeaderNames = document.DocumentNode.SelectNodes("//tr[@class='renglonNon']");
               


                foreach (var item in HeaderNames)
                {
                    if (item.InnerText.Contains(DateTime.Today.ToString("d")))
                    {
                        data = item.InnerText;
                    }
                    else
                    {
                        break;
                    }
                    //Console.WriteLine(item.InnerHtml);
                    //Console.WriteLine(item.InnerText);
                }
               
                data = data.ToString();
                data = Regex.Replace(data, @"\s+", " ");
                var aux = data.Split();
                oBanxico.Fecha = aux[1].Any() ? aux[1] : "null";
                // oBanxico.Fecha = aux[1];
                oBanxico.FIX = aux[2].Any() ? aux[2] : "null";
                //oBanxico.FIX = aux[2];
                oBanxico.DOF = aux[3].Any() ? aux[3] : "null";
               // oBanxico.DOF = aux[3];
                oBanxico.Pagos = aux[4].Any() ? aux[4] : "null";
                // oBanxico.Pagos = aux[4];
                //Console.WriteLine(oBanxico.Pagos.ToString());

                return oBanxico;
               // return oBanxico.Pagos.ToString();
                //oMain.Banxicolbl.Content = oBanxico.Pagos.ToString();

            }
            catch (Exception ex)
            {
                
                Console.WriteLine(ex.Message);
                return "null";
            }
            
        }
    }
}
