namespace DefaultNamespace
{
    public record GameSummaryDto(
        int Id,
        string Name,
        string Genre,
        decimal Price,
        string Date
    );
}