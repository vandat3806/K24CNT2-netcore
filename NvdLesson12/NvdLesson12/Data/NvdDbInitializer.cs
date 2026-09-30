using Microsoft.EntityFrameworkCore;

namespace NvdLesson12.Data;

public static class NvdDbInitializer
{
    public static async Task InitializeAsync(IServiceProvider services)
    {
        await using var scope = services.CreateAsyncScope();
        var context = scope.ServiceProvider.GetRequiredService<NvdLesson12DbContext>();
        await context.Database.EnsureCreatedAsync();
    }
}
