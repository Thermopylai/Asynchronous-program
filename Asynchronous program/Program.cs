namespace Asynchronous_program
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            var weatherTask = GetWeatherAsync();
            var newsTask = GetNewsAsync();
            await Task.WhenAll(weatherTask, newsTask);
            Console.WriteLine("Weather:");
            Console.WriteLine(weatherTask.Result);
            Console.WriteLine();
            Console.WriteLine("News:");
            Console.WriteLine(newsTask.Result);
            Console.WriteLine("\nPress any key to exit...");
            Console.ReadKey();
        }
        static async Task<string> GetWeatherAsync() 
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri("https://api.open-meteo.com/v1/forecast?latitude=52.52&longitude=13.41&hourly=temperature_2m&timezone=auto");
            var weatherTask = client.GetStringAsync(client.BaseAddress);
            var weatherResult = await weatherTask;
            return "Weather Data: " + weatherResult;
        }
        static async Task<string> GetNewsAsync()
        {
            var result = string.Empty;
            var client = new HttpClient();

            try { 
                client.BaseAddress = new Uri("https://newsapi.org/v2/top-headlines?country=us&apiKey=6f93446b30744b86a0cf872aebb59712");
                var newsTask = client.GetStringAsync(client.BaseAddress);
                var newsResult = await newsTask;
                result = "News Data: " + newsResult;
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine("Error fetching news data: " + ex.Message);
            }
            return result;
        }
    }
}
