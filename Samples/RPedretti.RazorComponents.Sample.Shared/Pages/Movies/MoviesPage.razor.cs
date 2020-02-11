using Microsoft.AspNetCore.Components;
using RPedretti.RazorComponents.Sample.Shared.Models;
using RPedretti.RazorComponents.Sample.Shared.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace RPedretti.RazorComponents.Sample.Shared.Pages.Movies
{
    public partial class MoviesPage
    {
        #region Fields

        private readonly Dictionary<int, IEnumerable<MoviePosterModel>> CachedMovies = new Dictionary<int, IEnumerable<MoviePosterModel>>();

        private CancellationTokenSource? _requestToken;

        #endregion Fields

        #region Properties

#pragma warning disable CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.
        [Inject] private IMovieService _movieService { get; set; }
#pragma warning restore CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.

        protected int CurrentPage { get; set; }

        protected bool HasContent { get; set; }

        protected bool Loading { get; set; }

        protected List<MoviePosterModel> Movies { get; set; } = new List<MoviePosterModel>();

        protected int MoviesCount { get; set; }

        protected int PageCount { get; set; }

        protected string? SearchMovieTitle { get; set; }

        #endregion Properties

        #region Methods

        protected void ClearMovies()
        {
            Movies.Clear();
            MoviesCount = 0;
            HasContent = false;
            CachedMovies.Clear();
        }

        protected async Task GetMoviesAsync(int page = 1)
        {
            IEnumerable<MoviePosterModel> movies;

            Loading = true;

            Movies.Clear();
            CurrentPage = page;

            if (!CachedMovies.ContainsKey(page))
            {
                try
                {
                    if (_requestToken != null)
                    {
                        _requestToken.Cancel();
                    }

                    _requestToken = new CancellationTokenSource();

                    var moviesResult = await _movieService.FindMoviesByPattern(SearchMovieTitle, page, _requestToken.Token);

                    if (bool.Parse(moviesResult.Response))
                    {
                        movies = moviesResult.Search.Select(m => new MoviePosterModel
                        {
                            Id = m.imdbID,
                            Plot = m.Plot,
                            Poster = m.Poster,
                            Title = m.Title
                        });

                        CachedMovies[page] = movies;
                        if (MoviesCount == 0)
                        {
                            MoviesCount = int.Parse(moviesResult.totalResults);
                            HasContent = MoviesCount > 0;
                            PageCount = (int)Math.Ceiling(MoviesCount / 10d);
                        }

                        movies = CachedMovies[page];
                        Movies.AddRange(movies);
                    }
                    else
                    {
                        HasContent = false;
                    }
                }
                catch (OperationCanceledException)
                {
                }
                catch
                {
                    HasContent = false;
                }
            }
            else
            {
                movies = CachedMovies[page];
                Movies.AddRange(movies);
            }

            Loading = false;
        }

        protected override void OnInitialized()
        {
            HasContent = MoviesCount > 0;
        }

        protected async Task RequestPage(int page)
        {
            await GetMoviesAsync(page);
        }

        public void GoToMovie(MoviePosterModel model)
        {
            Console.WriteLine($"Olar filme {model.Title} ({model.Id})");
        }

        public async Task SearchAsync()
        {
            ClearMovies();
            await GetMoviesAsync();
        }

        #endregion Methods
    }
}
