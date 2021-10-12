using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TestTask.Domain;

namespace TestTask.DataAccess
{
    public class VideoGamesRepository : IGamesRepository
    {
        private readonly VideoGamesContext context;
        private readonly IMapper mapper;

        public VideoGamesRepository(VideoGamesContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }
        
        public async Task AddAsync(VideoGame game)
        {
            await context.Games.AddAsync(mapper.Map<DataBaseVideoGame>(game));
            await context.SaveChangesAsync();
        }

        public async Task<VideoGame> GetAsync(string name)
        {
            var game = await context.Games.FirstOrDefaultAsync(x => x.Name == name);
            return mapper.Map<VideoGame>(game);
        }
        
        public async Task<IEnumerable<VideoGame>> GetGamesOfGenreAsync(Genre genre)
        {
            var games = await context.Games.Where(x => x.Genres.Contains(genre)).ToListAsync();
            var res = games.Select(x => mapper.Map<VideoGame>(x));
            return res;
        }

        public async Task<VideoGame> UpdateAsync(string name, UpdateData newData)
        {
            var game = await context.Games.FirstOrDefaultAsync(x => x.Name == name);
            game.DevelopersStudio = newData.NewCompanyName;
            game.Genres = newData.NewGenres;
            await context.SaveChangesAsync();
            return mapper.Map<VideoGame>(game);
        }

        public async Task DeleteAsync(string name)
        {
            var game = await context.Games.FirstOrDefaultAsync(x => x.Name == name);
            context.Games.Remove(game);
            await context.SaveChangesAsync();
        }

        public async Task<bool> IsGameExistsAsync(string name)
        {
            return await context.Games.AnyAsync(x => x.Name == name);
        }
    }
}//TODO в базе данных - своя сущность игры с Id
// при поступлении - просто мапим