using NvdLesson08.Models.DataModels;

namespace NvdLesson08.Repositories;

public interface INvdMemberRepository
{
    IReadOnlyList<NvdMember> GetAll();

    NvdMember? GetById(int id);

    bool UserNameExists(string userName, int? exceptId = null);

    bool EmailExists(string email, int? exceptId = null);

    int Add(NvdMember member);

    bool Update(NvdMember member);

    bool Delete(int id);

    bool ToggleStatus(int id);
}
