using MoviesApp.Data.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MoviesApp.Data
{
    public interface IMoviesRepository
    {
        Task<IEnumerable<Movie>> AllMoviesAsync();
        Task<IEnumerable<Actor>> AllActorsAsync();

        Task<IEnumerable<Genre>> AllGenresAsync();

        Task<IEnumerable<Actor>> MovieActorsAsync(int movieId);
        Task<long> AddMovieAsync(Movie movie);
        Task<int> UpdateMovieAsync(Movie movie);
    }
}
