using System;
using GlueHome.Api.Repositories;

namespace GlueHome.Api.Services
{
    public class AuthService
    {
        private static readonly string INVALID_AUTH_MSG = "Invalid username or password provided";

        private readonly MemberRepository _memberRepository;
        private readonly AuthRepository _authRepository;

        public bool IsAuthenticated(string username, string password) 
        {
            var member = _memberRepository.FindByEmail(username);
            if (member == null) {
                throw new ArgumentException(INVALID_AUTH_MSG);
            }

            var auth = _authRepository.FindOne(member.MemberId);
            if (auth == null) {
                throw new ArgumentException(INVALID_AUTH_MSG);
            }

            // yikes
            return auth.Password == password;
        }
    }
}