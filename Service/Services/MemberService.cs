using DataAccess.DTO;
using BusinessObject.Entities;
using DataAccess.Repositories.Interfaces;
using Service.Services.Interfaces;
using Microsoft.Extensions.Options;

namespace Service.Services
{
    public class MemberService : IMemberService
    {
        private readonly IMemberRepository _memberRepository;
        private readonly IOptions<AdminAccountSettings> _adminAccountSettings;

        public MemberService(IMemberRepository memberRepository, IOptions<AdminAccountSettings> adminAccountSettings)
        {
            _memberRepository = memberRepository;
            _adminAccountSettings = adminAccountSettings;
        }

        public async Task<List<MemberDTO>> GetAllMembersAsync()
        {
            return await _memberRepository.GetAllAsync();
        }
      

        public async Task<MemberDTO> GetMemberByIdAsync(int id)
        {
            return await _memberRepository.GetByIdAsync(id);
        }

        public async Task<MemberDTO> GetMemberByEmailAsync(string email)
        {
            return await _memberRepository.GetMemberByEmailAsync(email);
        }

        public async Task<MemberDTO?> LoginAsync(string email, string password)
        {
            return await _memberRepository.LoginAsync(email, password, _adminAccountSettings);
        }

        public async Task<bool> IsAdmin(MemberDTO member)
        {
            return await _memberRepository.IsAdmin(member, _adminAccountSettings);
        }

        public async Task AddMemberAsync(MemberDTO member)
        {
            await _memberRepository.AddAsync(member);
        }

        public async Task UpdateMemberAsync(MemberUpdateDTO member)
        {
            await _memberRepository.UpdateAsync(member);
        }

        public async Task DeleteMemberAsync(int id)
        {
            await _memberRepository.DeleteAsync(id);
        }

        public async Task<List<Member>> GetAllsAsync()
        {
           return await _memberRepository.GetAllsAsync();
        }
    }
}
