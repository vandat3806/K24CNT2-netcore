namespace NvdLesson12.Services;

public sealed class LocalImageStorageService(IWebHostEnvironment environment) : IImageStorageService
{
    private static readonly IReadOnlyDictionary<string, string> AllowedTypes =
        new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
        {
            [".jpg"] = "image/jpeg",
            [".jpeg"] = "image/jpeg",
            [".png"] = "image/png",
            [".gif"] = "image/gif",
            [".webp"] = "image/webp"
        };

    public async Task<string> SaveImageAsync(
        IFormFile file,
        string folder,
        long maxBytes,
        CancellationToken cancellationToken = default)
    {
        if (file.Length <= 0)
        {
            throw new InvalidOperationException("Tệp ảnh rỗng.");
        }

        if (file.Length > maxBytes)
        {
            throw new InvalidOperationException($"Tệp ảnh vượt quá giới hạn {maxBytes / 1024 / 1024} MB.");
        }

        var extension = Path.GetExtension(file.FileName).ToLowerInvariant();
        if (!AllowedTypes.TryGetValue(extension, out var expectedContentType) ||
            !string.Equals(file.ContentType, expectedContentType, StringComparison.OrdinalIgnoreCase))
        {
            throw new InvalidOperationException("Chỉ chấp nhận ảnh JPG, PNG, GIF hoặc WEBP.");
        }

        var signature = new byte[12];
        await using (var signatureStream = file.OpenReadStream())
        {
            var bytesRead = await signatureStream.ReadAsync(signature.AsMemory(0, signature.Length), cancellationToken);
            if (!HasValidSignature(extension, signature.AsSpan(0, bytesRead)))
            {
                throw new InvalidOperationException("Nội dung tệp không đúng định dạng ảnh đã khai báo.");
            }
        }
        var safeFolder = folder.Trim('/', '\\');
        if (string.IsNullOrWhiteSpace(safeFolder) || safeFolder.Contains("..", StringComparison.Ordinal))
        {
            throw new InvalidOperationException("Thư mục lưu ảnh không hợp lệ.");
        }

        var uploadRoot = Path.Combine(environment.WebRootPath, "uploads");
        var targetFolder = Path.GetFullPath(Path.Combine(uploadRoot, safeFolder));
        if (!targetFolder.StartsWith(Path.GetFullPath(uploadRoot) + Path.DirectorySeparatorChar, StringComparison.Ordinal))
        {
            throw new InvalidOperationException("Thư mục lưu ảnh không hợp lệ.");
        }

        Directory.CreateDirectory(targetFolder);
        var fileName = $"{Guid.NewGuid():N}{extension}";
        var fullPath = Path.Combine(targetFolder, fileName);
        await using var source = file.OpenReadStream();
        await using var target = new FileStream(fullPath, FileMode.CreateNew, FileAccess.Write, FileShare.None);
        await source.CopyToAsync(target, cancellationToken);

        return $"/uploads/{safeFolder.Replace('\\', '/')}/{fileName}";
    }

    public Task DeleteImageAsync(string? relativePath, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(relativePath) || !relativePath.StartsWith("/uploads/", StringComparison.Ordinal))
        {
            return Task.CompletedTask;
        }

        var uploadRoot = Path.GetFullPath(Path.Combine(environment.WebRootPath, "uploads"));
        var fullPath = Path.GetFullPath(Path.Combine(environment.WebRootPath, relativePath.TrimStart('/').Replace('/', Path.DirectorySeparatorChar)));
        if (fullPath.StartsWith(uploadRoot + Path.DirectorySeparatorChar, StringComparison.Ordinal) && File.Exists(fullPath))
        {
            File.Delete(fullPath);
        }

        return Task.CompletedTask;
    }

    private static bool HasValidSignature(string extension, ReadOnlySpan<byte> bytes) => extension switch
    {
        ".jpg" or ".jpeg" => bytes.Length >= 3 && bytes[0] == 0xFF && bytes[1] == 0xD8 && bytes[2] == 0xFF,
        ".png" => bytes.Length >= 8 && bytes[..8].SequenceEqual(new byte[] { 0x89, 0x50, 0x4E, 0x47, 0x0D, 0x0A, 0x1A, 0x0A }),
        ".gif" => bytes.Length >= 6 && (bytes[..6].SequenceEqual("GIF87a"u8) || bytes[..6].SequenceEqual("GIF89a"u8)),
        ".webp" => bytes.Length >= 12 && bytes[..4].SequenceEqual("RIFF"u8) && bytes.Slice(8, 4).SequenceEqual("WEBP"u8),
        _ => false
    };
}
