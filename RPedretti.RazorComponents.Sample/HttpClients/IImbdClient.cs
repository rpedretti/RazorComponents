using RPedretti.RazorComponents.Sample.Domain;
using System.Threading;
using System.Threading.Tasks;

namespace RPedretti.RazorComponents.Sample.HttpClients
{
    public interface IImdbClient
    {
        #region Methods

        Task<Movie> GetMovieById(string id);

        Task<Movie> GetMovieById(string id, CancellationToken cancelationToken);

        Task<Movie> GetMovieByTitle(string title);

        Task<Movie> GetMovieByTitle(string title, CancellationToken cancelationToken);

        Task<MovieSearchResult> GetMoviesByPattern(string pattern, int page, CancellationToken cancelationToken);

        #endregion Methods
    }
}
