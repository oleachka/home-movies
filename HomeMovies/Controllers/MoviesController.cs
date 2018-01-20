using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MoviesApp.Data;
using MoviesApp.Data.Models;

namespace myMovies.Controllers
{
    [Produces("application/json")]
    [Route("api/movies")]
    public class MoviesController : Controller
    {
        private readonly IMoviesRepository _repo;
        public MoviesController(IMoviesRepository repository)
        {
            _repo = repository;
        }
        [HttpGet]
        [Route("movies")]
        public async Task<IActionResult> AllMovies()
        {
            try
            {
                IEnumerable<Movie> data = await _repo.AllMoviesAsync();                
                return Ok(data);
            }
            catch(Exception e)
            {
                return BadRequest(e);
            }
            
        }
        [HttpGet]
        [Route("genres")]
        public async Task<IActionResult> AllGeneres()
        {
            try
            {
                var data = await _repo.AllGenresAsync();
                return Ok(data);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }

        }

        [HttpGet]
        [Route("actors")]
        public async Task<IActionResult> AllActors()
        {
            try
            {
                var data = await _repo.AllActorsAsync();
                return Ok(data);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }

        }

        [HttpGet]
        [Route("actors/{movieId}")]
        public async Task<IActionResult> AllActors(int movieId)
        {
            try
            {
                var data = await _repo.MovieActorsAsync(movieId);
                return Ok(data);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }

        }

        [HttpPost]
        [Route("addmovie")]
        public async Task<IActionResult> AddMovie([FromBody] Movie movie)
        {
            try
            {                
                await _repo.AddMovieAsync(movie);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }

        }
        [HttpPost]
        [Route("updatemovie")]
        public async Task<IActionResult> UpdateMovie([FromBody] Movie movie)
        {
            try
            {
                await _repo.UpdateMovieAsync(movie);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }

        }
        [HttpGet("imdb")]
        public async Task<IActionResult> SampleApi()
        {
            // movie search http://www.omdbapi.com/?s=kill&apikey=fab416ed
            // movie details http://www.omdbapi.com/?i=tt0266697&apikey=fab416ed (rating is there)
            // you have intellisense when they add movies and store the IMDB id in the movies table
            // this way you can look stuff up.

            
            return null;
        }
    }
}