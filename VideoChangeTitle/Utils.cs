using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace VideoChangeTitle
{
    public class Utils
    {
        public static void println(Object obj)
        {
            if(obj == null) { obj = ""; }
            Console.WriteLine(obj.ToString());
        }

        public static void print(Object obj)
        {
            if (obj == null) { obj = ""; }
            Console.Write(obj.ToString());
        }

        public static string[] parseTitle(string title)
        {
            var match = Regex.Match(title, @"^(.*) - (\d+)x(\d+)");
            if (match.Success)
            {
                string titulo = match.Groups[1].Value.Trim();
                string temporada = match.Groups[2].Value;
                string episodio = match.Groups[3].Value;
                return new string[] { titulo, temporada, episodio };
            }
            return new string[0]; // Devuelve un array vacío si no coincide el patrón
        }
    
    }

}
