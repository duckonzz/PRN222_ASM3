using DataAccess.DTO;
using DataAccess.Entities;

namespace eStore.Services.Interfaces
{
    public interface IMemberService
    {
        Task<List<MemberDTO>> GetAllMembersAsync();
        Task<MemberDTO> GetMemberByIdAsync(int id);
        Task<MemberDTO> LoginAsync(string email, string password);
        Task AddMemberAsync(MemberDTO member);
        Task UpdateMemberAsync(MemberDTO member);
        Task DeleteMemberAsync(int id);
    }
}
