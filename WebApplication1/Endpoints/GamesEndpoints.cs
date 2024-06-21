using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Entities;
using WebApplication1.Mapping;

namespace DefaultNamespace;

public static class GamesEndpoints
{
    private static readonly List<GameSummaryDto> games =
    [
        new(1, "AB", "Fight", 13.13M, "2002.09.09"),
        new(2, "FG", "Action", 23.34M, "2012.09.10")
    ];
    
    public static RouteGroupBuilder MapGamesEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("games").WithParameterValidation();
        
        
        group.MapGet("/", async (GameStoreContext dbContext) =>
           await dbContext.Games
                .Include(game=>game.Genre)
                .Select(game=>game.ToGameSummaryDto())
                .AsNoTracking().ToListAsync());
        
        

        group.MapGet("/{id}",async (int id,GameStoreContext dbContext) =>
        {
            Game? game = await dbContext.Games.FindAsync(id);
            return game is null ? Results.NotFound() : Results.Ok(game.ToGameDetailsDto());

        }).WithName("GetGame");
        
        


        group.MapPost("/", async (CreateGameDTO createGameDTO,GameStoreContext dbContext) =>
        {
            // if (string.IsNullOrEmpty(createGameDTO.Name))
            // {
            //     return Results.BadRequest("NAME IS REQUIRD");
            // }
            
            // Game game =  new()
            // {
            //     Name = createGameDTO.Name,
            //     Genre = dbContext.Genres.Find(createGameDTO.GenreId),
            //     GenreId = createGameDTO.GenreId,
            //     Price = createGameDTO.Price,
            //     Date = createGameDTO.Date
            //     
            // };
            
            Game game = createGameDTO.ToEntity();
            
            dbContext.Games.Add(game);
            await dbContext.SaveChangesAsync();
            
            return Results.CreatedAtRoute("GetGame", new { id = game.Id }, game.ToGameDetailsDto());
            //return Results.Created($"/games/{gameDto.Id}", gameDto);
        });
        
        


        group.MapPut("/{id}", async (int id, UpdateGameDTO updateGameDto,GameStoreContext dbContext) =>
        {
            var existingGame = await dbContext.Games.FindAsync(id);

            if (existingGame is null)
            {
                return Results.NotFound();
            }
            
            dbContext.Entry(existingGame).CurrentValues.SetValues(updateGameDto.ToEntity(id));
            await dbContext.SaveChangesAsync();
            return Results.NoContent();
        });
        


        group.MapDelete("/{id}",async (int id,GameStoreContext dbContext) =>
        {
           await dbContext.Games
                .Where(game => game.Id == id)
                .ExecuteDeleteAsync();
            return Results.NoContent();
        });

        return group;
    }
}