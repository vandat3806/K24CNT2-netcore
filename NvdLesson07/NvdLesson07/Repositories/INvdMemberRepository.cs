using NvdLesson07.Models.DataModels;

namespace NvdLesson07.Repositories;

public interface INvdMemberRepository
{
    IReadOnlyList<NvdMember> GetAll();
    NvdMember? GetById(int id);
    NvdMember Add(NvdMember member);
    bool Update(NvdMember member);
    bool Delete(int id);
    bool UserNameExists(string userName, int? exceptId = null);
    bool EmailExists(string email, int? exceptId = null);
}
