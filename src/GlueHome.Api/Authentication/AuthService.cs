using GlueHome.Api.Models.Table;
using GlueHome.Api.Repositories;
using Microsoft.Extensions.Logging;

namespace GlueHome.Api.Authentication
{
    public class AuthService : IAuthenticator
    {
        private readonly ILogger<AuthService> logger;
        private readonly MemberRepository memberRepository;
        private readonly AuthRepository authRepository;

        public AuthService(
            ILogger<AuthService> logger,
            IRepository<Member> memberRepository,
            IRepository<Auth> authRepository) 
        {
            this.logger = logger;
            this.memberRepository = (MemberRepository)memberRepository;
            this.authRepository = (AuthRepository)authRepository;
        }

        public bool IsAuthenticated(string username, string password) 
        {
            var member = memberRepository.FindByEmail(username);
            if (member == null) {
                return false;
            }

            var auth = authRepository.FindOne(member.MemberId);
            if (auth == null) {
                return false;
            }

            // yikes
            return auth.Password == password;
        }
    }
}