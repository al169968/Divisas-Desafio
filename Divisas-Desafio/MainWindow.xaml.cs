using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Divisas_Desafio
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            showData();
        }
        private async void showData()
        {
            string currencylayer = "";
            
            Apis.CurrencyLayerAPI CurrencyLayer = new Apis.CurrencyLayerAPI();
            currencylayer = await CurrencyLayer.AsyncApiFunction();
            Console.WriteLine(currencylayer.ToString());

            Models.banxicoModel obanxicoModel = new Models.banxicoModel();   
            Apis.BanxicoWebScrapping oBanxico = new Apis.BanxicoWebScrapping();
            obanxicoModel = (Models.banxicoModel)oBanxico.LoadBanxico();

            Console.WriteLine(obanxicoModel.Pagos.ToString());

            //Banxicolbl.Content = BanxicoUSDtoMXN;


            //CurrencyLayerlbl.Content = ocurrencyLayer.quotes.USDMXN.ToString();
            // Banxicolbl.Content =  


        }
    }
}
