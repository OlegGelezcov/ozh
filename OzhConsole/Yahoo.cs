using LaYumba.Functional;
using System.Net.Http;
using static System.Console;

namespace OzhConsole {
    static class Yahoo {
        public static decimal GetRate(string ccyPair) {
            WriteLine($"fetching rate...");
            var uri = $"http://finance.yahoo.com/d/quotes.csv?f=l1&s={ccyPair}=X";
            var request = new HttpClient().GetStringAsync(uri);
            return decimal.Parse(request.Result.Trim());
        }

        public static Try<decimal> TryGetRate(string ccyPair)
            => () => GetRate(ccyPair);
    }
}
