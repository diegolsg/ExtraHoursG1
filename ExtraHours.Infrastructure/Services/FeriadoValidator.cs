using System.Text.RegularExpressions;
using System.Globalization;

public static class FeriadoValidator
{
    private static readonly HttpClient _http = new();

    public static async Task<bool> EsFeriadoColombiaAsync(DateTime fecha)
    {
        int año = fecha.Year;
        string url = $"https://www.festivos.com.co";
        string html = await _http.GetStringAsync(url);

        // Extraer fechas tipo "20 de julio" usando Regex
        var matches = Regex.Matches(html, @"(\d{1,2}) de ([a-zñ]+)", RegexOptions.IgnoreCase);

        foreach (Match m in matches)
        {
            if (int.TryParse(m.Groups[1].Value, out int dia))
            {
                string mesNombre = m.Groups[2].Value.ToLower();
                if (MesEnNumero(mesNombre, out int mes))
                {
                    var fechaFeriado = new DateTime(año, mes, dia);
                    if (fechaFeriado.Date == fecha.Date)
                        return true;
                }
            }
        }

        return false;
    }

    private static bool MesEnNumero(string mesTexto, out int mes)
    {
        try
        {
            mes = DateTime.ParseExact(mesTexto, "MMMM", new CultureInfo("es-ES")).Month;
            return true;
        }
        catch
        {
            mes = 0;
            return false;
        }
    }
}