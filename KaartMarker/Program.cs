using System.Net.Http;
using System.Text.Json;
using System.IO;

namespace KaartMarker
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            string url = "https://nominatim.openstreetmap.org/search?format=json&q=Roermond";

            using HttpClient client = new HttpClient();

            client.DefaultRequestHeaders.UserAgent.ParseAdd("KaartMarker/1.0");

            string resultaat = await client.GetStringAsync(url);

            JsonDocument json = JsonDocument.Parse(resultaat);

            JsonElement eersteResultaat = json.RootElement[0];

            string latitude = eersteResultaat.GetProperty("lat").GetString();
            string longitude = eersteResultaat.GetProperty("lon").GetString();

            Console.WriteLine("Latitude: " + latitude);
            Console.WriteLine("Longitude: " + longitude);

            string html = File.ReadAllText("index.html");

            html = html.Replace("LATTITUDE", latitude);
            html = html.Replace("LONGITUDE", longitude);

            File.WriteAllText("index.html", html);

        }
    }
}
