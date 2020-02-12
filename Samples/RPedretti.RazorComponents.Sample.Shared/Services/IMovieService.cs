using RPedretti.RazorComponents.Sample.Shared.Domain;
using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;

namespace RPedretti.RazorComponents.Sample.Shared.Services
{
    public interface IMovieService
    {
        #region Methods

        Task<MovieSearchResult> FindMoviesByPattern([DisallowNull] string pattern, int page, CancellationToken cancelationToken);

        Task<MovieSearchResult> FindMoviesByPattern([DisallowNull] string pattern, int page);

        Task<Movie> GetMovieById([DisallowNull] string id, CancellationToken cancelationToken);

        Task<Movie> GetMovieById([DisallowNull] string id);

        Task<Movie> GetMovieByTitle([DisallowNull] string title, CancellationToken cancelationToken);

        Task<Movie> GetMovieByTitle([DisallowNull] string title);

        #endregion Methods
    }
}
