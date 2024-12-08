using Newtonsoft.Json;
using static VideoChangeTitle.Utils;
using static VideoChangeTitle.ApiCaller;

namespace VideoChangeTitle
{
    public class TheMovieDB
    {
        // Apikey obtenida de un ejemplo
        private static readonly string API_KEY = "6a5be4999abf74eba1f9a8311294c267";
        // URL base de la API
        private static readonly string API_URL = "https://api.themoviedb.org";

        // obtiene el texto para renombrar un capítulo de una serie.
        public static async Task<string> getTitle(string title, string seasson, string chapter)
        {
            if (string.IsNullOrWhiteSpace(title)) { throw new ArgumentNullException("title"); }
            if (string.IsNullOrWhiteSpace(chapter)) { throw new ArgumentNullException("chapter"); }
            if (string.IsNullOrWhiteSpace(seasson)) { throw new ArgumentNullException("seasson"); }

            println($"título: {title}, temporada: {seasson}, capítulo:{chapter}");

            TvShowResponse response = await getTvShowData(title);
            if (response == null)
            {
                println("-----------------------------------");
                println("no se ha encontrado la serie");
                println("-----------------------------------");
                return null;
            }

            println($"Id de la primera serie recibida: {response.Results[0].Id}");

            tvShowResult tvShowResult = new();

            foreach (tvShowResult item in response.Results)
            {
                if (item.Name.ToLower().Trim().Equals(title.ToLower().Trim()))
                {
                    tvShowResult = item;
                    println($"encontrado: {item.Name}");
                    break;
                }
            }

            if (tvShowResult.Id == 0)
            {
                Console.WriteLine("Ningún resultado coincide con la serie buscada");
                return null;
            }

            println($"id de la serie: {tvShowResult.Id}");
            TvShowChapterResponse tvShowChapterResponse = await getTvShowChapterData(tvShowResult.Id+"",seasson,chapter);

            if (tvShowChapterResponse == null)
            {
                tvShowChapterResponse= new TvShowChapterResponse();
                tvShowChapterResponse.Name = title;
            }

            return $"{title} T:{seasson} E:{chapter} - {tvShowChapterResponse.Name}";
        }
        
        
        // Obtiene los datos de la serie (incluido el id que es necesario para buscar datos de un capítulo de una temporada)
        private static async Task<TvShowResponse> getTvShowData(string title)
        {
            /*
                https://api.themoviedb.org/3/search/tv?query=mandalorian&include_adult=false&language=es-ES&page=1%27
            */
            string apiUrl = $"{API_URL}/3/search/tv?api_key={API_KEY}&query={title}&include_adult=true&language=es-ES&page=1%27";
            // llama a la api y devuelve un objeto TvShowResponse
            TvShowResponse response = await CallApi<TvShowResponse>(apiUrl);
            return response;
        }

        // Obtiene los datos de un capítulo de la serie (
        private static async Task<TvShowChapterResponse> getTvShowChapterData(string q_title_id, string q_seasson, string q_chapter)
        {
            /*
                https://api.themoviedb.org/3/tv/82856/season/3/episode/1?api_key=6a5be4999abf74eba1f9a8311294c267&language=es-ES
            */
            string apiUrl = $"{API_URL}/3/tv/{q_title_id}/season/{q_seasson}/episode/{q_chapter}?api_key={API_KEY}&language=es-ES";
            // Llama a la api y devuelve un objeto TvShowChapterResponse 
            TvShowChapterResponse response = await CallApi<TvShowChapterResponse>(apiUrl);
            return response;
        }


    }
}
