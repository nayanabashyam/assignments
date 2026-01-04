namespace Movies_EF.DTOs
{
    public record UpdateMovieDto(string Title, string Genre, DateTimeOffset ReleaseDate, double Rating);

}
