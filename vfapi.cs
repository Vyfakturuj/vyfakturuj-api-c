using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

// pro testovani neni treba vyvojove prostredi. Testuj napr na http://rextester.com/
namespace Rextester
{
    class Program
    {
        private const string URL = "https://api.vyfakturuj.cz/2.0/"; // zakladni adresa API

		static HttpClient client = new HttpClient();
        
        public static void Main(string[] args)
        {
            Console.WriteLine("Zacatek programu"); // zacatek programu
            
            client.BaseAddress = new Uri(URL); // nastaveni URL adresy naseho API
			//client.DefaultRequestHeaders.Add("X-Debug","1"); // debugovaci hlavicka. Pokud je odeslana, zobrazi data, ktera server obdrzel
            //client.DefaultRequestHeaders.Add("Authorization","Basic " + Base64Encode(LOGIN + ":" + APIKEY)); // nastaveni autorizace pomoci base64 kodovaneho stringu slozeneho z loginu, dvojtecky a api klice.
            client.DefaultRequestHeaders.Add("X-Authorization","Basic " + Base64Encode(LOGIN + ":" + APIKEY)); // normalni autorizace asi v C# nefunguje. pouzivam specialni hlavicku

            // v tuto chvili je HttpClient nastaveny
            Task<string> R = TestAsync(); // volani funkce ktera provede pripojeni
            Console.WriteLine(R.Result.ToString());
            Console.WriteLine("Konec programu");
            Console.ReadLine();
        }

        public static async Task<string> TestAsync()
        {
            using (var response = await client.GetAsync("invoice/" + "66627"))
            {
                Console.WriteLine(response.RequestMessage.ToString()); // zobrazi co jsme odeslali
                return await response.Content.ReadAsStringAsync();
            }

        }      
        private static string Base64Encode(string plainText) 
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return System.Convert.ToBase64String(plainTextBytes);
        }  
    }
}
