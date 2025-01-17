using System.ComponentModel.DataAnnotations;

namespace DefaultNamespace
{
    public record  CreateGameDTO
    (
         [Required][StringLength(50)]string Name,
         int GenreId,
         [Range(1,100)]decimal Price,
         string Date
     );
}