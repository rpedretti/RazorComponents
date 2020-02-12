using RPedretti.RazorComponents.Sample.Shared.Domain;
using RPedretti.RazorComponents.Sample.Shared.HttpClients;
using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;

namespace RPedretti.RazorComponents.Sample.Shared.Services
{
    public sealed class ImdbService : IMovieService
    {
        #region Fields

        private readonly IImdbClient _imdbClient;

        #endregion Fields

        #region Constructors

        public ImdbService(IImdbClient imdbClient)
        {
            this._imdbClient = imdbClient;
        }

        #endregion Constructors

        #region Methods

        public async Task<MovieSearchResult> FindMoviesByPattern([DisallowNull] string pattern, int page)
        {
            return await FindMoviesByPattern(pattern, page, CancellationToken.None);
        }

        public async Task<MovieSearchResult> FindMoviesByPattern([DisallowNull] string pattern, int page, CancellationToken cancelationToken)
        {
            return await _imdbClient.GetMoviesByPattern(pattern, page, cancelationToken);
        }

        public async Task<Movie> GetMovieById([DisallowNull] string id)
        {
            return await GetMovieById(id, CancellationToken.None);
        }

        public async Task<Movie> GetMovieById([DisallowNull] string id, CancellationToken cancelationToken)
        {
            return await _imdbClient.GetMovieById(id, cancelationToken);
        }

        public async Task<Movie> GetMovieByTitle([DisallowNull] string title)
        {
            return await GetMovieByTitle(title, CancellationToken.None);
        }

        public async Task<Movie> GetMovieByTitle(string title, CancellationToken cancelationToken)
        {
            return await _imdbClient.GetMovieByTitle(title, cancelationToken);
        }

        #endregion Methods
    }
}
