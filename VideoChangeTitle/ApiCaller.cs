using Newtonsoft.Json;

namespace VideoChangeTitle
{
    public class ApiCaller
    {
        // Llama a una API y devuelve un objeto de tipo T con la repuesta de la API
        public static async Task<T> CallApi<T>(string apiUrl) where T : class
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    Console.WriteLine("-----------------------------------");
                    Console.WriteLine($"llamada a: {apiUrl}");
                    Console.WriteLine("-----------------------------------");
                    HttpResponseMessage response = await client.GetAsync(apiUrl);
                    response.EnsureSuccessStatusCode();

                    string responseBody = await response.Content.ReadAsStringAsync();
                    T? t = JsonConvert.DeserializeObject<T>(responseBody);
                    return t;
                }
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine($"Request error: {e.Message}");
                return null;
            }
        }
    }
}
