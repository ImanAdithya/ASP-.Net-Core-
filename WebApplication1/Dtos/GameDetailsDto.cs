namespace DefaultNamespace;

public record GameDetailsDto(
    int Id,
    string Name,
    int GenreId,
    decimal Price,
    string Date
    );