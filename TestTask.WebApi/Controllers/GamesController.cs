using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TestTask.Domain;

namespace TestTask.WebApi.Controllers
{
    [ApiController]
    [Route("api/games")]
    public class GamesController : Controller
    {
        private readonly IGamesService gamesService;

        public GamesController(IGamesService gamesService) //add mapper
        {
            this.gamesService = gamesService;
        }

        [HttpPost("create")]
        public async Task<ActionResult> CreateAlgorithmAsync([FromBody] CreateVideoGameRequest request)
        {
            var game = new VideoGame
                {Name = request.Name, DevelopersStudio = request.DevelopersStudio, Genres = request.Genres};
            try
            {
                await gamesService.CreateGameAsync(game);
                return Ok();
            }
            catch (ArgumentException e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("{name}")]
        public async Task<ActionResult<VideoGame>> GetGameAsync([FromRoute] string name)
        {
            try
            {
                var game = await gamesService.GetGameAsync(name);
                return Ok(game);
            }
            catch (ArgumentException e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost("update/{name}")]
        public async Task<ActionResult> UpdateGameAsync([FromRoute] string name, [FromBody] UpdateVideoGameRequest request)
        {
            var updateData = new UpdateData {NewCompanyName = request.NewCompanyName, NewGenres = request.NewGenres};
            try
            {
                await gamesService.UpdateGameAsync(name, updateData);
                return Ok();
            }
            catch (ArgumentException e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("{name}")]
        public async Task<ActionResult> DeleteGameAsync([FromRoute] string name)
        {
            try
            {
                await gamesService.DeleteGameAsync(name);
                return Ok();
            }
            catch (ArgumentException e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("genres/{genre}")]
        public async Task<ActionResult<List<VideoGame>>> GetGamesOfStringGenreAsync([FromRoute] string genre)
        {
            if (!Enum.TryParse<Genre>(genre, true, out var res))
                return BadRequest($"No such genre: {genre}");
            var games = await gamesService.GetAllGamesOfGenre(res);
            return Ok(games);
        }
    }
}