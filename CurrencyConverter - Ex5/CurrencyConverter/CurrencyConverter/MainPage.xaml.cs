using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using System.Xml;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace CurrencyConverter
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page, INotifyPropertyChanged
    {
        private List<string> _currencyList;
        public List<string> CurrencyList
        {
            get { return _currencyList; }
            set
            {
                _currencyList = value;
                OnPropertyChanged();
            }
        }

        private int _selectedCurrencyFrom;
        public int SelectedCurrencyFrom
        {
            get { return _selectedCurrencyFrom; }
            set
            {
                if (Equals(value, _selectedCurrencyFrom)) return;
                _selectedCurrencyFrom = value;
                OnPropertyChanged();

            }
        }

        private int _selectedCurrencyTo;
        public int SelectedCurrencyTo
        {
            get { return _selectedCurrencyTo; }
            set
            {
                if (Equals(value, _selectedCurrencyTo)) return;
                _selectedCurrencyTo = value;
                OnPropertyChanged();
            }
        }

        //private decimal _cashAmount;
        //public decimal CashAmount
        //{
        //    get { return _cashAmount; }
        //    set
        //    {
        //        if (Equals(value, _cashAmount)) return;
        //        _cashAmount = value;
        //        OnPropertyChanged();
        //    }
        //}

        private string _logText;
        public string LogText
        {
            get { return _logText; }
            set
            {
                _logText += value + "\n";
                OnPropertyChanged();
            }
        }

        public void ChangeCurrency(object sender, SelectionChangedEventArgs e)
        {
            
        }

        private Currency ratetable;
        public void CurrencyExchange()
        {
            decimal result = ratetable.Exchange(decimal.Parse(TbxCurrencyAmount.Text),
                CurrencyList[SelectedCurrencyFrom],
                CurrencyList[SelectedCurrencyTo]);
            LogText = CurrencyList[SelectedCurrencyFrom] + "|"
                     + CurrencyList[SelectedCurrencyTo] + "|"
                     + TbxCurrencyAmount.Text + "|"
                     + String.Format("{0:0.00}",result) + "|"
                     + DateTime.Now.ToString("HH:mm:ss");
        }


        public MainPage()
        {
            this.InitializeComponent();

            ratetable = new Currency();

            this.DataContext = this;

            TbxCurrencyAmount.Text = "5000.00";

            CurrencyList = ratetable.GetCurrencyList();

            SelectedCurrencyFrom = 1;
            SelectedCurrencyTo = 2;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
