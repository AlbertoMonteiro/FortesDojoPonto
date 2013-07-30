using System;

namespace Ponto.ViewModel
{
    public class Filtro
    {
        public DateTime DataInicial { get; set; }
        public DateTime DataFinal { get; set; }
        public Empresa Empresa { get; set; }

        public Filtro(DateTime dataInicial, DateTime dataFinal, Empresa empresa)
        {
            DataInicial = dataInicial;
            DataFinal = dataFinal;
            Empresa = empresa;
        }
    }
}