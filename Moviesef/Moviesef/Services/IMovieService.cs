using Movies_EF.DTOs;

namespace Movies_EF.Services
{
    public interface IMovieService
    {
        Task<MovieDto> CreateMovieAsync(CreateMovieDto command);
        Task<MovieDto?> GetMovieByIdAsync(Guid id);
        Task<IEnumerable<MovieDto>> GetAllMoviesAsync();
        Task UpdateMovieAsync(Guid id, UpdateMovieDto command);
        Task DeleteMovieAsync(Guid id);
    }


}
