using System;
using System.Collections.Generic;

namespace Ponto.ViewModel
{
    public class Historico
    {
        public DateTime Data { get; set; }
        public string Previsto { get; set; }
        public string Realizado { get; set; }
        public string Diferenca { get; set; }
        public string Saldo { get; set; }
        public IList<Informacao> Informacoes { get; set; }
    }
}