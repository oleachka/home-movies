using MoviesApp.Data.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Data.Sqlite;
using Dapper;
using System.Linq;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Hosting;
using System.IO;

namespace MoviesApp.Data
{

    public class MoviesRepository : IMoviesRepository
    {
        private readonly string dbFile;
        private readonly IHostingEnvironment env;

        public MoviesRepository(IOptions<AppSettings> opts, IHostingEnvironment env)
        {
            this.dbFile = Path.Combine(env.ContentRootPath, opts.Value.DbPath);
            this.env = env;
        }
        public async Task<IEnumerable<Movie>> AllMoviesAsync()
        {
            string sql = $"select Movies.Id, Title, Genre, Genres.Name as GenreName, Rating " +
                $"from Movies " +
                $"Left JOIN Genres " +
                $"on Movies.Genre = Genres.Id ";
            using (var cnn = CreateConnection())
            {
                var movies = await cnn.QueryAsync<Movie>(sql);
                movies.ToList().ForEach(async (m) => m.Actors = await this.MovieActorsAsync(m.Id));

                return movies.ToList();
            }
        }
        public async Task<IEnumerable<Actor>> AllActorsAsync()
        {
            using (var cnn = CreateConnection())
            {
                var actors = await cnn.QueryAsync<Actor>("select * from Actors");
                return actors.ToList();
            }
        }
        public async Task<IEnumerable<Genre>> AllGenresAsync()
        {
            using (var cnn = CreateConnection())
            {
                var genre = await cnn.QueryAsync<Genre>("select * from Genres");
                return genre.ToList();
            }
        }
        public async Task<IEnumerable<Actor>> MovieActorsAsync(int movieId)
        {
            string sql = $"SELECT Actors.ActorName FROM Movies " +
                $"LEFT JOIN MovieActors " +
                $"On Movies.Id = MovieActors.MovieId " +
                $"LEFT JOIN Actors " +
                $"On MovieActors.ActorId = Actors.Id " +
                $"Where Movies.Id = {movieId}";
            using (var cnn = CreateConnection())
            {
                var actors = await cnn.QueryAsync<Actor>(sql);
                return actors.ToList();
            }
        }
        public async Task<int> AddMovieActorAsync(long movieId, int actorId )
        {
            string sql = $"INSERT INTO [MovieActors] " +
                       $"([MovieId],[ActorId]) VALUES ({movieId},{actorId})";
            using (var cnn = CreateConnection())
            {
                cnn.Open();
                var affectedRows = await cnn.ExecuteAsync(sql);

                return affectedRows;
            }
        }
        public async Task<int> AddMovieActorAsync(Actor actor)
        {
            string sql = $"INSERT INTO [Actors] " +
                       $"([ActorName]) VALUES ('{actor.ActorName}');" +
                       $"select last_insert_rowid()";
            using (var cnn = CreateConnection())
            {
                cnn.Open();
                var actorId = await cnn.QueryAsync<int>(sql);

                return actorId.First();
            }
        }
        public async Task<long> AddMovieAsync(Movie movie)
        {
            string sql = $"INSERT INTO Movies ([Title],[Genre],[Rating]) VALUES ('{movie.Title}',{movie.Genre},{movie.Rating});" +
                $"select last_insert_rowid()";
            
            IEnumerable<Actor> allActors = null;
            if (movie.ActorList != null && movie.ActorList.Length > 0)
            {
               allActors =  await AllActorsAsync();
            }
            using (var cnn = CreateConnection())
            {
                cnn.Open();
                var movieId = await cnn.QueryAsync<int>(sql);

                movie.Actors.ToList().ForEach(async a =>
                {
                    int? actorId = allActors
                        .Where(dbActor => dbActor.ActorName == a.ActorName)
                        .FirstOrDefault()?.Id;
                    if (actorId == null)                    
                        actorId = await this.AddMovieActorAsync(a);                    

                    await this.AddMovieActorAsync(movieId.First(), (int)actorId);
                });            
                
                return movieId.First();
            }
           
        }
        public async Task<int> UpdateMovieAsync(Movie movie)
        {
            string sql = $"UPDATE Movies SET" +
                $" [Title] = '{movie.Title}'," +
                $" [Genre] ={movie.Genre}," +
                $" [Rating]={movie.Rating}" +
                $" WHERE [Id]={movie.Id}";
            using (var cnn = CreateConnection())
            {
                cnn.Open();
                var affectedRows = await cnn.ExecuteAsync(sql);
                return affectedRows;
            }

        }
        private SqliteConnection CreateConnection()
        {
            return new SqliteConnection($"Data Source={dbFile}");
        }
    }
}
