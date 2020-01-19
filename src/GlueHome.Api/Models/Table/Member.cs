using System;

namespace GlueHome.Api.Models.Table
{
    public class Member
    {
        public long MemberId { get; set; }
        public string Forename { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string AddressLine3 { get; set; }
        public string Postcode { get; set; }
        public bool IsPartner { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public DateTime DeleteDate { get; set; }
    }
}