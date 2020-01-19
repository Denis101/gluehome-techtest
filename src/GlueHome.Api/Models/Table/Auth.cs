using System;

namespace GlueHome.Api.Models.Table
{
    public class Auth
    {
        public long MemberId { get; set; }
        public string Password { get; set; }
        public DateTime LastLogin { get; set; }
    }
}