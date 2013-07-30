using System;
using System.Collections.Generic;
using System.Net;
using Ponto.ViewModel;

namespace Ponto
{
    public class PontoService
    {
        private const string URL_BASE = @"http://10.1.4.109:8087/datasnap/rest/TSMPonto/";
        private const string GET_BATIDA_EMPREGADO = @"GetBatidaEmpregado/{0}/{1}/{2}/{3}/";
        private const string GET_EMPRESAS_EMPREGADO = @"getempresaempregado/{0}";

        public void Batidas(string RG, string Empresa, DateTime DataInicial, DateTime DataFinal, Action<IEnumerable<Historico>> callback)
        {
            var deserializer = new Deserializer<IEnumerable<Historico>>(callback);

            var request = new WebClient();

            var url = string.Format(URL_BASE + GET_BATIDA_EMPREGADO, RG, Empresa, DataInicial.RemoveBarras(), DataFinal.RemoveBarras());
            var uri = new Uri(url, UriKind.Absolute);

            request.DownloadStringCompleted += deserializer.Deserializa;
            request.DownloadStringAsync(uri);
        }

        public void Empresas(string rg, Action<IEnumerable<Empresa>> callback)
        {
            var deserializer = new Deserializer<IEnumerable<Empresa>>(callback);

            var request = new WebClient();

            var url = string.Format(URL_BASE + GET_EMPRESAS_EMPREGADO, rg);
            var uri = new Uri(url, UriKind.Absolute);

            request.DownloadStringCompleted += deserializer.Deserializa;
            request.DownloadStringAsync(uri);
        }

    }

    public class Result
    {
        public string[] result { get; set; }
    }
}