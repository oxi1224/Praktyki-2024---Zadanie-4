using System.Text.Json;

Console.WriteLine("Podaj kwotę w PLN:");
string? input = Console.ReadLine();
if (input == null) {
  return;
}
HttpClient client = new();
HttpResponseMessage res = await client.GetAsync("https://api.nbp.pl/api/exchangerates/rates/a/usd/");

string content = await res.Content.ReadAsStringAsync();
Kurs? kurs = JsonSerializer.Deserialize<Kurs>(content);
if (kurs == null) {
  Console.WriteLine("Nie udało się pobrać kursu USD");
  return;
}

float kwota = float.Parse(input.Trim());
Console.WriteLine($"Kwota w USD: {Math.Round(kwota / kurs.rates[0].mid, 2).ToString("N2")}");

public class Kurs {
  public string code { get; set; }
  public string currency { get; set; }
  public List<KursRates> rates { get; set; }
  public string table { get; set; }
}

public class KursRates {
  public string no { get; set; }
  public string effectiveDate { get; set; }
  public float mid { get; set; }
}