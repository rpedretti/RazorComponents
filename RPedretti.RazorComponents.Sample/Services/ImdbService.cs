using Microsoft.JSInterop;
using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using RPedretti.RazorComponents.Sample.Domain;
using System.Text.Json;
using RPedretti.RazorComponents.Sample.HttpClients;

namespace RPedretti.RazorComponents.Sample.Services
{
    public sealed class ImdbService : IMovieService
    {
        private readonly IImdbClient imdbClient;
        #region Fields

        private readonly ILogger<ImdbService> logger;

        #endregion Fields

        #region Constructors

        public ImdbService(IImdbClient imdbClient, ILogger<ImdbService> logger)
        {
            this.imdbClient = imdbClient;
            this.logger = logger;
        }

        #endregion Constructors

        #region Methods

        public async Task<MovieSearchResult> FindMoviesByPattern(string pattern, int page)
        {
            return await FindMoviesByPattern(pattern, page, CancellationToken.None);
        }

        public async Task<MovieSearchResult> FindMoviesByPattern(string pattern, int page, CancellationToken cancelationToken)
        {
            return await imdbClient.GetMoviesByPattern(pattern, page, cancelationToken);
        }

        public async Task<Movie> GetMovieById(string id)
        {
            return await GetMovieById(id, CancellationToken.None);
        }

        public async Task<Movie> GetMovieById(string id, CancellationToken cancelationToken)
        {
            return await imdbClient.GetMovieById(id, cancelationToken);
        }

        public async Task<Movie> GetMovieByTitle(string title)
        {
            return await GetMovieByTitle(title, CancellationToken.None);
        }

        public async Task<Movie> GetMovieByTitle(string title, CancellationToken cancelationToken)
        {
            return await imdbClient.GetMovieByTitle(title, cancelationToken);
        }

        #endregion Methods
    }
}
