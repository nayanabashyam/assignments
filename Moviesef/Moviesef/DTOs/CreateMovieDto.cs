namespace Movies_EF.DTOs
{
    public record CreateMovieDto(string Title, string Genre, DateTimeOffset ReleaseDate, double Rating);

}
