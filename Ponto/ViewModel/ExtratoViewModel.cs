using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Windows.Navigation;
using GalaSoft.MvvmLight;
using NavigationService = Ponto.Navigation.NavigationService;

namespace Ponto.ViewModel
{
    public class ExtratoViewModel : ViewModelBase
    {
        private readonly NavigationService navigationService;
        private string nome;
        private string periodo;
        private string rg;
        private string saldo;
        private string saldoInicial;

        public ExtratoViewModel(NavigationService navigationService)
        {
            this.navigationService = navigationService;
            
            navigationService.Navigated += Navegado;

            Historico = new ObservableCollection<Historico>();
            if (IsInDesignMode)
            {
                Nome = "Alberto";
                RG = "2007009198057";
                Periodo = "01/01/2013 a 31/01/2013";
                Saldo = "09:00D";
                SaldoInicial = "09:00D";

                for (var i = 1; i < 10; i++)
                    Historico.Add(new Historico
                    {
                        Data = new DateTime(2013, 01, i),
                        Previsto = "08:30",
                        Realizado = "08:30",
                        Diferenca = "00:00",
                        Saldo = "09:00D",
                    });
            }
        }

        private void Navegado(object sender, NavigationEventArgs e)
        {
            var url = e.Uri.ToString();

            if (url.Contains("/ExtratoView.xaml"))
            {
                var rg = IsolatedStorageSettings.ApplicationSettings["RG"].ToString();
                var filtro = IsolatedStorageSettings.ApplicationSettings["Filtro"] as Filtro;

                var pontoService = new PontoService();
                pontoService.Batidas(rg, filtro.Empresa.Codigo, filtro.DataInicial, filtro.DataFinal, AdicionaHistorios);
            }
        }


        public string SaldoInicial
        {
            get { return saldoInicial; }
            set
            {
                saldoInicial = value;
                RaisePropertyChanged("SaldoInicial");
            }
        }

        public string Nome
        {
            get { return nome; }
            set
            {
                nome = value;
                RaisePropertyChanged("Nome");
            }
        }

        public string RG
        {
            get { return rg; }
            set
            {
                rg = value;
                RaisePropertyChanged("RG");
            }
        }

        public string Periodo
        {
            get { return periodo; }
            set
            {
                periodo = value;
                RaisePropertyChanged("Periodo");
            }
        }

        public string Saldo
        {
            get { return saldo; }
            set
            {
                saldo = value;
                RaisePropertyChanged("Saldo");
            }
        }

        public ObservableCollection<Historico> Historico { get; set; }

        private void AdicionaHistorios(IEnumerable<Historico> historicos)
        {
            var saldoAtual = historicos.FirstOrDefault();
            var saldoInicial = historicos.LastOrDefault();

            Saldo = saldoAtual.Saldo;
            SaldoInicial = saldoInicial.Saldo;

            var total = historicos.Count() - 2;
            foreach (var historico in historicos.Skip(1).Take(total))
                Historico.Add(historico);
        }
    }

    public class Informacao
    {
        public string Descricao { get; set; }
    }
}