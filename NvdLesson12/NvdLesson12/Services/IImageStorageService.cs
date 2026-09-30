namespace NvdLesson12.Services;

public interface IImageStorageService
{
    Task<string> SaveImageAsync(IFormFile file, string folder, long maxBytes, CancellationToken cancellationToken = default);
    Task DeleteImageAsync(string? relativePath, CancellationToken cancellationToken = default);
}
