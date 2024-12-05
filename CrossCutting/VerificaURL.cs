using System.Text.RegularExpressions;

namespace TechNation.CrossCutting
{
    public static class VerificaURL
    {
        public static bool ContemLink(this string texto)
        {
            // Expressão regular para detectar URLs
            string pattern = @"http[s]?://[\w]+(.[\w]+)+";
            Regex regex = new Regex(pattern);
            return regex.IsMatch(texto);
        }
    }
}
