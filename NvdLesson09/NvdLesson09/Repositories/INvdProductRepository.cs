using NvdLesson09.Models.DataModels;

namespace NvdLesson09.Repositories;

public interface INvdProductRepository
{
    IReadOnlyList<NvdProduct> GetAll();
    IReadOnlyList<NvdCategory> GetCategories();
    NvdProduct? GetById(int id);
    NvdProduct Add(NvdProduct product);
    bool Update(NvdProduct product);
    bool Delete(int id);
    bool CategoryExists(int categoryId);
}
