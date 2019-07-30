using System.Threading;
using System.Threading.Tasks;
using RPedretti.RazorComponents.Sample.Domain;

namespace RPedretti.RazorComponents.Sample.HttpClients
{
    public interface IImdbClient
    {
        Task<Movie> GetMovieById(string id);
        Task<Movie> GetMovieById(string id, CancellationToken cancelationToken);
        Task<Movie> GetMovieByTitle(string title);
        Task<Movie> GetMovieByTitle(string title, CancellationToken cancelationToken);
        Task<MovieSearchResult> GetMoviesByPattern(string pattern, int page, CancellationToken cancelationToken);
    }
}