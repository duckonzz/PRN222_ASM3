using DataAccess.DTO;
using BusinessObject.Entities;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories.Interfaces
{
    public interface IMemberRepository
    {
        public Task<List<MemberDTO>> GetAllAsync();
        public Task<MemberDTO> GetByIdAsync(int id);
        public Task<MemberDTO?> LoginAsync(string email, string password, IOptions<AdminAccountSettings> adminAccountSettings);
        public Task<bool> IsAdmin(MemberDTO member, IOptions<AdminAccountSettings> adminAccountSettings);
        public Task AddAsync(MemberDTO member);
        public Task UpdateAsync(MemberUpdateDTO member);
        public Task DeleteAsync(int id);
    }
}
