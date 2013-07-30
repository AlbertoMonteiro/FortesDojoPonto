using System;

namespace Ponto
{
    public static class DateTimeExtensions
    {
        public static string RemoveBarras(this DateTime data)
        {
            return data.ToString("ddMMyyyy");
        }
    }
}