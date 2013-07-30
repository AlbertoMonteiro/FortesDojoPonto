using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO.IsolatedStorage;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Ponto.Navigation;

namespace Ponto.ViewModel
{
    public class FiltroBatidaViewModel : ViewModelBase
    {
        private readonly NavigationService navigationService;
        private Empresa empresaSelecionada;
        private string nome;
        private string rg;
        private DateTime dataInicial;
        private DateTime dataFinal;

        public FiltroBatidaViewModel(NavigationService navigationService)
        {
            this.navigationService = navigationService;
         
            Empresas = new ObservableCollection<Empresa>();
            RG = "2007009198057";
            if (IsInDesignMode)
            {
                Nome = "Alberto";
                DataInicial = DateTime.Now;
                DataFinal = DateTime.Now;
            }
            else
            {
                DataInicial = DateTime.Today.Subtract(TimeSpan.FromDays(5));
                DataFinal= DateTime.Today;
                var pontoService = new PontoService();
                pontoService.Empresas("2007009198057", AdicionaEmpresas);
                OnRelatorioClick = new RelayCommand(RelatorioClick);
            }
        }

        private void RelatorioClick()
        {
            IsolatedStorageSettings.ApplicationSettings["RG"] = RG;
            IsolatedStorageSettings.ApplicationSettings["Filtro"] = new Filtro(DataInicial, DataFinal, EmpresaSelecionada);
            navigationService.NavigateTo("/ExtratoView.xaml");
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

        public DateTime DataInicial
        {
            get { return dataInicial; }
            set
            {
                dataInicial = value;
                RaisePropertyChanged("DataInicial");
            }
        }

        public DateTime DataFinal
        {
            get { return dataFinal; }
            set
            {
                dataFinal = value;
                RaisePropertyChanged("DataFinal");
            }
        }

        public ObservableCollection<Empresa> Empresas { get; set; }

        public Empresa EmpresaSelecionada
        {
            get { return empresaSelecionada; }
            set
            {
                empresaSelecionada = value;
                RaisePropertyChanged("EmpresaSelecionada");
            }
        }

        public RelayCommand OnRelatorioClick { get; set; }

        private void AdicionaEmpresas(IEnumerable<Empresa> empresas)
        {
            foreach (var empresa in empresas)
                Empresas.Add(empresa);
        }

    }

    public class Empresa
    {
        public string Nome { get; set; }
        public string Codigo { get; set; }
        public string RazaoSocial { get; set; }
    }
}