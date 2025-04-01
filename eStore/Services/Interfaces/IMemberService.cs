using DataAccess.DTO;
using BusinessObject.Entities;
using Microsoft.Extensions.Options;

namespace eStore.Services.Interfaces
{
    public interface IMemberService
    {
        Task<List<MemberDTO>> GetAllMembersAsync();
        Task<MemberDTO> GetMemberByIdAsync(int id);
        Task<MemberDTO?> LoginAsync(string email, string password);
        Task<bool> IsAdmin(MemberDTO member);
        Task AddMemberAsync(MemberDTO member);
        Task UpdateMemberAsync(MemberUpdateDTO member);
        Task DeleteMemberAsync(int id);
    }
}
