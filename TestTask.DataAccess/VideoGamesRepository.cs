using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TestTask.Domain;

namespace TestTask.DataAccess
{
    public class VideoGamesRepository : IGamesRepository
    {
        private readonly VideoGamesContext context;

        public VideoGamesRepository(VideoGamesContext context)
        {
            this.context = context;
        }
        
        public async Task AddAsync(VideoGame game)
        {
            var g = new DataBaseVideoGame
                {Name = game.Name, DevelopersStudio = game.DevelopersStudio, Genres = game.Genres};
            await context.Games.AddAsync(g);
            await context.SaveChangesAsync();
        }

        public async Task<VideoGame> GetAsync(string name)
        {
            var game = await context.Games.FirstOrDefaultAsync(x => x.Name == name);
            var g = new VideoGame {Name = game.Name, DevelopersStudio = game.DevelopersStudio, Genres = game.Genres};
            return g;
        }
        
        public async Task<IEnumerable<VideoGame>> GetGamesOfGenreAsync(Genre genre)
        {
            var games = await context.Games.Where(x => x.Genres.Contains(genre)).ToListAsync();
            var res = games.Select(x => new VideoGame
                {Name = x.Name, DevelopersStudio = x.DevelopersStudio, Genres = x.Genres});
            return res;
        }

        private async Task<IEnumerable<DataBaseVideoGame>> GetGamesAsync(Predicate<DataBaseVideoGame> condition)
        {
            var games = await context.Games.Where(x => condition(x)).ToListAsync();
            return games;
        }


        public async Task<VideoGame> UpdateAsync(string name, UpdateData newData)
        {
            var game = await context.Games.FirstOrDefaultAsync(x => x.Name == name);
            game.DevelopersStudio = newData.NewCompanyName;
            game.Genres = newData.NewGenres;
            await context.SaveChangesAsync();
            return new VideoGame {Name = game.Name, DevelopersStudio = game.DevelopersStudio, Genres = game.Genres};
        }

        public async Task DeleteAsync(string name)
        {
            var game = await context.Games.FirstOrDefaultAsync(x => x.Name == name);
            context.Games.Remove(game);
            await context.SaveChangesAsync();
        }
    }
}//TODO в базе данных - своя сущность игры с Id
// при поступлении - просто мапим