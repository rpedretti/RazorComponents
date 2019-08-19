using Microsoft.Extensions.Logging;
using RPedretti.RazorComponents.Sample.Domain;
using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace RPedretti.RazorComponents.Sample.HttpClients
{
    public class ImdbClient : IImdbClient
    {
        #region Fields

        private const string key = "db5dadb9";
        private readonly ILogger<ImdbClient> logger;
        public HttpClient httpClient;

        #endregion Fields

        #region Constructors

        public ImdbClient(HttpClient httpClient, ILogger<ImdbClient> logger)
        {
            this.httpClient = httpClient;
            this.logger = logger;
            var baseUrl = $"http://www.omdbapi.com/";
            this.httpClient.BaseAddress = new Uri(baseUrl);
        }

        #endregion Constructors

        #region Methods

        public async Task<Movie> GetMovieById(string id)
        {
            return await GetMovieById(id, CancellationToken.None);
        }

        public async Task<Movie> GetMovieById(string id, CancellationToken cancelationToken)
        {
            var responseJson = await httpClient.GetAsync($"?apikey={key}&i={id}");
            var content = await responseJson.Content.ReadAsStringAsync();

            var movie = JsonSerializer.Deserialize<Movie>(content);
            return movie;
        }

        public async Task<Movie> GetMovieByTitle(string title)
        {
            return await GetMovieByTitle(title, CancellationToken.None);
        }

        public async Task<Movie> GetMovieByTitle(string title, CancellationToken cancelationToken)
        {
            var responseJson = await httpClient.GetAsync($"?apikey={key}&t={title}");
            var content = await responseJson.Content.ReadAsStringAsync();

            var movie = JsonSerializer.Deserialize<Movie>(content);
            return movie;
        }

        public async Task<MovieSearchResult> GetMoviesByPattern(string pattern, int page, CancellationToken cancelationToken)
        {
            try
            {
                var responseJson = await httpClient.GetAsync($"?apikey={key}&s={pattern}&page={page}", cancelationToken);
                var content = await responseJson.Content.ReadAsStringAsync();
                var movies = JsonSerializer.Deserialize<MovieSearchResult>(content);
                return movies;
            }
            catch (Exception e)
            {
                logger.LogError(e, e.Message);
                throw;
            }
        }

        #endregion Methods
    }
}
