using System;
using GlueHome.Api.Models.Table;
using GlueHome.Api.Repositories;
using Microsoft.Extensions.Logging;

namespace GlueHome.Api.Authentication
{
    public class AuthService : IAuthenticator
    {
        private static readonly string INVALID_AUTH_MSG = "Invalid username or password provided";

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
                throw new ArgumentException(INVALID_AUTH_MSG);
            }

            var auth = authRepository.FindOne(member.MemberId);
            if (auth == null) {
                throw new ArgumentException(INVALID_AUTH_MSG);
            }

            // yikes
            return auth.Password == password;
        }
    }
}