using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Mapping;

namespace DefaultNamespace;

public static class GenreEndPoints
{
    public static RouteGroupBuilder MapGenreEndPoint(this WebApplication app)
    {
        var group = app.MapGroup("genres").WithParameterValidation();

        group.MapGet("/", async (GameStoreContext dbContext) => 
            await dbContext.Genres
                .Select(genre=>genre.ToDto()).AsNoTracking().ToListAsync()
            );
        
        

        return group;
    }
}